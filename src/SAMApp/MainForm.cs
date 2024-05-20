using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using OpenVinoSharp;
using DSize = System.Drawing.Size;
using DPoint = System.Drawing.Point;
using static OpenVinoSharp.Node;
using SharpCompress.Common;
using System;
using static OpenCvSharp.FileStorage;
using static System.Net.Mime.MediaTypeNames;
using Point = System.Drawing.Point;
namespace SAMApp
{
    public partial class MainForm : Form
    {
        Log log = Log.Instance;

        EncodePredictor encode;

        DecoderPredictor decoder;
        float[] image_encode_data = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cb_device.Items.Clear();
            cb_device.Items.AddRange(new object[] { "AUTO", "CPU", "GPU.0", "GPU.1" });
            cb_device.SelectedIndex = 1;
            log.set_current_cb(tb_msg);
        }


        public Bitmap ImageAdaption(System.Drawing.Image img, DSize bg_size)
        {
            DSize img_size = img.Size;
            double factor_img = (double)img_size.Width / (double)img_size.Height;
            double factor_bg = (double)bg_size.Width / (double)bg_size.Height;
            int mw = 0;
            int mh = 0;
            if (factor_img < factor_bg || factor_img == factor_bg)
            {
                double factor = (double)img_size.Height / (double)bg_size.Height;
                mh = bg_size.Height;
                mw = img_size.Width * mh / img_size.Height;
                if (mw > bg_size.Width)
                {
                    mw = bg_size.Width;
                    mh = (int)img_size.Height * mw / img_size.Width;
                }
            }
            else
            {
                double factor = (double)img_size.Height / (double)img_size.Width;
                mw = bg_size.Width;
                mh = (int)img_size.Height * mw / img_size.Width;
                if (mh > bg_size.Height)
                {
                    mh = bg_size.Height;
                    mw = img_size.Width * mh / img_size.Height;
                }
            }
            Bitmap dest_bitmap = new Bitmap(mw, mh);
            Graphics g = Graphics.FromImage(dest_bitmap);
            g.Clear(Color.Transparent);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, new Rectangle(0, 0, mw, mh), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
            return dest_bitmap;
        }

        private string check_rb(Control.ControlCollection controls)
        {
            string key = "";
            foreach (Control ctr in controls)
            {
                if (ctr is RadioButton && (ctr as RadioButton).Checked)
                {
                    key = ctr.Text;
                }
            }
            return key;
        }
        private void show_worn_msg_box(string message)
        {
            string caption = "Warning";
            MessageBoxButtons buttons = MessageBoxButtons.OK; // 设置按钮
            MessageBoxIcon icon = MessageBoxIcon.Warning; // 设置图标
            DialogResult result = MessageBox.Show(this, message, caption, buttons, icon);

            // 根据用户的点击按钮处理逻辑
            if (result == DialogResult.OK)
            {
                // 用户点击了OK
                return;
            }
        }


