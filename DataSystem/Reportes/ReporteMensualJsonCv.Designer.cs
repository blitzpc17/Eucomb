namespace DataSystem.Reportes
{
    partial class ReporteMensualJsonCv
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblEstado = new System.Windows.Forms.Label();
            this.gbxComparacion = new System.Windows.Forms.GroupBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tsErrores = new System.Windows.Forms.ToolStripLabel();
            this.dgvErrores = new System.Windows.Forms.DataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.gbxFacturacion = new System.Windows.Forms.GroupBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsManuales = new System.Windows.Forms.ToolStripLabel();
            this.dgvManuales = new System.Windows.Forms.DataGridView();
            this.chkTodo = new System.Windows.Forms.CheckBox();
            this.gbxCv = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsTotalRegistros = new System.Windows.Forms.ToolStripLabel();
            this.dgvRegistrosDiario = new System.Windows.Forms.DataGridView();
            this.btnImportarLayout = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnComparar = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRfcProveedor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtRfcContrib = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSucursal = new System.Windows.Forms.TextBox();
            this.txtRfcRepresentante = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPeriodo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNoPermiso = new System.Windows.Forms.TextBox();
            this.txtCaracter = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtModPermiso = new System.Windows.Forms.TextBox();
            this.panelInventarios = new System.Windows.Forms.Panel();
            this.btnXmlJson = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbxComparacion.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).BeginInit();
            this.gbxFacturacion.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvManuales)).BeginInit();
            this.gbxCv.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistrosDiario)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(11, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1145, 636);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.lblEstado);
            this.tabPage1.Controls.Add(this.gbxComparacion);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.gbxFacturacion);
            this.tabPage1.Controls.Add(this.chkTodo);
            this.tabPage1.Controls.Add(this.gbxCv);
            this.tabPage1.Controls.Add(this.btnImportarLayout);
            this.tabPage1.Controls.Add(this.btnCancelar);
            this.tabPage1.Controls.Add(this.btnComparar);
            this.tabPage1.Controls.Add(this.btnExportar);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1137, 610);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Archivo";
            // 
            // lblEstado
            // 
            this.lblEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEstado.Location = new System.Drawing.Point(387, 573);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(142, 23);
            this.lblEstado.TabIndex = 42;
            this.lblEstado.Text = "¡Listo!";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbxComparacion
            // 
            this.gbxComparacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxComparacion.Controls.Add(this.toolStrip3);
            this.gbxComparacion.Controls.Add(this.dgvErrores);
            this.gbxComparacion.Location = new System.Drawing.Point(779, 20);
            this.gbxComparacion.Name = "gbxComparacion";
            this.gbxComparacion.Size = new System.Drawing.Size(343, 540);
            this.gbxComparacion.TabIndex = 7;
            this.gbxComparacion.TabStop = false;
            this.gbxComparacion.Text = "Resultado Comparación";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.tsErrores});
            this.toolStrip3.Location = new System.Drawing.Point(3, 512);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(337, 25);
            this.toolStrip3.TabIndex = 1;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(99, 22);
            this.toolStripLabel4.Text = "Total de registros:";
            // 
            // tsErrores
            // 
            this.tsErrores.Name = "tsErrores";
            this.tsErrores.Size = new System.Drawing.Size(13, 22);
            this.tsErrores.Text = "0";
            // 
            // dgvErrores
            // 
            this.dgvErrores.AllowUserToAddRows = false;
            this.dgvErrores.AllowUserToDeleteRows = false;
            this.dgvErrores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvErrores.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvErrores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErrores.Location = new System.Drawing.Point(6, 20);
            this.dgvErrores.Name = "dgvErrores";
            this.dgvErrores.ReadOnly = true;
            this.dgvErrores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvErrores.Size = new System.Drawing.Size(331, 489);
            this.dgvErrores.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(535, 573);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(127, 23);
            this.progressBar1.TabIndex = 41;
            // 
            // gbxFacturacion
            // 
            this.gbxFacturacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbxFacturacion.Controls.Add(this.toolStrip2);
            this.gbxFacturacion.Controls.Add(this.dgvManuales);
            this.gbxFacturacion.Location = new System.Drawing.Point(394, 20);
            this.gbxFacturacion.Name = "gbxFacturacion";
            this.gbxFacturacion.Size = new System.Drawing.Size(343, 543);
            this.gbxFacturacion.TabIndex = 6;
            this.gbxFacturacion.TabStop = false;
            this.gbxFacturacion.Text = "Contenido Facturación";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tsManuales});
            this.toolStrip2.Location = new System.Drawing.Point(3, 515);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(337, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(99, 22);
            this.toolStripLabel2.Text = "Total de registros:";
            // 
            // tsManuales
            // 
            this.tsManuales.Name = "tsManuales";
            this.tsManuales.Size = new System.Drawing.Size(13, 22);
            this.tsManuales.Text = "0";
            // 
            // dgvManuales
            // 
            this.dgvManuales.AllowUserToAddRows = false;
            this.dgvManuales.AllowUserToDeleteRows = false;
            this.dgvManuales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvManuales.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvManuales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvManuales.Location = new System.Drawing.Point(7, 20);
            this.dgvManuales.Name = "dgvManuales";
            this.dgvManuales.ReadOnly = true;
            this.dgvManuales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvManuales.Size = new System.Drawing.Size(330, 492);
            this.dgvManuales.TabIndex = 0;
            // 
            // chkTodo
            // 
            this.chkTodo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTodo.AutoSize = true;
            this.chkTodo.Location = new System.Drawing.Point(668, 579);
            this.chkTodo.Name = "chkTodo";
            this.chkTodo.Size = new System.Drawing.Size(66, 17);
            this.chkTodo.TabIndex = 40;
            this.chkTodo.Text = "Ver todo";
            this.chkTodo.UseVisualStyleBackColor = true;
            // 
            // gbxCv
            // 
            this.gbxCv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbxCv.Controls.Add(this.toolStrip1);
            this.gbxCv.Controls.Add(this.dgvRegistrosDiario);
            this.gbxCv.Location = new System.Drawing.Point(9, 20);
            this.gbxCv.Name = "gbxCv";
            this.gbxCv.Size = new System.Drawing.Size(343, 543);
            this.gbxCv.TabIndex = 3;
            this.gbxCv.TabStop = false;
            this.gbxCv.Text = "Contenido Control Volumetrico";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsTotalRegistros});
            this.toolStrip1.Location = new System.Drawing.Point(3, 515);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(337, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(99, 22);
            this.toolStripLabel1.Text = "Total de registros:";
            // 
            // tsTotalRegistros
            // 
            this.tsTotalRegistros.Name = "tsTotalRegistros";
            this.tsTotalRegistros.Size = new System.Drawing.Size(13, 22);
            this.tsTotalRegistros.Text = "0";
            // 
            // dgvRegistrosDiario
            // 
            this.dgvRegistrosDiario.AllowUserToAddRows = false;
            this.dgvRegistrosDiario.AllowUserToDeleteRows = false;
            this.dgvRegistrosDiario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRegistrosDiario.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvRegistrosDiario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegistrosDiario.Location = new System.Drawing.Point(7, 20);
            this.dgvRegistrosDiario.Name = "dgvRegistrosDiario";
            this.dgvRegistrosDiario.ReadOnly = true;
            this.dgvRegistrosDiario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRegistrosDiario.Size = new System.Drawing.Size(330, 492);
            this.dgvRegistrosDiario.TabIndex = 0;
            // 
            // btnImportarLayout
            // 
            this.btnImportarLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportarLayout.Location = new System.Drawing.Point(12, 569);
            this.btnImportarLayout.Name = "btnImportarLayout";
            this.btnImportarLayout.Size = new System.Drawing.Size(110, 35);
            this.btnImportarLayout.TabIndex = 4;
            this.btnImportarLayout.Text = "Importar Excel";
            this.btnImportarLayout.UseVisualStyleBackColor = true;
            this.btnImportarLayout.Click += new System.EventHandler(this.btnImportarLayout_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(777, 569);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 35);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnComparar
            // 
            this.btnComparar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComparar.Location = new System.Drawing.Point(1009, 569);
            this.btnComparar.Name = "btnComparar";
            this.btnComparar.Size = new System.Drawing.Size(110, 35);
            this.btnComparar.TabIndex = 5;
            this.btnComparar.Text = "Comparar";
            this.btnComparar.UseVisualStyleBackColor = true;
            this.btnComparar.Click += new System.EventHandler(this.btnComparar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.Location = new System.Drawing.Point(893, 569);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(110, 35);
            this.btnExportar.TabIndex = 8;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.panelInventarios);
            this.tabPage2.Controls.Add(this.btnXmlJson);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1137, 610);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Inventarios";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtRfcProveedor);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtVersion);
            this.groupBox1.Controls.Add(this.txtRfcContrib);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtSucursal);
            this.groupBox1.Controls.Add(this.txtRfcRepresentante);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPeriodo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNoPermiso);
            this.groupBox1.Controls.Add(this.txtCaracter);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtModPermiso);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1131, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Encabezado";
            // 
            // txtRfcProveedor
            // 
            this.txtRfcProveedor.Location = new System.Drawing.Point(511, 34);
            this.txtRfcProveedor.Name = "txtRfcProveedor";
            this.txtRfcProveedor.ReadOnly = true;
            this.txtRfcProveedor.Size = new System.Drawing.Size(225, 20);
            this.txtRfcProveedor.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 23);
            this.label1.TabIndex = 20;
            this.label1.Text = "Versión;";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 23);
            this.label2.TabIndex = 21;
            this.label2.Text = "RFC Contrib.:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(126, 63);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(225, 20);
            this.txtVersion.TabIndex = 22;
            // 
            // txtRfcContrib
            // 
            this.txtRfcContrib.Location = new System.Drawing.Point(126, 92);
            this.txtRfcContrib.Name = "txtRfcContrib";
            this.txtRfcContrib.ReadOnly = true;
            this.txtRfcContrib.Size = new System.Drawing.Size(225, 20);
            this.txtRfcContrib.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(390, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 23);
            this.label3.TabIndex = 24;
            this.label3.Text = "RFC Repre.:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSucursal
            // 
            this.txtSucursal.Location = new System.Drawing.Point(126, 34);
            this.txtSucursal.Name = "txtSucursal";
            this.txtSucursal.ReadOnly = true;
            this.txtSucursal.Size = new System.Drawing.Size(225, 20);
            this.txtSucursal.TabIndex = 37;
            // 
            // txtRfcRepresentante
            // 
            this.txtRfcRepresentante.Location = new System.Drawing.Point(511, 63);
            this.txtRfcRepresentante.Name = "txtRfcRepresentante";
            this.txtRfcRepresentante.ReadOnly = true;
            this.txtRfcRepresentante.Size = new System.Drawing.Size(225, 20);
            this.txtRfcRepresentante.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(5, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 23);
            this.label7.TabIndex = 36;
            this.label7.Text = "Sucursal;";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(390, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 23);
            this.label6.TabIndex = 26;
            this.label6.Text = "RFC Proveedor;";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPeriodo
            // 
            this.txtPeriodo.Location = new System.Drawing.Point(511, 92);
            this.txtPeriodo.Name = "txtPeriodo";
            this.txtPeriodo.ReadOnly = true;
            this.txtPeriodo.Size = new System.Drawing.Size(225, 20);
            this.txtPeriodo.TabIndex = 35;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(775, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 23);
            this.label5.TabIndex = 27;
            this.label5.Text = "Caracter.:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNoPermiso
            // 
            this.txtNoPermiso.Location = new System.Drawing.Point(896, 92);
            this.txtNoPermiso.Name = "txtNoPermiso";
            this.txtNoPermiso.ReadOnly = true;
            this.txtNoPermiso.Size = new System.Drawing.Size(225, 20);
            this.txtNoPermiso.TabIndex = 34;
            // 
            // txtCaracter
            // 
            this.txtCaracter.Location = new System.Drawing.Point(896, 34);
            this.txtCaracter.Name = "txtCaracter";
            this.txtCaracter.ReadOnly = true;
            this.txtCaracter.Size = new System.Drawing.Size(225, 20);
            this.txtCaracter.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(390, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 23);
            this.label8.TabIndex = 33;
            this.label8.Text = "Periodo:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(775, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 23);
            this.label4.TabIndex = 30;
            this.label4.Text = "Mod. Permiso:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(775, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 23);
            this.label9.TabIndex = 32;
            this.label9.Text = "No. Permiso:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtModPermiso
            // 
            this.txtModPermiso.Location = new System.Drawing.Point(896, 63);
            this.txtModPermiso.Name = "txtModPermiso";
            this.txtModPermiso.ReadOnly = true;
            this.txtModPermiso.Size = new System.Drawing.Size(225, 20);
            this.txtModPermiso.TabIndex = 31;
            // 
            // panelInventarios
            // 
            this.panelInventarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInventarios.AutoScroll = true;
            this.panelInventarios.Location = new System.Drawing.Point(6, 153);
            this.panelInventarios.Name = "panelInventarios";
            this.panelInventarios.Size = new System.Drawing.Size(1111, 410);
            this.panelInventarios.TabIndex = 2;
            // 
            // btnXmlJson
            // 
            this.btnXmlJson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXmlJson.Location = new System.Drawing.Point(6, 569);
            this.btnXmlJson.Name = "btnXmlJson";
            this.btnXmlJson.Size = new System.Drawing.Size(110, 35);
            this.btnXmlJson.TabIndex = 11;
            this.btnXmlJson.Text = "Importar JSON";
            this.btnXmlJson.UseVisualStyleBackColor = true;
            this.btnXmlJson.Click += new System.EventHandler(this.btnXmlJson_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // ReporteMensualJsonCv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 660);
            this.Controls.Add(this.tabControl1);
            this.Name = "ReporteMensualJsonCv";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte Mensual Json";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.gbxComparacion.ResumeLayout(false);
            this.gbxComparacion.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).EndInit();
            this.gbxFacturacion.ResumeLayout(false);
            this.gbxFacturacion.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvManuales)).EndInit();
            this.gbxCv.ResumeLayout(false);
            this.gbxCv.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistrosDiario)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.GroupBox gbxComparacion;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel tsErrores;
        private System.Windows.Forms.DataGridView dgvErrores;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox gbxFacturacion;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel tsManuales;
        private System.Windows.Forms.DataGridView dgvManuales;
        private System.Windows.Forms.CheckBox chkTodo;
        private System.Windows.Forms.GroupBox gbxCv;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel tsTotalRegistros;
        private System.Windows.Forms.DataGridView dgvRegistrosDiario;
        private System.Windows.Forms.Button btnImportarLayout;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnComparar;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRfcProveedor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtRfcContrib;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSucursal;
        private System.Windows.Forms.TextBox txtRfcRepresentante;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPeriodo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNoPermiso;
        private System.Windows.Forms.TextBox txtCaracter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtModPermiso;
        private System.Windows.Forms.Panel panelInventarios;
        private System.Windows.Forms.Button btnXmlJson;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}