namespace MathProjet
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.start_App_btn = new System.Windows.Forms.Button();
            this.listBoxPoints = new System.Windows.Forms.ListBox();
            this.listBoxPentes = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // start_App_btn
            // 
            this.start_App_btn.Location = new System.Drawing.Point(1333, 12);
            this.start_App_btn.Name = "start_App_btn";
            this.start_App_btn.Size = new System.Drawing.Size(130, 23);
            this.start_App_btn.TabIndex = 0;
            this.start_App_btn.Text = "Démarrer l\'application";
            this.start_App_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.start_App_btn.UseVisualStyleBackColor = true;
            this.start_App_btn.Click += new System.EventHandler(this.start_App_btn_Click);
            // 
            // listBoxPoints
            // 
            this.listBoxPoints.FormattingEnabled = true;
            this.listBoxPoints.Location = new System.Drawing.Point(1066, 12);
            this.listBoxPoints.Name = "listBoxPoints";
            this.listBoxPoints.Size = new System.Drawing.Size(120, 95);
            this.listBoxPoints.TabIndex = 1;
            // 
            // listBoxPentes
            // 
            this.listBoxPentes.FormattingEnabled = true;
            this.listBoxPentes.Location = new System.Drawing.Point(431, 12);
            this.listBoxPentes.Name = "listBoxPentes";
            this.listBoxPentes.Size = new System.Drawing.Size(606, 95);
            this.listBoxPentes.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.listBoxPentes);
            this.Controls.Add(this.listBoxPoints);
            this.Controls.Add(this.start_App_btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button start_App_btn;
        private System.Windows.Forms.ListBox listBoxPoints;
        private System.Windows.Forms.ListBox listBoxPentes;
    }
}