        private void rb_openvino_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_openvino.Checked)
            {
                cb_device.Items.Clear();
                cb_device.Items.AddRange(new object[] { "AUTO", "CPU", "GPU.0", "GPU.1" });
                cb_device.SelectedIndex = 1;
            }
        }

        private void rb_tensorrt_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_tensorrt.Checked)
            {
                cb_device.Items.Clear();
                cb_device.Items.AddRange(new object[] { "GPU.0", "GPU.1" });
                cb_device.SelectedIndex = 0;
            }
        }

        private void rb_onnx_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_onnx.Checked)
            {
                cb_device.Items.Clear();
                cb_device.Items.AddRange(new object[] { "CPU", "GPU.0", "GPU.1" });
                cb_device.SelectedIndex = 0;
            }
        }





        private void btn_selct_encode_model_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "E:\\GitCode\\segment-anything";
            dlg.Title = "Select inference model file.";
            dlg.Filter = "Model file(*.pdmodel,*.onnx,*.xml,*.engine)|*.pdmodel;*.onnx;*.xml;*.engine";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tb_encode_model_path.Text = dlg.FileName;
            }
        }


        DSize img_size = new DSize();
        DSize bg_size = new DSize();
        DSize zoom_img_size = new DSize();
        DPoint zoom_left_point = new DPoint();
        Point zoom_img_point = new Point();
        float img_scale = 0.0f;

        private void btn_select_image_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "E:\\ModelData";
            dlg.Title = "Select the test input file.";
            dlg.Filter = "Input file(*.png,*.jpg,*.jepg,*.mp4,*.mov)|*.png;*.jpg;*.jepg;*.mp4;*.mov";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tb_image_path.Text = dlg.FileName;
                Bitmap image = (Bitmap)System.Drawing.Image.FromFile(dlg.FileName);
                Bitmap new_img = ImageAdaption(image, pb_image.Size);
                img_size = image.Size;
                bg_size = pb_image.Size;
                zoom_img_size = new_img.Size;
                zoom_left_point = new DPoint(zoom_img_size.Width < bg_size.Width ? (bg_size.Width - zoom_img_size.Width) / 2 : 0,
                    zoom_img_size.Height < bg_size.Height ? (bg_size.Height - zoom_img_size.Height) / 2 : 0);
                pb_image.Image = image;
                tb_msg.Text = zoom_left_point.ToString();
                zoom_img_point.X = (bg_size.Width - zoom_img_size.Width) / 2;
                zoom_img_point.Y = (bg_size.Height - zoom_img_size.Height) / 2;
                img_scale = (float)img_size.Height /(float) bg_size.Height;
            }
        }

        private void btn_load_encode_model_Click(object sender, EventArgs e)
        {
            string engine_type_str = check_rb(gb_engine.Controls);
            if (engine_type_str == "")
            {
                show_worn_msg_box("Please select an inference engine.");
                return;
            }
            EngineType engine_type = MyEnum.GetEngineType<EngineType>(engine_type_str);
            encode = new EncodePredictor(tb_encode_model_path.Text, engine_type, cb_device.SelectedItem.ToString());
        }

        private void btn_image_encode_Click(object sender, EventArgs e)
        {
            Mat img = Cv2.ImRead(tb_image_path.Text);

            image_encode_data = encode.infer(img);
        }

        private void btn_save_encode_Click(object sender, EventArgs e)
        {
            if (image_encode_data != null && tb_image_path.Text != "")
            {
                string path = Path.GetDirectoryName(tb_image_path.Text);
                string name = Path.GetFileNameWithoutExtension(tb_image_path.Text);
                using (FileStream fs = new FileStream(Path.Combine(path, name + ".bin"), FileMode.Create))
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    foreach (float value in image_encode_data)
                    {
                        bw.Write(value);
                    }
                }
            }

        }

        private void btn_encode_select_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "E:\\ModelData\\image";
            dlg.Title = "Select the test input file.";
            dlg.Filter = "Input file(*.bin)|*.bin";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(tb_encode_path.Text, FileMode.Open))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    image_encode_data = new float[fs.Length / sizeof(float)];
                    for (int i = 0; i < image_encode_data.Length; i++)
                    {
                        image_encode_data[i] = br.ReadSingle();
                    }
                }
            }
        }

        List<DPoint> draw_forward_points = new List<DPoint>();
        List<DPoint> draw_reverse_points = new List<DPoint>();
        DPoint start; //画框的起始点
        DPoint end; //画框的结束点<br>bool blnDraw;//判断是否绘制<br>Rectangel rect;

        List<Rectangle> draw_rects = new List<Rectangle>();
        Rectangle rect = new Rectangle();
        bool flag_new_rect = false;

        private void pb_image_MouseDown(object sender, MouseEventArgs e)
        {
            if (!flag_draw_type && flag_click_mouse && pb_image.Image != null) // 选择点
            {
                pb_image.Invalidate();
                flag_draw = true;
                if (rb_add_forward_point.Checked)
                {
                    draw_forward_points.Add(e.Location);
                    lb_points.Items.Add("+P" + (draw_forward_points.Count - 1) + ": ("
                        + ((e.X - zoom_img_point.X) * img_scale).ToString("0") + ", "
                        + ((e.Y - zoom_img_point.Y) * img_scale).ToString("0") + ")");
                }
                if (rb_add_reverse_point.Checked)
                {
                    draw_reverse_points.Add(e.Location);
                    lb_points.Items.Add("-P" + (draw_reverse_points.Count - 1) + ": (" 
                        + ((e.X - zoom_img_point.X) * img_scale).ToString("0") + ", " 
                        + ((e.Y - zoom_img_point.Y) * img_scale).ToString("0") + ")");
                }
                if (rb_remove_point.Checked)
                {
                    DPoint p = e.Location;
                    for (int i = 0; i < draw_forward_points.Count; i++)
                    {
                        DPoint t = draw_forward_points[i];
                        if ((Math.Abs(t.X - p.X) < 20) && (Math.Abs(t.Y - p.Y) < 20))
                        {
                            draw_forward_points.Remove(t);
                        }
                    }
                    for (int i = 0; i < draw_reverse_points.Count; i++)
                    {
                        DPoint t = draw_reverse_points[i];
                        if ((Math.Abs(t.X - p.X) < 20) && (Math.Abs(t.Y - p.Y) < 20))
                        {
                            draw_reverse_points.Remove(t);
                        }
                    }
                    lb_points.Items.Clear();
                    for (int i = 0; i < draw_forward_points.Count; i++)
                    {
                        lb_points.Items.Add("+P" + i.ToString() + ": (" 
                            + ((draw_forward_points[i].X - zoom_img_point.X) * img_scale).ToString("0")+ ", " 
                            + ((draw_forward_points[i].Y - zoom_img_point.Y) * img_scale).ToString("0") + ")");
                    }
                    for (int i = 0; i < draw_reverse_points.Count; i++)
                    {
                        lb_points.Items.Add("-P" + i.ToString() + ": (" 
                            + ((draw_forward_points[i].X - zoom_img_point.X) * img_scale).ToString("0") + ", " 
                            + ((draw_reverse_points[i].Y - zoom_img_point.Y) * img_scale).ToString("0") + ")");
                    }

                }

            }
            if (flag_draw_type && flag_click_mouse && pb_image.Image != null) // 选择框
            {
                pb_image.Invalidate();
                flag_draw = true;
                if (rb_add_box.Checked)
                {
                    start = e.Location;
                    flag_new_rect = true;
                }
                if (rb_remove_box.Checked)
                {
                    DPoint p = e.Location;
                    for (int i = 0; i < draw_rects.Count; i++)
                    {
                        Rectangle r = draw_rects[i];
                        if (p.X > r.Location.X && p.X < (r.Location.X + r.Size.Width) && p.Y > r.Location.Y && p.Y < (r.Location.Y + r.Size.Height))
                        {
                            draw_rects.Remove(r);
                        }
                    }
                    lb_rects.Items.Clear();
                    for (int i = 0; i < draw_rects.Count; i++)
                    {
                        //lb_rects.Items.Add(draw_rects[i].ToString());
                        lb_rects.Items.Add("R" + i.ToString() + ": (" 
                            + ((draw_rects[i].X - zoom_img_point.X) * img_scale).ToString("0")+ ", " 
                            + ((draw_rects[i].Y - zoom_img_point.Y) * img_scale).ToString("0") + ", "
                            + (draw_rects[i].Width * img_scale).ToString("0") + ", " 
                            + (draw_rects[i].Height * img_scale).ToString("0") + ")");
                    }
                }
            }

        }

        private void pb_image_MouseUp(object sender, MouseEventArgs e)
        {

            if (flag_new_rect && flag_draw_type && flag_click_mouse && pb_image.Image != null) // 选择框
            {
                if (rect.Width > 20 && rect.Height > 20)
                {
                    draw_rects.Add(rect);
                    lb_rects.Items.Add("R" + (draw_rects.Count - 1).ToString() + ": (" 
                        + ((rect.X - zoom_img_point.X) * img_scale).ToString("0")+ ", " 
                        + ((rect.Y - zoom_img_point.Y) * img_scale).ToString("0") + ", " 
                        + (rect.Width * img_scale).ToString("0") + ", " 
                        + (rect.Height * img_scale).ToString("0") + ")");
                }
            }
            flag_draw = false;
            flag_new_rect = false;
        }

        private void pb_image_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag_draw)
            {
                if (flag_draw_type && flag_click_mouse) // 选择框
                {
                    DPoint tempEndPoint = e.Location; //记录框的位置和大小
                    rect.Location = new DPoint(
                    Math.Min(start.X, tempEndPoint.X),
                    Math.Min(start.Y, tempEndPoint.Y));
                    rect.Size = new DSize(
                    Math.Abs(start.X - tempEndPoint.X),
                    Math.Abs(start.Y - tempEndPoint.Y));
                    pb_image.Invalidate();
                }
            }
        }

        private void pb_image_Paint(object sender, PaintEventArgs e)
        {
            if (flag_draw && pb_image.Image != null && flag_click_mouse)
            {
                if (rect != null && rect.Width > 0 && rect.Height > 0)
                {
                    if (flag_new_rect && rect.Width > 20 && rect.Height > 20)
                    {
                        e.Graphics.DrawRectangle(new Pen(Color.Red, 3), rect);//重新绘制颜色为红色
                        e.Graphics.DrawString("Rect " + draw_rects.Count, new Font("Arial", 12), Brushes.Black, new PointF(rect.X, rect.Y - 20));
                    }

                    for (int i = 0; i < draw_rects.Count; ++i)
                    {
                        Rectangle r = draw_rects[i];
                        e.Graphics.DrawRectangle(new Pen(Color.Red, 3), r);//重新绘制颜色为红色
                        e.Graphics.DrawString("Rect " + i, new Font("Arial", 12), Brushes.Black, new PointF(r.X, r.Y - 20));
                    }
                }
            }
            if (flag_draw && pb_image.Image != null && flag_click_mouse)
            {
                if (draw_forward_points.Count > 0)
                {
                    for (int i = 0; i < draw_forward_points.Count; ++i)
                    {
                        e.Graphics.FillEllipse(Brushes.Red, draw_forward_points[i].X, draw_forward_points[i].Y, 10, 10);
                        e.Graphics.DrawString("+Point " + i, new Font("Arial", 12), Brushes.Black, new PointF(draw_forward_points[i].X + 10, draw_forward_points[i].Y - 5));
                    }
                }
                if (draw_reverse_points.Count > 0)
                {
                    for (int i = 0; i < draw_reverse_points.Count; ++i)
                    {
                        e.Graphics.FillEllipse(Brushes.Red, draw_reverse_points[i].X, draw_reverse_points[i].Y, 10, 10);
                        e.Graphics.DrawString("-Point " + i, new Font("Arial", 12), Brushes.Black, new PointF(draw_reverse_points[i].X + 10, draw_reverse_points[i].Y - 5));
                    }
                }
            }
        }

        // 选择内容
        bool flag_draw_type = false; // flase: point, true: rect
        private void tc_draw_Selected(object sender, TabControlEventArgs e)
        {
            if (tc_draw.SelectedIndex == 0)
            {
                flag_draw_type = false;
            }
            else
            {
                flag_draw_type = true;
            }
            tb_msg.Text = flag_draw_type.ToString();
        }

        // 是否点击绘制按键
        bool flag_click_mouse = false;

        // 是否已经开始绘制
        bool flag_draw = false;
        private void btn_click_point_Click(object sender, EventArgs e)
        {
            if (btn_click_point.Text == "Click")
            {
                flag_click_mouse = true;
                btn_click_point.Text = "Unclick";
                btn_click_box.Text = "Unclick";
            }
            else
            {
                flag_click_mouse = false;
                btn_click_point.Text = "Click";
                btn_click_box.Text = "Click";
            }
        }

        private void btn_click_box_Click(object sender, EventArgs e)
        {
            if (btn_click_box.Text == "Click")
            {
                flag_click_mouse = true;
                btn_click_point.Text = "Unclick";
                btn_click_box.Text = "Unclick";
            }
            else
            {
                flag_click_mouse = false;
                btn_click_point.Text = "Click";
                btn_click_box.Text = "Click";
            }
        }

        private void btb_select_decoder_model_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "E:\\GitCode\\segment-anything";
            dlg.Title = "Select inference model file.";
            dlg.Filter = "Model file(*.pdmodel,*.onnx,*.xml,*.engine)|*.pdmodel;*.onnx;*.xml;*.engine";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tb_decoder_model_path.Text = dlg.FileName;
            }
        }

        private void btn_load_decoder_model_Click(object sender, EventArgs e)
        {
            string engine_type_str = check_rb(gb_engine.Controls);
            if (engine_type_str == "")
            {
                show_worn_msg_box("Please select an inference engine.");
                return;
            }
            EngineType engine_type = MyEnum.GetEngineType<EngineType>(engine_type_str);
            decoder = new DecoderPredictor(tb_decoder_model_path.Text, engine_type, cb_device.SelectedItem.ToString());
        }

        private void btn_decoder_infer_Click(object sender, EventArgs e)
        {
            Mat img = Cv2.ImRead(tb_image_path.Text);
            int num = ((img.Cols > img.Rows) ? img.Cols : img.Rows);
            float scales = (float)num / 1024.0f;
            List<float> point_coords = new List<float>();
            List<float> point_labels = new List<float>();
            foreach (var p in draw_forward_points) 
            {
                point_coords.Add((p.X - zoom_img_point.X) * img_scale / scales);
                point_coords.Add((p.Y - zoom_img_point.Y) * img_scale / scales);
                point_labels.Add(1.0f);
            }

            foreach (var p in draw_reverse_points)
            {
                point_coords.Add((p.X - zoom_img_point.X) * img_scale / scales);
                point_coords.Add((p.Y - zoom_img_point.Y) * img_scale / scales);
                point_labels.Add(-1.0f);
            }
        }

        private void btn_unload_image_Click(object sender, EventArgs e)
        {

        }





        //private void button1_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog dlg = new OpenFileDialog();
        //    dlg.InitialDirectory = "E:\\ModelData";
        //    dlg.Title = "Select the test input file.";
        //    dlg.Filter = "Input file(*.png,*.jpg,*.jepg,*.mp4,*.mov)|*.png;*.jpg;*.jepg;*.mp4;*.mov";
        //    string path = "";
        //    if (dlg.ShowDialog() == DialogResult.OK)
        //    {
        //        path = dlg.FileName;
        //    }

        //    Bitmap image = (Bitmap)Image.FromFile(path);
        //    //Bitmap new_img = ImageAdaption(image, pictureBox1.Size);

        //    pictureBox1.Image = image;


        //}


        //Point start; //画框的起始点
        //Point end;//画框的结束点<br>bool blnDraw;//判断是否绘制<br>Rectangel rect;
        //Rectangle rect = new Rectangle();
        //bool blnDraw = false;
        //private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        //{

        //    start = e.Location;
        //    Invalidate();
        //    blnDraw = true;
        //}

        //private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (blnDraw)
        //    {
        //        if (e.Button != MouseButtons.Left)//判断是否按下左键
        //            return;
        //        Point tempEndPoint = e.Location; //记录框的位置和大小
        //        rect.Location = new Point(
        //        Math.Min(start.X, tempEndPoint.X),
        //        Math.Min(start.Y, tempEndPoint.Y));
        //        rect.Size = new Size(
        //        Math.Abs(start.X - tempEndPoint.X),
        //        Math.Abs(start.Y - tempEndPoint.Y));
        //        pictureBox1.Invalidate();
        //    }
        //}

        //private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    blnDraw = false; //结束绘制
        //    textBox1.AppendText(start.ToString());

        //}


        //private void pictureBox1_Paint(object sender, PaintEventArgs e)
        //{
        //    if (blnDraw)
        //    {
        //        if (pictureBox1.Image != null)
        //        {
        //            if (rect != null && rect.Width > 0 && rect.Height > 0)
        //            {
        //                e.Graphics.DrawRectangle(new Pen(Color.Red, 3), rect);//重新绘制颜色为红色
        //            }
        //        }
        //    }
        //}
    }
}
