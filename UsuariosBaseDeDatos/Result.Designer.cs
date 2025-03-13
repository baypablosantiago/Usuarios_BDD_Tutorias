namespace UsuariosBaseDeDatos
{
    partial class Result
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
            groupBox1 = new GroupBox();
            labelUserName = new Label();
            labelSecret = new Label();
            helpProvider1 = new HelpProvider();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(labelUserName);
            groupBox1.Controls.Add(labelSecret);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(-5, 1);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(478, 163);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Mensaje de Sistema:";
            // 
            // labelUserName
            // 
            labelUserName.AutoSize = true;
            labelUserName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelUserName.Location = new Point(23, 43);
            labelUserName.Name = "labelUserName";
            labelUserName.Size = new Size(52, 21);
            labelUserName.TabIndex = 1;
            labelUserName.Text = "label1";
            // 
            // labelSecret
            // 
            labelSecret.AutoSize = true;
            labelSecret.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelSecret.Location = new Point(23, 91);
            labelSecret.Name = "labelSecret";
            labelSecret.Size = new Size(89, 21);
            labelSecret.TabIndex = 0;
            labelSecret.Text = "labelSecret";
            // 
            // Result
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(467, 157);
            Controls.Add(groupBox1);
            Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "Result";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Resultado Login";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        public GroupBox groupBox1;
        private HelpProvider helpProvider1;
        public Label labelSecret;
        public Label labelUserName;
    }
}