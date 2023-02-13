namespace ArtNetDMX
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.StartProgram = new System.Windows.Forms.Button();
            this.recvIPcombobox = new System.Windows.Forms.ComboBox();
            this.recvUniCombobox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StartProgram
            // 
            this.StartProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartProgram.Location = new System.Drawing.Point(12, 133);
            this.StartProgram.Name = "StartProgram";
            this.StartProgram.Size = new System.Drawing.Size(145, 56);
            this.StartProgram.TabIndex = 0;
            this.StartProgram.Text = "Start";
            this.StartProgram.UseVisualStyleBackColor = true;
            this.StartProgram.Click += new System.EventHandler(this.StartProgram_Click);
            // 
            // recvIPcombobox
            // 
            this.recvIPcombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recvIPcombobox.FormattingEnabled = true;
            this.recvIPcombobox.Location = new System.Drawing.Point(214, 10);
            this.recvIPcombobox.Name = "recvIPcombobox";
            this.recvIPcombobox.Size = new System.Drawing.Size(298, 46);
            this.recvIPcombobox.TabIndex = 1;
            this.recvIPcombobox.SelectedIndexChanged += new System.EventHandler(this.recvIPcombobox_SelectedIndexChanged);
            // 
            // recvUniCombobox
            // 
            this.recvUniCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recvUniCombobox.FormattingEnabled = true;
            this.recvUniCombobox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.recvUniCombobox.Location = new System.Drawing.Point(339, 77);
            this.recvUniCombobox.MaxDropDownItems = 16;
            this.recvUniCombobox.MaxLength = 2;
            this.recvUniCombobox.Name = "recvUniCombobox";
            this.recvUniCombobox.Size = new System.Drawing.Size(173, 46);
            this.recvUniCombobox.TabIndex = 2;
            this.recvUniCombobox.SelectedIndexChanged += new System.EventHandler(this.recvUniCombobox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 39);
            this.label1.TabIndex = 3;
            this.label1.Text = "ArtNet IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(294, 39);
            this.label2.TabIndex = 4;
            this.label2.Text = "Receive Universe:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(235, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 39);
            this.label3.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 217);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.recvUniCombobox);
            this.Controls.Add(this.recvIPcombobox);
            this.Controls.Add(this.StartProgram);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ArtNet to DMX FTDI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartProgram;
        private System.Windows.Forms.ComboBox recvIPcombobox;
        private System.Windows.Forms.ComboBox recvUniCombobox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

