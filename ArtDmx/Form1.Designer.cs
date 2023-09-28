namespace ArtNetToDMX
{
    partial class ArtNet_to_DMX
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
            components = new System.ComponentModel.Container();
            IP_Label = new Label();
            Uni_Label = new Label();
            Address_cb = new ComboBox();
            Universe_cb = new ComboBox();
            Stop_button = new Button();
            Start_button = new Button();
            oneUniverse_checkbox = new CheckBox();
            label1 = new Label();
            Save_button = new Button();
            toolTip1 = new ToolTip(components);
            autoStart_checkbox = new CheckBox();
            SuspendLayout();
            // 
            // IP_Label
            // 
            IP_Label.AutoSize = true;
            IP_Label.Font = new Font("Segoe UI", 30F, FontStyle.Regular, GraphicsUnit.Point);
            IP_Label.Location = new Point(12, 9);
            IP_Label.Name = "IP_Label";
            IP_Label.Size = new Size(81, 67);
            IP_Label.TabIndex = 0;
            IP_Label.Text = "IP:";
            // 
            // Uni_Label
            // 
            Uni_Label.AutoSize = true;
            Uni_Label.Font = new Font("Segoe UI", 30F, FontStyle.Regular, GraphicsUnit.Point);
            Uni_Label.Location = new Point(12, 106);
            Uni_Label.Name = "Uni_Label";
            Uni_Label.Size = new Size(114, 67);
            Uni_Label.TabIndex = 1;
            Uni_Label.Text = "Uni:";
            // 
            // Address_cb
            // 
            Address_cb.Font = new Font("Segoe UI", 30F, FontStyle.Regular, GraphicsUnit.Point);
            Address_cb.FormattingEnabled = true;
            Address_cb.Location = new Point(99, 6);
            Address_cb.Name = "Address_cb";
            Address_cb.Size = new Size(365, 75);
            Address_cb.TabIndex = 2;
            Address_cb.KeyPress += Address_cb_KeyPress;
            // 
            // Universe_cb
            // 
            Universe_cb.DropDownHeight = 210;
            Universe_cb.DropDownStyle = ComboBoxStyle.DropDownList;
            Universe_cb.Font = new Font("Segoe UI", 30F, FontStyle.Regular, GraphicsUnit.Point);
            Universe_cb.FormattingEnabled = true;
            Universe_cb.IntegralHeight = false;
            Universe_cb.ItemHeight = 67;
            Universe_cb.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" });
            Universe_cb.Location = new Point(123, 106);
            Universe_cb.MaxDropDownItems = 16;
            Universe_cb.Name = "Universe_cb";
            Universe_cb.Size = new Size(341, 75);
            Universe_cb.TabIndex = 3;
            // 
            // Stop_button
            // 
            Stop_button.Enabled = false;
            Stop_button.Font = new Font("Segoe UI", 35F, FontStyle.Regular, GraphicsUnit.Point);
            Stop_button.Location = new Point(12, 187);
            Stop_button.Name = "Stop_button";
            Stop_button.Size = new Size(215, 82);
            Stop_button.TabIndex = 4;
            Stop_button.Text = "Stop";
            Stop_button.TextAlign = ContentAlignment.TopCenter;
            Stop_button.UseVisualStyleBackColor = true;
            Stop_button.Click += Stop_button_Click;
            // 
            // Start_button
            // 
            Start_button.Font = new Font("Segoe UI", 35F, FontStyle.Regular, GraphicsUnit.Point);
            Start_button.Location = new Point(231, 187);
            Start_button.Name = "Start_button";
            Start_button.Size = new Size(231, 82);
            Start_button.TabIndex = 5;
            Start_button.Text = "Start";
            Start_button.UseVisualStyleBackColor = true;
            Start_button.Click += Start_button_Click;
            // 
            // oneUniverse_checkbox
            // 
            oneUniverse_checkbox.AutoSize = true;
            oneUniverse_checkbox.Checked = true;
            oneUniverse_checkbox.CheckState = CheckState.Checked;
            oneUniverse_checkbox.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            oneUniverse_checkbox.Location = new Point(12, 275);
            oneUniverse_checkbox.Name = "oneUniverse_checkbox";
            oneUniverse_checkbox.Size = new Size(522, 39);
            oneUniverse_checkbox.TabIndex = 6;
            oneUniverse_checkbox.Text = "Only One Universe Running Through This IP";
            oneUniverse_checkbox.UseVisualStyleBackColor = true;
            oneUniverse_checkbox.CheckedChanged += oneUniverse_checkbox_CheckedChanged;
            oneUniverse_checkbox.MouseHover += oneUniverse_checkbox_MouseHover;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(476, 9);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(40, 230);
            label1.TabIndex = 7;
            label1.Text = "n\r\nt\r\n2\r\nd\r\ns\r\n";
            // 
            // Save_button
            // 
            Save_button.BackgroundImageLayout = ImageLayout.Center;
            Save_button.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);
            Save_button.Location = new Point(468, 233);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(40, 36);
            Save_button.TabIndex = 8;
            Save_button.Text = "Save";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // autoStart_checkbox
            // 
            autoStart_checkbox.AutoSize = true;
            autoStart_checkbox.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            autoStart_checkbox.Location = new Point(12, 320);
            autoStart_checkbox.Name = "autoStart_checkbox";
            autoStart_checkbox.Size = new Size(248, 39);
            autoStart_checkbox.TabIndex = 10;
            autoStart_checkbox.Text = "Autostart next time";
            autoStart_checkbox.UseVisualStyleBackColor = true;
            autoStart_checkbox.MouseHover += autoStart_checkbox_MouseHover;
            // 
            // ArtNet_to_DMX
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(528, 367);
            Controls.Add(autoStart_checkbox);
            Controls.Add(Save_button);
            Controls.Add(label1);
            Controls.Add(oneUniverse_checkbox);
            Controls.Add(Start_button);
            Controls.Add(Stop_button);
            Controls.Add(Universe_cb);
            Controls.Add(Address_cb);
            Controls.Add(Uni_Label);
            Controls.Add(IP_Label);
            MaximizeBox = false;
            Name = "ArtNet_to_DMX";
            Text = "ArtNet to DMX FTDI";
            FormClosing += CloseApp;
            Load += ArtNet_to_DMX_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label IP_Label;
        private Label Uni_Label;
        private ComboBox Address_cb;
        private ComboBox Universe_cb;
        private Button Stop_button;
        private Button Start_button;
        private CheckBox oneUniverse_checkbox;
        private Label label1;
        private Button Save_button;
        private ToolTip toolTip1;
        private CheckBox autoStart_checkbox;
    }
}