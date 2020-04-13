namespace Biblioteka
{
    partial class LibraryForm
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
            this.dodajWidokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.childViewsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodajRekordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dodajWidokToolStripMenuItem
            // 
            this.dodajWidokToolStripMenuItem.Name = "dodajWidokToolStripMenuItem";
            this.dodajWidokToolStripMenuItem.Size = new System.Drawing.Size(107, 24);
            this.dodajWidokToolStripMenuItem.Text = "Włącz widok";
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.childViewsMenuItem,
            this.dodajWidokToolStripMenuItem,
            this.dodajRekordToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.childViewsMenuItem;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "Menu";
            // 
            // childViewsMenuItem
            // 
            this.childViewsMenuItem.Name = "childViewsMenuItem";
            this.childViewsMenuItem.Size = new System.Drawing.Size(127, 24);
            this.childViewsMenuItem.Text = "Aktywne widoki";
            // 
            // dodajRekordToolStripMenuItem
            // 
            this.dodajRekordToolStripMenuItem.Name = "dodajRekordToolStripMenuItem";
            this.dodajRekordToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.dodajRekordToolStripMenuItem.Text = "Dodaj rekord";
            // 
            // LibraryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip);
            this.Name = "LibraryForm";
            this.Text = "LibraryForm";
            this.Load += new System.EventHandler(this.LibraryForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem childViewsMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem dodajWidokToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodajRekordToolStripMenuItem;
    }
}