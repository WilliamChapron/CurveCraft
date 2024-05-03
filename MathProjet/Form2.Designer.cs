namespace MathProjet
{
    partial class Form2
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
            this.start_Demo1_btn = new System.Windows.Forms.Button();
            this.start_Demo2_btn = new System.Windows.Forms.Button();
            this.start_Demo3_btn = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // start_Demo1_btn
            // 
            this.start_Demo1_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.start_Demo1_btn.Location = new System.Drawing.Point(114, 147);
            this.start_Demo1_btn.Name = "start_Demo1_btn";
            this.start_Demo1_btn.Size = new System.Drawing.Size(185, 61);
            this.start_Demo1_btn.TabIndex = 1;
            this.start_Demo1_btn.Text = "Démarrer la démo exercice 1";
            this.start_Demo1_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.start_Demo1_btn.UseVisualStyleBackColor = true;
            this.start_Demo1_btn.Click += new System.EventHandler(this.start_App_btn_Click);
            // 
            // start_Demo2_btn
            // 
            this.start_Demo2_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.start_Demo2_btn.Location = new System.Drawing.Point(305, 147);
            this.start_Demo2_btn.Name = "start_Demo2_btn";
            this.start_Demo2_btn.Size = new System.Drawing.Size(185, 61);
            this.start_Demo2_btn.TabIndex = 2;
            this.start_Demo2_btn.Text = "Démarrer la démo exercice 2";
            this.start_Demo2_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.start_Demo2_btn.UseVisualStyleBackColor = true;
            this.start_Demo2_btn.Click += new System.EventHandler(this.start_Demo2_btn_Click);
            // 
            // start_Demo3_btn
            // 
            this.start_Demo3_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.start_Demo3_btn.Location = new System.Drawing.Point(496, 147);
            this.start_Demo3_btn.Name = "start_Demo3_btn";
            this.start_Demo3_btn.Size = new System.Drawing.Size(185, 61);
            this.start_Demo3_btn.TabIndex = 3;
            this.start_Demo3_btn.Text = "Démarrer la démo exercice 3";
            this.start_Demo3_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.start_Demo3_btn.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(275, 215);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(0, 0);
            this.textBox2.TabIndex = 5;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.start_Demo3_btn);
            this.Controls.Add(this.start_Demo2_btn);
            this.Controls.Add(this.start_Demo1_btn);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start_Demo1_btn;
        private System.Windows.Forms.Button start_Demo2_btn;
        private System.Windows.Forms.Button start_Demo3_btn;
        private System.Windows.Forms.TextBox textBox2;
    }
}