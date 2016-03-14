namespace Pathfinding {
    partial class Form1 {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.FindWayButton = new System.Windows.Forms.Button();
            this.MapPanel = new System.Windows.Forms.Panel();
            this.resetButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // FindWayButton
            // 
            this.FindWayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FindWayButton.Location = new System.Drawing.Point(612, 567);
            this.FindWayButton.Name = "FindWayButton";
            this.FindWayButton.Size = new System.Drawing.Size(75, 23);
            this.FindWayButton.TabIndex = 0;
            this.FindWayButton.Text = "Find Way";
            this.FindWayButton.UseVisualStyleBackColor = true;
            this.FindWayButton.Click += new System.EventHandler(this.FindWayButton_Click);
            // 
            // MapPanel
            // 
            this.MapPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MapPanel.AutoScroll = true;
            this.MapPanel.BackColor = System.Drawing.Color.White;
            this.MapPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MapPanel.Location = new System.Drawing.Point(12, 12);
            this.MapPanel.Name = "MapPanel";
            this.MapPanel.Size = new System.Drawing.Size(675, 549);
            this.MapPanel.TabIndex = 1;
            this.MapPanel.Visible = false;
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetButton.Location = new System.Drawing.Point(531, 567);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 2;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(9, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(674, 548);
            this.panel2.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 602);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.MapPanel);
            this.Controls.Add(this.FindWayButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button FindWayButton;
        private System.Windows.Forms.Panel MapPanel;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Panel panel2;
    }
}

