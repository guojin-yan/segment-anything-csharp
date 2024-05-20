namespace SAMApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pb_image = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            tb_encode_model_path = new TextBox();
            panel1 = new Panel();
            btn_save_encode = new Button();
            btn_image_encode = new Button();
            btn_load_encode_model = new Button();
            btn_select_image = new Button();
            btn_selct_encode_model = new Button();
            tb_image_path = new TextBox();
            panel2 = new Panel();
            lb_rects = new ListBox();
            lb_points = new ListBox();
            tc_draw = new TabControl();
            tabPage1 = new TabPage();
            btn_click_point = new Button();
            rb_remove_point = new RadioButton();
            rb_add_reverse_point = new RadioButton();
            rb_add_forward_point = new RadioButton();
            tabPage2 = new TabPage();
            btn_click_box = new Button();
            rb_remove_box = new RadioButton();
            rb_add_box = new RadioButton();
            btn_encode_select = new Button();
            btb_select_decoder_model = new Button();
            tb_encode_path = new TextBox();
            label5 = new Label();
            label8 = new Label();
            tb_decoder_model_path = new TextBox();
            tb_msg = new TextBox();
            cb_device = new ComboBox();
            gb_engine = new GroupBox();
            rb_onnx = new RadioButton();
            rb_tensorrt = new RadioButton();
            rb_openvino = new RadioButton();
            label6 = new Label();
            panel3 = new Panel();
            label7 = new Label();
            btn_load_decoder_model = new Button();
            btn_decoder_infer = new Button();
            btn_unload_image = new Button();
            label9 = new Label();
            ((System.ComponentModel.ISupportInitialize)pb_image).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            tc_draw.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            gb_engine.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // pb_image
            // 
            pb_image.BackColor = SystemColors.ControlDark;
            pb_image.Location = new Point(537, 93);
            pb_image.Name = "pb_image";
            pb_image.Size = new Size(1308, 901);
            pb_image.SizeMode = PictureBoxSizeMode.Zoom;
            pb_image.TabIndex = 0;
            pb_image.TabStop = false;
            pb_image.Paint += pb_image_Paint;
            pb_image.MouseDown += pb_image_MouseDown;
            pb_image.MouseMove += pb_image_MouseMove;
            pb_image.MouseUp += pb_image_MouseUp;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(410, 19);
            label1.Name = "label1";
            label1.Size = new Size(848, 40);
            label1.TabIndex = 1;
            label1.Text = "Segment Anything Model  Deployment Testing Platform";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.GradientInactiveCaption;
            label2.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(15, 91);
            label2.Name = "label2";
            label2.Size = new Size(90, 19);
            label2.TabIndex = 2;
            label2.Text = "Image Path:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(171, 11);
            label3.Name = "label3";
            label3.Size = new Size(138, 24);
            label3.TabIndex = 1;
            label3.Text = "Image Encode";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.GradientInactiveCaption;
            label4.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(15, 50);
            label4.Name = "label4";
            label4.Size = new Size(92, 19);
            label4.TabIndex = 2;
            label4.Text = "Model Path:";
            // 
            // tb_encode_model_path
            // 
            tb_encode_model_path.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tb_encode_model_path.Location = new Point(113, 47);
            tb_encode_model_path.Name = "tb_encode_model_path";
            tb_encode_model_path.Size = new Size(251, 26);
            tb_encode_model_path.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(btn_save_encode);
            panel1.Controls.Add(btn_image_encode);
            panel1.Controls.Add(btn_load_encode_model);
            panel1.Controls.Add(btn_select_image);
            panel1.Controls.Add(btn_selct_encode_model);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(tb_image_path);
            panel1.Controls.Add(tb_encode_model_path);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(12, 227);
            panel1.Name = "panel1";
            panel1.Size = new Size(493, 187);
            panel1.TabIndex = 4;
            // 
            // btn_save_encode
            // 
            btn_save_encode.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btn_save_encode.Location = new Point(334, 133);
            btn_save_encode.Name = "btn_save_encode";
            btn_save_encode.Size = new Size(140, 40);
            btn_save_encode.TabIndex = 4;
            btn_save_encode.Text = "Save Encode";
            btn_save_encode.UseVisualStyleBackColor = true;
            btn_save_encode.Click += btn_save_encode_Click;
            // 
            // btn_image_encode
            // 
            btn_image_encode.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btn_image_encode.Location = new Point(174, 133);
            btn_image_encode.Name = "btn_image_encode";
            btn_image_encode.Size = new Size(140, 40);
            btn_image_encode.TabIndex = 4;
            btn_image_encode.Text = "Image Encode";
            btn_image_encode.UseVisualStyleBackColor = true;
            btn_image_encode.Click += btn_image_encode_Click;
            // 
            // btn_load_encode_model
            // 
            btn_load_encode_model.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btn_load_encode_model.Location = new Point(18, 133);
            btn_load_encode_model.Name = "btn_load_encode_model";
            btn_load_encode_model.Size = new Size(140, 40);
            btn_load_encode_model.TabIndex = 4;
            btn_load_encode_model.Text = "Load Model";
            btn_load_encode_model.UseVisualStyleBackColor = true;
            btn_load_encode_model.Click += btn_load_encode_model_Click;
            // 
            // btn_select_image
            // 
            btn_select_image.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_select_image.Location = new Point(381, 84);
            btn_select_image.Name = "btn_select_image";
            btn_select_image.Size = new Size(80, 30);
            btn_select_image.TabIndex = 4;
            btn_select_image.Text = "Select";
            btn_select_image.UseVisualStyleBackColor = true;
            btn_select_image.Click += btn_select_image_Click;
            // 
            // btn_selct_encode_model
            // 
            btn_selct_encode_model.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_selct_encode_model.Location = new Point(381, 43);
            btn_selct_encode_model.Name = "btn_selct_encode_model";
            btn_selct_encode_model.Size = new Size(80, 30);
            btn_selct_encode_model.TabIndex = 4;
            btn_selct_encode_model.Text = "Select";
            btn_selct_encode_model.UseVisualStyleBackColor = true;
            btn_selct_encode_model.Click += btn_selct_encode_model_Click;
            // 
            // tb_image_path
            // 
            tb_image_path.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tb_image_path.Location = new Point(113, 87);
            tb_image_path.Name = "tb_image_path";
            tb_image_path.Size = new Size(251, 26);
            tb_image_path.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.InactiveCaption;
            panel2.Controls.Add(lb_rects);
            panel2.Controls.Add(lb_points);
            panel2.Controls.Add(btn_unload_image);
            panel2.Controls.Add(btn_decoder_infer);
            panel2.Controls.Add(btn_load_decoder_model);
            panel2.Controls.Add(tc_draw);
            panel2.Controls.Add(btn_encode_select);
            panel2.Controls.Add(btb_select_decoder_model);
            panel2.Controls.Add(tb_encode_path);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(tb_decoder_model_path);
            panel2.Location = new Point(12, 430);
            panel2.Name = "panel2";
            panel2.Size = new Size(493, 360);
            panel2.TabIndex = 5;
            // 
            // lb_rects
            // 
            lb_rects.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lb_rects.FormattingEnabled = true;
            lb_rects.ItemHeight = 19;
            lb_rects.Location = new Point(298, 137);
            lb_rects.Name = "lb_rects";
            lb_rects.SelectionMode = SelectionMode.MultiSimple;
            lb_rects.Size = new Size(185, 156);
            lb_rects.TabIndex = 6;
            // 
            // lb_points
            // 
            lb_points.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lb_points.FormattingEnabled = true;
            lb_points.ItemHeight = 19;
            lb_points.Location = new Point(170, 137);
            lb_points.Name = "lb_points";
            lb_points.SelectionMode = SelectionMode.MultiSimple;
            lb_points.Size = new Size(120, 156);
            lb_points.TabIndex = 6;
            // 
            // tc_draw
            // 
            tc_draw.Controls.Add(tabPage1);
            tc_draw.Controls.Add(tabPage2);
            tc_draw.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            tc_draw.Location = new Point(7, 131);
            tc_draw.Name = "tc_draw";
            tc_draw.SelectedIndex = 0;
            tc_draw.Size = new Size(155, 162);
            tc_draw.TabIndex = 0;
            tc_draw.Selected += tc_draw_Selected;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btn_click_point);
            tabPage1.Controls.Add(rb_remove_point);
            tabPage1.Controls.Add(rb_add_reverse_point);
            tabPage1.Controls.Add(rb_add_forward_point);
            tabPage1.Location = new Point(4, 28);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(147, 130);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "  Point  ";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_click_point
            // 
            btn_click_point.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_click_point.Location = new Point(31, 92);
            btn_click_point.Name = "btn_click_point";
            btn_click_point.Size = new Size(80, 30);
            btn_click_point.TabIndex = 6;
            btn_click_point.Text = "Click";
            btn_click_point.UseVisualStyleBackColor = true;
            btn_click_point.Click += btn_click_point_Click;
            // 
            // rb_remove_point
            // 
            rb_remove_point.AutoSize = true;
            rb_remove_point.Location = new Point(17, 61);
            rb_remove_point.Name = "rb_remove_point";
            rb_remove_point.Size = new Size(121, 23);
            rb_remove_point.TabIndex = 1;
            rb_remove_point.Text = "Remove Point";
            rb_remove_point.UseVisualStyleBackColor = true;
            // 
            // rb_add_reverse_point
            // 
            rb_add_reverse_point.AutoSize = true;
            rb_add_reverse_point.Checked = true;
            rb_add_reverse_point.Location = new Point(18, 35);
            rb_add_reverse_point.Name = "rb_add_reverse_point";
            rb_add_reverse_point.Size = new Size(101, 23);
            rb_add_reverse_point.TabIndex = 0;
            rb_add_reverse_point.TabStop = true;
            rb_add_reverse_point.Text = "Add - Point";
            rb_add_reverse_point.UseVisualStyleBackColor = true;
            // 
            // rb_add_forward_point
            // 
            rb_add_forward_point.AutoSize = true;
            rb_add_forward_point.Checked = true;
            rb_add_forward_point.Location = new Point(18, 9);
            rb_add_forward_point.Name = "rb_add_forward_point";
            rb_add_forward_point.Size = new Size(105, 23);
            rb_add_forward_point.TabIndex = 0;
            rb_add_forward_point.TabStop = true;
            rb_add_forward_point.Text = "Add + Point";
            rb_add_forward_point.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btn_click_box);
            tabPage2.Controls.Add(rb_remove_box);
            tabPage2.Controls.Add(rb_add_box);
            tabPage2.Location = new Point(4, 28);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(147, 130);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "   Box   ";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_click_box
            // 
            btn_click_box.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_click_box.Location = new Point(35, 85);
            btn_click_box.Name = "btn_click_box";
            btn_click_box.Size = new Size(80, 30);
            btn_click_box.TabIndex = 9;
            btn_click_box.Text = "Click";
            btn_click_box.UseVisualStyleBackColor = true;
            btn_click_box.Click += btn_click_box_Click;
            // 
            // rb_remove_box
            // 
            rb_remove_box.AutoSize = true;
            rb_remove_box.Location = new Point(24, 47);
            rb_remove_box.Name = "rb_remove_box";
            rb_remove_box.Size = new Size(114, 23);
            rb_remove_box.TabIndex = 8;
            rb_remove_box.Text = "Remove Box";
            rb_remove_box.UseVisualStyleBackColor = true;
            // 
            // rb_add_box
            // 
            rb_add_box.AutoSize = true;
            rb_add_box.Checked = true;
            rb_add_box.Location = new Point(24, 14);
            rb_add_box.Name = "rb_add_box";
            rb_add_box.Size = new Size(85, 23);
            rb_add_box.TabIndex = 7;
            rb_add_box.TabStop = true;
            rb_add_box.Text = "Add Box";
            rb_add_box.UseVisualStyleBackColor = true;
            // 
            // btn_encode_select
            // 
            btn_encode_select.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_encode_select.Location = new Point(381, 48);
            btn_encode_select.Name = "btn_encode_select";
            btn_encode_select.Size = new Size(80, 30);
            btn_encode_select.TabIndex = 4;
            btn_encode_select.Text = "Select";
            btn_encode_select.UseVisualStyleBackColor = true;
            btn_encode_select.Click += btn_encode_select_Click;
            // 
            // btb_select_decoder_model
            // 
            btb_select_decoder_model.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btb_select_decoder_model.Location = new Point(378, 89);
            btb_select_decoder_model.Name = "btb_select_decoder_model";
            btb_select_decoder_model.Size = new Size(80, 30);
            btb_select_decoder_model.TabIndex = 4;
            btb_select_decoder_model.Text = "Select";
            btb_select_decoder_model.UseVisualStyleBackColor = true;
            btb_select_decoder_model.Click += btb_select_decoder_model_Click;
            // 
            // tb_encode_path
            // 
            tb_encode_path.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tb_encode_path.Location = new Point(110, 51);
            tb_encode_path.Name = "tb_encode_path";
            tb_encode_path.Size = new Size(251, 26);
            tb_encode_path.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.InactiveCaption;
            label5.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(10, 54);
            label5.Name = "label5";
            label5.Size = new Size(100, 19);
            label5.TabIndex = 2;
            label5.Text = "Encode Data:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = SystemColors.InactiveCaption;
            label8.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label8.Location = new Point(12, 96);
            label8.Name = "label8";
            label8.Size = new Size(92, 19);
            label8.TabIndex = 2;
            label8.Text = "Model Path:";
            // 
            // tb_decoder_model_path
            // 
            tb_decoder_model_path.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tb_decoder_model_path.Location = new Point(110, 93);
            tb_decoder_model_path.Name = "tb_decoder_model_path";
            tb_decoder_model_path.Size = new Size(251, 26);
            tb_decoder_model_path.TabIndex = 3;
            // 
            // tb_msg
            // 
            tb_msg.Location = new Point(12, 814);
            tb_msg.Multiline = true;
            tb_msg.Name = "tb_msg";
            tb_msg.Size = new Size(493, 184);
            tb_msg.TabIndex = 6;
            // 
            // cb_device
            // 
            cb_device.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cb_device.FormattingEnabled = true;
            cb_device.Items.AddRange(new object[] { "CPU", "GPU" });
            cb_device.Location = new Point(338, 63);
            cb_device.Margin = new Padding(2, 3, 2, 3);
            cb_device.Name = "cb_device";
            cb_device.Size = new Size(127, 27);
            cb_device.TabIndex = 9;
            // 
            // gb_engine
            // 
            gb_engine.Controls.Add(rb_onnx);
            gb_engine.Controls.Add(rb_tensorrt);
            gb_engine.Controls.Add(rb_openvino);
            gb_engine.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            gb_engine.Location = new Point(15, 38);
            gb_engine.Margin = new Padding(2, 3, 2, 3);
            gb_engine.Name = "gb_engine";
            gb_engine.Padding = new Padding(2, 3, 2, 3);
            gb_engine.Size = new Size(241, 82);
            gb_engine.TabIndex = 8;
            gb_engine.TabStop = false;
            gb_engine.Text = "Inference Engine";
            // 
            // rb_onnx
            // 
            rb_onnx.AutoSize = true;
            rb_onnx.Location = new Point(14, 54);
            rb_onnx.Margin = new Padding(2, 3, 2, 3);
            rb_onnx.Name = "rb_onnx";
            rb_onnx.Size = new Size(74, 23);
            rb_onnx.TabIndex = 2;
            rb_onnx.Text = "ONNX";
            rb_onnx.UseVisualStyleBackColor = true;
            rb_onnx.CheckedChanged += rb_onnx_CheckedChanged;
            // 
            // rb_tensorrt
            // 
            rb_tensorrt.AutoSize = true;
            rb_tensorrt.Location = new Point(130, 25);
            rb_tensorrt.Margin = new Padding(2, 3, 2, 3);
            rb_tensorrt.Name = "rb_tensorrt";
            rb_tensorrt.Size = new Size(94, 23);
            rb_tensorrt.TabIndex = 1;
            rb_tensorrt.Text = "TensorRT";
            rb_tensorrt.UseVisualStyleBackColor = true;
            rb_tensorrt.CheckedChanged += rb_tensorrt_CheckedChanged;
            // 
            // rb_openvino
            // 
            rb_openvino.AutoSize = true;
            rb_openvino.Checked = true;
            rb_openvino.Location = new Point(14, 25);
            rb_openvino.Margin = new Padding(2, 3, 2, 3);
            rb_openvino.Name = "rb_openvino";
            rb_openvino.Size = new Size(104, 23);
            rb_openvino.TabIndex = 0;
            rb_openvino.TabStop = true;
            rb_openvino.Text = "OpenVINO";
            rb_openvino.UseVisualStyleBackColor = true;
            rb_openvino.CheckedChanged += rb_openvino_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(269, 67);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(61, 19);
            label6.TabIndex = 7;
            label6.Text = "Device:";
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ControlLight;
            panel3.Controls.Add(gb_engine);
            panel3.Controls.Add(cb_device);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(label7);
            panel3.Location = new Point(12, 93);
            panel3.Name = "panel3";
            panel3.Size = new Size(493, 123);
            panel3.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(154, 12);
            label7.Name = "label7";
            label7.Size = new Size(160, 24);
            label7.TabIndex = 1;
            label7.Text = "Inference Device";
            // 
            // btn_load_decoder_model
            // 
            btn_load_decoder_model.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btn_load_decoder_model.Location = new Point(12, 310);
            btn_load_decoder_model.Name = "btn_load_decoder_model";
            btn_load_decoder_model.Size = new Size(140, 40);
            btn_load_decoder_model.TabIndex = 4;
            btn_load_decoder_model.Text = "Load Model";
            btn_load_decoder_model.UseVisualStyleBackColor = true;
            btn_load_decoder_model.Click += btn_load_decoder_model_Click;
            // 
            // btn_decoder_infer
            // 
            btn_decoder_infer.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btn_decoder_infer.Location = new Point(169, 310);
            btn_decoder_infer.Name = "btn_decoder_infer";
            btn_decoder_infer.Size = new Size(140, 40);
            btn_decoder_infer.TabIndex = 4;
            btn_decoder_infer.Text = "Infer";
            btn_decoder_infer.UseVisualStyleBackColor = true;
            btn_decoder_infer.Click += btn_decoder_infer_Click;
            // 
            // btn_unload_image
            // 
            btn_unload_image.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btn_unload_image.Location = new Point(321, 310);
            btn_unload_image.Name = "btn_unload_image";
            btn_unload_image.Size = new Size(140, 40);
            btn_unload_image.TabIndex = 4;
            btn_unload_image.Text = "Unload Image";
            btn_unload_image.UseVisualStyleBackColor = true;
            btn_unload_image.Click += btn_unload_image_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(169, 13);
            label9.Name = "label9";
            label9.Size = new Size(146, 24);
            label9.TabIndex = 1;
            label9.Text = "Image Decoder";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1857, 1010);
            Controls.Add(panel3);
            Controls.Add(tb_msg);
            Controls.Add(panel2);
            Controls.Add(label1);
            Controls.Add(pb_image);
            Controls.Add(panel1);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)pb_image).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tc_draw.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            gb_engine.ResumeLayout(false);
            gb_engine.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pb_image;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox tb_encode_model_path;
        private Panel panel1;
        private Button btn_image_encode;
        private Button btn_load_encode_model;
        private Button btn_select_image;
        private Button btn_selct_encode_model;
        private TextBox tb_image_path;
        private Panel panel2;
        private TabControl tc_draw;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private RadioButton rb_remove_point;
        private RadioButton rb_add_forward_point;
        private Button btn_click_point;
        private ListBox lb_points;
        private Button btn_click_box;
        private RadioButton rb_remove_box;
        private RadioButton rb_add_box;
        private Button btn_save_encode;
        private TextBox tb_msg;
        private ListBox lb_rects;
        private Button btn_encode_select;
        private TextBox tb_encode_path;
        private Label label5;
        private ComboBox cb_device;
        private GroupBox gb_engine;
        private RadioButton rb_onnx;
        private RadioButton rb_tensorrt;
        private RadioButton rb_openvino;
        private Label label6;
        private Panel panel3;
        private Label label7;
        private Button btb_select_decoder_model;
        private Label label8;
        private TextBox tb_decoder_model_path;
        private RadioButton rb_add_reverse_point;
        private Button btn_load_decoder_model;
        private Button btn_unload_image;
        private Button btn_decoder_infer;
        private Label label9;
    }
}
