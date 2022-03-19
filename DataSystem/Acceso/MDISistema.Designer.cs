namespace DataSystem.Acceso
{
    partial class MDISistema
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDISistema));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.sistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteríaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesCvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteMensualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDiarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sistemaToolStripMenuItem,
            this.reporteríaToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1246, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // sistemaToolStripMenuItem
            // 
            this.sistemaToolStripMenuItem.Name = "sistemaToolStripMenuItem";
            this.sistemaToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.sistemaToolStripMenuItem.Text = "Sistema";
            // 
            // reporteríaToolStripMenuItem
            // 
            this.reporteríaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportesCvToolStripMenuItem});
            this.reporteríaToolStripMenuItem.Name = "reporteríaToolStripMenuItem";
            this.reporteríaToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.reporteríaToolStripMenuItem.Text = "Reportería";
            // 
            // reportesCvToolStripMenuItem
            // 
            this.reportesCvToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reporteMensualToolStripMenuItem,
            this.reporteDiarioToolStripMenuItem});
            this.reportesCvToolStripMenuItem.Name = "reportesCvToolStripMenuItem";
            this.reportesCvToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.reportesCvToolStripMenuItem.Text = "Reportes Cv";
            // 
            // reporteMensualToolStripMenuItem
            // 
            this.reporteMensualToolStripMenuItem.Name = "reporteMensualToolStripMenuItem";
            this.reporteMensualToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.reporteMensualToolStripMenuItem.Text = "Reporte Mensual";
            this.reporteMensualToolStripMenuItem.Click += new System.EventHandler(this.reporteMensualToolStripMenuItem_Click);
            // 
            // reporteDiarioToolStripMenuItem
            // 
            this.reporteDiarioToolStripMenuItem.Name = "reporteDiarioToolStripMenuItem";
            this.reporteDiarioToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.reporteDiarioToolStripMenuItem.Text = "Reporte Diario";
            this.reporteDiarioToolStripMenuItem.Click += new System.EventHandler(this.reporteDiarioToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 581);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1246, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // MDISistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 603);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MDISistema";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EUCOMB GASOLINERAS MÉXICO";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem sistemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteríaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesCvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteMensualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDiarioToolStripMenuItem;
    }
}



