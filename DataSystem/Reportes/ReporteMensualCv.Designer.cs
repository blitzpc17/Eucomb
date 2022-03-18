namespace DataSystem.Reportes
{
    partial class ReporteMensualCv
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsTotalRegistros = new System.Windows.Forms.ToolStripLabel();
            this.dgvRegistrosDiario = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbJson = new System.Windows.Forms.RadioButton();
            this.rbXml = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCargarArchivo = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPeriodo = new System.Windows.Forms.TextBox();
            this.txtNoPermiso = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtModPermiso = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCaracter = new System.Windows.Forms.TextBox();
            this.txtRfcProveedor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRfcRepresentante = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRfcContrib = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImportarLayout = new System.Windows.Forms.Button();
            this.btnComparar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsManuales = new System.Windows.Forms.ToolStripLabel();
            this.dgvManuales = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tsErrores = new System.Windows.Forms.ToolStripLabel();
            this.dgvErrores = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistrosDiario)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvManuales)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Controls.Add(this.dgvRegistrosDiario);
            this.groupBox2.Location = new System.Drawing.Point(12, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 359);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Registros Sistema";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsTotalRegistros});
            this.toolStrip1.Location = new System.Drawing.Point(3, 331);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(366, 25);
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
            this.dgvRegistrosDiario.Size = new System.Drawing.Size(359, 308);
            this.dgvRegistrosDiario.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btnCargarArchivo);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtPeriodo);
            this.groupBox1.Controls.Add(this.txtNoPermiso);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtModPermiso);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCaracter);
            this.groupBox1.Controls.Add(this.txtRfcProveedor);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtRfcRepresentante);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtRfcContrib);
            this.groupBox1.Controls.Add(this.txtVersion);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1239, 196);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Encabezado";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbJson);
            this.panel1.Controls.Add(this.rbXml);
            this.panel1.Location = new System.Drawing.Point(888, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 24);
            this.panel1.TabIndex = 23;
            // 
            // rbJson
            // 
            this.rbJson.AutoSize = true;
            this.rbJson.Location = new System.Drawing.Point(169, 3);
            this.rbJson.Name = "rbJson";
            this.rbJson.Size = new System.Drawing.Size(53, 17);
            this.rbJson.TabIndex = 1;
            this.rbJson.Text = "JSON";
            this.rbJson.UseVisualStyleBackColor = true;
            // 
            // rbXml
            // 
            this.rbXml.AutoSize = true;
            this.rbXml.Checked = true;
            this.rbXml.Location = new System.Drawing.Point(3, 3);
            this.rbXml.Name = "rbXml";
            this.rbXml.Size = new System.Drawing.Size(47, 17);
            this.rbXml.TabIndex = 0;
            this.rbXml.TabStop = true;
            this.rbXml.Text = "XML";
            this.rbXml.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(767, 61);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 23);
            this.label11.TabIndex = 22;
            this.label11.Text = "Tipo Archivo:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCargarArchivo
            // 
            this.btnCargarArchivo.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnCargarArchivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargarArchivo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCargarArchivo.Image = global::DataSystem.Properties.Resources.carpeta__5_;
            this.btnCargarArchivo.Location = new System.Drawing.Point(1082, 88);
            this.btnCargarArchivo.Name = "btnCargarArchivo";
            this.btnCargarArchivo.Size = new System.Drawing.Size(31, 22);
            this.btnCargarArchivo.TabIndex = 21;
            this.btnCargarArchivo.UseVisualStyleBackColor = false;
            this.btnCargarArchivo.Click += new System.EventHandler(this.btnCargarArchivo_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(888, 90);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(188, 20);
            this.textBox2.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(767, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 23);
            this.label10.TabIndex = 19;
            this.label10.Text = "Archivo:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(147, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(225, 20);
            this.textBox1.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(26, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 23);
            this.label7.TabIndex = 17;
            this.label7.Text = "Sucursal;";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPeriodo
            // 
            this.txtPeriodo.Location = new System.Drawing.Point(888, 30);
            this.txtPeriodo.Name = "txtPeriodo";
            this.txtPeriodo.ReadOnly = true;
            this.txtPeriodo.Size = new System.Drawing.Size(225, 20);
            this.txtPeriodo.TabIndex = 16;
            // 
            // txtNoPermiso
            // 
            this.txtNoPermiso.Location = new System.Drawing.Point(516, 123);
            this.txtNoPermiso.Name = "txtNoPermiso";
            this.txtNoPermiso.ReadOnly = true;
            this.txtNoPermiso.Size = new System.Drawing.Size(225, 20);
            this.txtNoPermiso.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(767, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 23);
            this.label8.TabIndex = 14;
            this.label8.Text = "Periodo:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(395, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 23);
            this.label9.TabIndex = 13;
            this.label9.Text = "No. Permiso:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtModPermiso
            // 
            this.txtModPermiso.Location = new System.Drawing.Point(516, 90);
            this.txtModPermiso.Name = "txtModPermiso";
            this.txtModPermiso.ReadOnly = true;
            this.txtModPermiso.Size = new System.Drawing.Size(225, 20);
            this.txtModPermiso.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(395, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 23);
            this.label4.TabIndex = 11;
            this.label4.Text = "Mod. Permiso:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCaracter
            // 
            this.txtCaracter.Location = new System.Drawing.Point(516, 60);
            this.txtCaracter.Name = "txtCaracter";
            this.txtCaracter.ReadOnly = true;
            this.txtCaracter.Size = new System.Drawing.Size(225, 20);
            this.txtCaracter.TabIndex = 10;
            // 
            // txtRfcProveedor
            // 
            this.txtRfcProveedor.Location = new System.Drawing.Point(516, 30);
            this.txtRfcProveedor.Name = "txtRfcProveedor";
            this.txtRfcProveedor.ReadOnly = true;
            this.txtRfcProveedor.Size = new System.Drawing.Size(225, 20);
            this.txtRfcProveedor.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(395, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Caracter.:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(395, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 23);
            this.label6.TabIndex = 7;
            this.label6.Text = "RFC Proveedor;";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRfcRepresentante
            // 
            this.txtRfcRepresentante.Location = new System.Drawing.Point(147, 123);
            this.txtRfcRepresentante.Name = "txtRfcRepresentante";
            this.txtRfcRepresentante.ReadOnly = true;
            this.txtRfcRepresentante.Size = new System.Drawing.Size(225, 20);
            this.txtRfcRepresentante.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "RFC Repre.:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRfcContrib
            // 
            this.txtRfcContrib.Location = new System.Drawing.Point(147, 93);
            this.txtRfcContrib.Name = "txtRfcContrib";
            this.txtRfcContrib.ReadOnly = true;
            this.txtRfcContrib.Size = new System.Drawing.Size(225, 20);
            this.txtRfcContrib.TabIndex = 4;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(147, 63);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(225, 20);
            this.txtVersion.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "RFC Contrib.:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Versión;";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnImportarLayout
            // 
            this.btnImportarLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportarLayout.Location = new System.Drawing.Point(15, 590);
            this.btnImportarLayout.Name = "btnImportarLayout";
            this.btnImportarLayout.Size = new System.Drawing.Size(110, 35);
            this.btnImportarLayout.TabIndex = 4;
            this.btnImportarLayout.Text = "Importar Excel";
            this.btnImportarLayout.UseVisualStyleBackColor = true;
            this.btnImportarLayout.Click += new System.EventHandler(this.btnImportarLayout_Click);
            // 
            // btnComparar
            // 
            this.btnComparar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComparar.Location = new System.Drawing.Point(1141, 590);
            this.btnComparar.Name = "btnComparar";
            this.btnComparar.Size = new System.Drawing.Size(110, 35);
            this.btnComparar.TabIndex = 5;
            this.btnComparar.Text = "Comparar";
            this.btnComparar.UseVisualStyleBackColor = true;
            this.btnComparar.Click += new System.EventHandler(this.btnComparar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.toolStrip2);
            this.groupBox3.Controls.Add(this.dgvManuales);
            this.groupBox3.Location = new System.Drawing.Point(390, 214);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(425, 359);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "RegistrosManuales";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tsManuales});
            this.toolStrip2.Location = new System.Drawing.Point(3, 331);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(419, 25);
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
            this.dgvManuales.Size = new System.Drawing.Size(412, 308);
            this.dgvManuales.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.toolStrip3);
            this.groupBox4.Controls.Add(this.dgvErrores);
            this.groupBox4.Location = new System.Drawing.Point(821, 214);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(425, 359);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Resultado Comparacion";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.tsErrores});
            this.toolStrip3.Location = new System.Drawing.Point(3, 331);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(419, 25);
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
            this.dgvErrores.Location = new System.Drawing.Point(7, 20);
            this.dgvErrores.Name = "dgvErrores";
            this.dgvErrores.ReadOnly = true;
            this.dgvErrores.Size = new System.Drawing.Size(412, 308);
            this.dgvErrores.TabIndex = 0;
            // 
            // ReporteMensualCv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 646);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnComparar);
            this.Controls.Add(this.btnImportarLayout);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ReporteMensualCv";
            this.Text = "ReporteMensualCv";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistrosDiario)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvManuales)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel tsTotalRegistros;
        private System.Windows.Forms.DataGridView dgvRegistrosDiario;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbJson;
        private System.Windows.Forms.RadioButton rbXml;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCargarArchivo;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPeriodo;
        private System.Windows.Forms.TextBox txtNoPermiso;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtModPermiso;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCaracter;
        private System.Windows.Forms.TextBox txtRfcProveedor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRfcRepresentante;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRfcContrib;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImportarLayout;
        private System.Windows.Forms.Button btnComparar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel tsManuales;
        private System.Windows.Forms.DataGridView dgvManuales;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel tsErrores;
        private System.Windows.Forms.DataGridView dgvErrores;
    }
}