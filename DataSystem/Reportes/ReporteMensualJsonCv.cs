using DataSystem.Utilidades;
using Newtonsoft.Json;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using rootJsonMensualT = Entidades.JSON.Mensual.rootJsonMensual;
using ExVentaPorFecha = Entidades.JSON.Mensual.VentaPorFecha;
using clsJsonMensualT = Entidades.JSON.Mensual.clsJsonMensual;

using clsResultadoMensual = Entidades.cls.clsResultadosMensual;

using DataSystem.Recursos;
using Entidades.cls;

namespace DataSystem.Reportes
{
    public partial class ReporteMensualJsonCv : Form
    {

        private int sucursal = 0;
        private List<ExVentaPorFecha> LstExcel;
        private List<ExVentaPorFecha> LstExcelAux;
        private List<clsJsonMensualT> ListaJson;
        private List<clsJsonMensualT> ListaJsonAux;

        private List<KeyValuePair<string, string>> LstCfdisErrores;
        
        private string argumentoBackground = "";
        private string rutaExcel = "";

        //resultados
        List<clsResultadosMensual> LstResultados;

        public ReporteMensualJsonCv(string tituloModulo, Enumeraciones.Sucursales sucursal)
        {
            InitializeComponent();
            Text = tituloModulo;
            this.sucursal = (int)sucursal;
        }




        private void LeerExcel(string ruta)
        {

            // Obtener la ruta del archivo seleccionado
            string rutaExcel = ruta;

            // Abrir el archivo Excel para obtener las hojas
            using (SLDocument sl = new SLDocument(rutaExcel))
            {
                // Obtener la lista de nombres de las hojas
                List<string> hojas = sl.GetSheetNames();

                // Mostrar las hojas al usuario
                Console.WriteLine("Hojas disponibles:");
                for (int i = 0; i < hojas.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {hojas[i]}");
                }

                int indiceHoja = 0;

                if (indiceHoja >= 0 && indiceHoja < hojas.Count)
                {
                    // Seleccionar la hoja por su índice
                    sl.SelectWorksheet(hojas[indiceHoja]);

                    // Fila desde la cual comenzar a leer (ejemplo: fila 9)
                    int rowInicio = 9;

                    // Obtener el número de columnas
                    int colFinal = sl.GetWorksheetStatistics().EndColumnIndex;
                    int rowFinal = sl.GetWorksheetStatistics().EndRowIndex;

                    LstExcel = new List<ExVentaPorFecha>();


                    //recorrer rows

                    for (int row = rowInicio; row <= rowFinal; row++)
                    {
                        ExVentaPorFecha ObjRegistro = new ExVentaPorFecha();

                        // Leer celda por celda, columna por columna, a partir de la fila especificada
                        for (int col = 1; col <= colFinal; col++)
                        {
                            Console.WriteLine(col);
                            //en la col 8 se va a valdiar si tiene encabezado
                            string valor = sl.GetCellValueAsString(8, col);
                            if (string.IsNullOrEmpty(valor)) continue;


                            switch (valor)
                            {
                                case "Codigo Cliente":
                                    ObjRegistro.CodigoCliente = sl.GetCellValueAsString(row, col).PadLeft(4, '0');
                                    break;
                                case "Cliente":
                                    ObjRegistro.Cliente = sl.GetCellValueAsString(row, col);
                                    break;
                                case "Cred":
                                    ObjRegistro.Credito = sl.GetCellValueAsDecimal(row, col);
                                    break;
                                case "Id Fac":
                                    ObjRegistro.IdFactura = sl.GetCellValueAsInt32(row, col);
                                    break;
                                case "Factura":
                                    ObjRegistro.NumeroFactura = sl.GetCellValueAsString(row, col);
                                    break;
                                case "Fecha Venta":
                                    ObjRegistro.FechaVenta = sl.GetCellValueAsDateTime(row, col);
                                    break;
                                case "Fecha Factura":
                                    ObjRegistro.FechaFactura = sl.GetCellValueAsDateTime(row, col);
                                    break;
                                case "Fecha Timbrado":
                                    ObjRegistro.FechaTimbrado = sl.GetCellValueAsDateTime(row, col);
                                    break;
                                case "UUID":
                                    ObjRegistro.UUID = sl.GetCellValueAsString(row, col);
                                    break;
                                case "Venta":
                                    ObjRegistro.Venta = sl.GetCellValueAsDecimal(row, col);
                                    break;
                                case "Tipo Venta":
                                    ObjRegistro.TipoVenta = sl.GetCellValueAsString(row, col);
                                    break;
                                case "Tipo Factura":
                                    ObjRegistro.TipoFactura = sl.GetCellValueAsString(row, col);
                                    break;
                                case "Producto":
                                    ObjRegistro.Producto = sl.GetCellValueAsString(row, col);
                                    break;
                                case "Cantidad":
                                    ObjRegistro.Cantidad = RedondearCantidad(sl.GetCellValueAsDecimal(row, col));
                                    break;
                                case "IVA Factura":
                                    ObjRegistro.IvaFactura = sl.GetCellValueAsDecimal(row, col);
                                    break;
                                case "Total Factura":
                                    ObjRegistro.TotalFactura = sl.GetCellValueAsDecimal(row, col);
                                    break;
                                case "SubTotal S/I Venta":
                                    ObjRegistro.SubTotalSinIva = sl.GetCellValueAsDecimal(row, col);
                                    break;
                                case "SubTotalVenta":
                                    ObjRegistro.SubTotalVenta = sl.GetCellValueAsDecimal(row, col);
                                    break;
                                case "IVA Venta":
                                    ObjRegistro.IvaVenta = sl.GetCellValueAsDecimal(row, col);
                                    break;
                                case "IEPS Venta":
                                    ObjRegistro.IepsVenta = sl.GetCellValueAsDecimal(row, col);
                                    break;
                                case "Total Venta":
                                    ObjRegistro.TotalVenta = sl.GetCellValueAsDecimal(row, col);
                                    break;
                            }

                        }

                        LstExcel.Add(ObjRegistro);
                        LstExcel = LstExcel.Where(x => x.Producto != null && (x.Producto.StartsWith("Gasolina") || x.Producto.StartsWith("GASOLINA") || x.Producto.StartsWith("Diesel") || x.Producto.StartsWith("DIESEL") || x.Producto.StartsWith("COMBUSTIBLE") || x.Producto.StartsWith("Combustible") || x.Producto.StartsWith("combustible"))).ToList();

                    }
              
                }
                else
                {
                    Console.WriteLine("Índice de hoja inválido.");
                }
            } 

        }

        private void btnImportarLayout_Click(object sender, EventArgs e)
        {

            // Crear el OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos Excel (*.xlsx)|*.xlsx|Todos los archivos (*.*)|*.*";
            openFileDialog.Title = "Selecciona un archivo Excel";

            // Mostrar el diálogo y verificar si se seleccionó un archivo
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaExcel = openFileDialog.FileName;
            }
            else
            {
                MessageBox.Show("No se seleccionó ningún archivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            lblEstado.Text = "Importando archivo...";
            backgroundWorker1.RunWorkerAsync("importarExcel");
          
        }

        private void btnXmlJson_Click(object sender, EventArgs e)
        {
            ImportarJson();
        }

        private void ImportarJson()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos JSON (*.json)|*.json|Todos los archivos (*.*)|*.*";
            openFileDialog.Title = "Selecciona un archivo JSON";
            if (openFileDialog.ShowDialog() == DialogResult.OK) 
            { 
                string rutaArchivo = openFileDialog.FileName;
                string contenidoJson = File.ReadAllText(rutaArchivo);
                var facturaDetalle = JsonConvert.DeserializeObject<rootJsonMensualT>(contenidoJson);

                MostrarEnPantalla(facturaDetalle);
                MostrarInventarios(facturaDetalle);
                MessageBox.Show("Archivo JSON cargado y deserializado correctamente.");
            }
        }

        private void MostrarEnPantalla(rootJsonMensualT obj)
        {
            ListaJson = new List<clsJsonMensualT>();
            foreach(var prod in obj.Producto)
            {
                foreach (var complemento in prod.ReporteDeVolumenMensual.Entregas.Complemento)
                {
                   foreach(var entrega in complemento.Nacional)
                   {
                        ListaJson.Add( new clsJsonMensualT
                        {
                            RfcClienteOProveedor = entrega.RfcClienteOProveedor,
                            NombreClienteOProveedor = entrega.NombreClienteOProveedor,
                            CFDI = entrega.CFDIs.First().Cfdi,
                            FechaYHoraTransaccion = entrega.CFDIs.First().FechaYHoraTransaccion,
                            ValorNumerico = RedondearCantidad(entrega.CFDIs.First().VolumenDocumentado.ValorNumerico)
                        });     
                   }
                }
            }

            if (ListaJson.Count >= 1)
            {
                dgvRegistrosDiario.DataSource = ListaJson;
                tsTotalRegistros.Text = ListaJson.Count.ToString("N0");
            }
            
        }

        private void MostrarInventarios(rootJsonMensualT obj)
        {
            txtVersion.Text = obj.Version.ToString("N2");
            txtRfcRepresentante.Text = obj.RfcRepresentanteLegal;
            txtRfcProveedor.Text = obj.RfcProveedor;
            txtRfcContrib.Text = obj.RfcContribuyente;
            txtNoPermiso.Text = obj.NumPermiso;
            txtSucursal.Text = Parametros.CatalogSucursales().Where(x => x.Value == obj.NumPermiso).First().Key;
            txtCaracter.Text = obj.Caracter;
            txtModPermiso.Text = obj.ModalidadPermiso;
            txtPeriodo.Text = obj.FechaYHoraReporteMes.ToString();

            //limpiarPanelInventarios();

            int posicionDgvInventariosY = 0;
            foreach (var pro in obj.Producto.OrderByDescending(x => x.ClaveProducto).ToList())
            {

                DataGridView dgvEncabezadoInventario = new DataGridView();

                dgvEncabezadoInventario.Columns.Add("Texto", "");
                dgvEncabezadoInventario.Columns.Add("Valor", "");
                dgvEncabezadoInventario.Name = "dgvEncabezado" + pro.ClaveProducto;
                dgvEncabezadoInventario.Rows.Add("Producto:", pro.ClaveSubProducto + " " + pro.ClaveProducto);
                dgvEncabezadoInventario.Rows.Add("INVENTARIO EN TANQUE AL FINALIZAR EL MES:", pro.ReporteDeVolumenMensual.ControlDeExistencias.VolumenExistenciasMes.ToString("N2"));
                dgvEncabezadoInventario.Rows.Add("NÚMERO DE VECES QUE ENTRO PRODUCTO AL TANQUE:", pro.ReporteDeVolumenMensual.Recepciones.TotalRecepcionesMes.ToString("N0"));
                dgvEncabezadoInventario.Rows.Add("TOTAL DE LITROS QUE MUESTRA LA FACTURA:", pro.ReporteDeVolumenMensual.Recepciones.SumaVolumenRecepcionMes.ValorNumerico);
                panelInventarios.Controls.Add(dgvEncabezadoInventario);
                dgvEncabezadoInventario.Location = new System.Drawing.Point(10, posicionDgvInventariosY + 30);
                dgvEncabezadoInventario.Width = 1107;
                posicionDgvInventariosY = dgvEncabezadoInventario.Location.Y + dgvEncabezadoInventario.Size.Height + 15;
                dgvEncabezadoInventario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top)))));
                dgvEncabezadoInventario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGridView dgvPartidas = new DataGridView();
                dgvPartidas.Columns.Add("numero", "No.");
                dgvPartidas.Columns.Add("nombreCliente", "Nombre Cliente Proveedor");
                dgvPartidas.Columns.Add("rfcCliente", "Rfc Cliente Proveedor");
                dgvPartidas.Columns.Add("cfdi", "CFDI");
                dgvPartidas.Columns.Add("long", "Long.");
                dgvPartidas.Columns.Add("fechaHora", "Fecha y Hora");
                dgvPartidas.Columns.Add("precioCompra", "Precio Compra");
                dgvPartidas.Columns.Add("precioVenta", "Precio Venta Púb.");
                dgvPartidas.Columns.Add("valorNumerico", "ValorNumerico");

                panelInventarios.Controls.Add(dgvPartidas);
                dgvPartidas.Location = new System.Drawing.Point(10, posicionDgvInventariosY + 30);
                dgvPartidas.Width = 1107;
                dgvPartidas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top)))));
                dgvPartidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                //foreach partidas
                int numeral = 1;
                decimal sumaRecepciones = 0;
                if (pro.ReporteDeVolumenMensual.Recepciones.Complemento == null)
                {
                    sumaRecepciones = 0;
                    MessageBox.Show("El producto " + pro.ClaveSubProducto + " " + pro.ClaveProducto + " no tiene COMPLEMENTOS EN RECEPCIONES.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    foreach (var comp in pro.ReporteDeVolumenMensual.Recepciones.Complemento /*REPORTEDEVOLUMENMENSUAL.RECEPCIONES.Complemento.Complemento_Expendio.NACIONAL*/)
                    {
                        foreach(var part in comp.Nacional)
                        {
                            //validar cfdi
                            if (part.CFDIs.First().Cfdi.Length != 36)
                            {
                                LstCfdisErrores.Add(new KeyValuePair<string, string>(part.CFDIs.First().Cfdi, "Error de longitud: " + part.CFDIs.First().Cfdi.Length.ToString("N0")));
                            }
                            //imprimi
                            dgvPartidas.Rows.Add(numeral, part.NombreClienteOProveedor, part.RfcClienteOProveedor, part.CFDIs.First().Cfdi, part.CFDIs.First().Cfdi.Length, part.CFDIs.First().FechaYHoraTransaccion, part.CFDIs.First().PrecioCompra, part.CFDIs.First().PrecioDeVentaAlPublico, part.CFDIs.First().VolumenDocumentado.ValorNumerico );
                            sumaRecepciones += part.CFDIs.First().VolumenDocumentado.ValorNumerico;
                            numeral++;
                        }
                      
                    }
                }


                decimal diferenciaEntregadoRecepcion = pro.ReporteDeVolumenMensual.Recepciones.SumaVolumenRecepcionMes.ValorNumerico - sumaRecepciones;
                dgvPartidas.Rows.Add(null, null, null, null, null, null, "TOTAL:", sumaRecepciones);
                dgvPartidas.Rows.Add(null, null, null, "VENTA LTS. POR MES:", pro.ReporteDeVolumenMensual.Entregas.SumaVolumenEntregadoMes.ValorNumerico, null, null, null);
                dgvPartidas.Rows.Add(null, null, null, "DIF- FACT. VS PIPAS:", diferenciaEntregadoRecepcion, null, null, null);
                dgvPartidas.Rows.Add(null, null, null, "LA FACTURA TRAE", diferenciaEntregadoRecepcion >= 0 ? " MÁS" : " MENOS");
                posicionDgvInventariosY = dgvPartidas.Location.Y + dgvPartidas.Size.Height + 15;

            }
        }

        private void ComparacionAivic()
        {
            // LstControlVolumetricoMensual
            List<Entidades.cls.clsControlVolumetricoMensual> LstCvNoExisten = new List<Entidades.cls.clsControlVolumetricoMensual>();
            //   List<Entidades.AIVIC.EXCEL.FACTURADETALLE> LstFactDetalleNoExisten = new List<Entidades.AIVIC.EXCEL.FACTURADETALLE>();
            LstResultados = new List<Entidades.cls.clsResultadosMensual>();


            //buscar cuales estan en XML y no en AIVIC
            foreach (var objFact in LstExcel)
            {
                string observacion = "";
                var objNe = objFact;
                bool comparaNombre = false;
                bool comparaCfdi = false;
                bool comparaLts = false;
                clsJsonMensualT ObjRegistroCV;

                if (ListaJson.Any(x => x.CFDI == objFact.UUID))
                {
                    ObjRegistroCV = ListaJson.First(x=>x.CFDI== objFact.UUID); // LstControlVolumetricoMensual .First(x => x.CFDI == objFact.UUID);
                    DateTime fechaRegistroCv = DateTime.Parse(ObjRegistroCV.FechaYHoraTransaccion.ToShortDateString());
                    if (fechaRegistroCv != objFact.FechaVenta)
                    {
                        observacion = "EXISTE EN ARCHIVO C.V., PERO LAS FECHAS NO COICIDEN.";
                    }
                    else if (Math.Round(ObjRegistroCV.ValorNumerico, 2) != objFact.Cantidad)
                    {
                        observacion = "EXISTE EN ARCHIVO C.V PERO LAS CANTIDADES NO COINCIDEN";
                        comparaLts = false;
                    }

                }
                else
                {
                    ObjRegistroCV = null;
                    observacion = "NO EXISTE EN ARCHIVO C.V.";
                    comparaCfdi = false;

                }

                if (!string.IsNullOrEmpty(observacion) && ObjRegistroCV == null)
                {
                    //LstFactDetalleNoExisten.Add(objNe);
                    LstResultados.Add(new Entidades.cls.clsResultadosMensual
                    {
                        RfcClienteOProveedor = null,
                        NombreCliente = null,
                        CFDI = null,
                        FechaYHoraTransaccion = null,
                        VolumenNumerico = 0,
                        Existe = ">",
                        folio_Imp = objFact.NumeroFactura,
                        clavecli = "",
                        NombreClienteOPRoveedor = objFact.Cliente,
                        serie = "",
                        docto = "",
                        fecha_reg = objFact.FechaFactura,
                        nombrep = "",
                        Cant = objFact.Cantidad,
                        precio = 0,
                        imported = objFact.TotalVenta,
                        UUID = objFact.UUID,
                        ComparaNombre = comparaNombre,
                        ComparaCfdi = comparaCfdi,
                        ComparaLts = comparaLts,
                        Observacion = observacion,
                        DiferenciaCantidades = objFact.Cantidad
                    });
                }
                else
                {
                    if (ObjRegistroCV == null) return;
                    LstResultados.Add(new Entidades.cls.clsResultadosMensual
                    {
                        RfcClienteOProveedor = ObjRegistroCV.RfcClienteOProveedor,
                        NombreCliente = ObjRegistroCV.NombreClienteOProveedor,
                        CFDI = ObjRegistroCV.CFDI,
                        FechaYHoraTransaccion = ObjRegistroCV.FechaYHoraTransaccion,
                        VolumenNumerico = ObjRegistroCV.ValorNumerico,
                        Existe = "<>",
                        folio_Imp = objFact.UUID,
                        clavecli = "",
                        NombreClienteOPRoveedor = objFact.Cliente,
                        serie = "",
                        docto = "",
                        fecha_reg = objFact.FechaFactura,
                        nombrep = "",
                        Cant = Math.Round(objFact.Cantidad, 2),
                        precio = 0,
                        imported = objFact.TotalVenta,
                        UUID = objFact.UUID,
                        ComparaNombre = comparaNombre,
                        ComparaCfdi = comparaCfdi,
                        ComparaLts = comparaLts,
                        Observacion = observacion,
                        DiferenciaCantidades = (ObjRegistroCV.ValorNumerico - objFact.Cantidad) < 0 ? (ObjRegistroCV.ValorNumerico - objFact.Cantidad) * -1 : (ObjRegistroCV.ValorNumerico - objFact.Cantidad)
                    });
                }


            }

            //buscar cuales no estan del xml  en el excel de AIVIC
            foreach (var objCv in ListaJson)
            {
                string observacion = "";
                var objNe = objCv;
                bool comparaNombre = false;
                bool comparaCfdi = false;
                bool comparaLts = false;
                ExVentaPorFecha ObjRegistroFactura;

                if ( LstExcel.Any(x => x.UUID == objCv.CFDI))
                {
                    ObjRegistroFactura = LstExcel.First(x => x.UUID == objCv.CFDI);
                    DateTime fechaRegistroFactura = DateTime.Parse(objCv.FechaYHoraTransaccion.ToShortDateString());
                    if (ObjRegistroFactura.FechaFactura != fechaRegistroFactura)
                    {
                        observacion = "EXISTE EN FACTURA ATIO PERO LAS FECHAS NO COINCIDEN";

                    }
                    else if (Math.Round(objCv.ValorNumerico, 2) == ObjRegistroFactura.Cantidad)
                    {
                        observacion = "EXISTE EN FACTURA ATIO PERO LAS CANTIDADES NO COINCIDEN";
                        comparaLts = false;
                    }
                }
                else
                {
                    ObjRegistroFactura = null;
                    observacion = "NO EXISTE EN FACTURA ATIO.";
                    comparaCfdi = false;
                }


                if (!string.IsNullOrEmpty(observacion) && ObjRegistroFactura == null)
                {
                    //  LstFactDetalleNoExisten.Add(objNe);
                    LstResultados.Add(new Entidades.cls.clsResultadosMensual
                    {
                        RfcClienteOProveedor = objCv.RfcClienteOProveedor,
                        NombreCliente = objCv.NombreClienteOProveedor,
                        CFDI = objCv.CFDI,
                        FechaYHoraTransaccion = objCv.FechaYHoraTransaccion,
                        VolumenNumerico = 0,
                        Existe = "<",
                        folio_Imp = null,
                        clavecli = null,
                        NombreClienteOPRoveedor = null,
                        serie = null,
                        docto = null,
                        fecha_reg = null,
                        nombrep = null,
                        Cant = 0,
                        precio = 0,
                        imported = 0,
                        UUID = null,
                        ComparaNombre = comparaNombre,
                        ComparaCfdi = comparaCfdi,
                        ComparaLts = comparaLts,
                        Observacion = observacion,
                        DiferenciaCantidades = Math.Round(objCv.ValorNumerico, 2)
                    });
                }
                else
                {
                    if (ObjRegistroFactura == null) return;
                    LstResultados.Add(new Entidades.cls.clsResultadosMensual
                    {
                        RfcClienteOProveedor = objCv.RfcClienteOProveedor,
                        NombreCliente = objCv.NombreClienteOProveedor,
                        CFDI = objCv.CFDI,
                        FechaYHoraTransaccion = objCv.FechaYHoraTransaccion,
                        VolumenNumerico = objCv.ValorNumerico,
                        Existe = "<>",
                        folio_Imp = ObjRegistroFactura.UUID,
                        clavecli = "",
                        NombreClienteOPRoveedor = ObjRegistroFactura.Cliente,
                        serie = "",
                        docto = "",
                        fecha_reg = ObjRegistroFactura.FechaFactura,
                        nombrep = "",
                        Cant = Math.Round(ObjRegistroFactura.Cantidad, 2),
                        precio = 0,
                        imported = ObjRegistroFactura.TotalVenta,
                        UUID = ObjRegistroFactura.UUID,
                        ComparaNombre = comparaNombre,
                        ComparaCfdi = comparaCfdi,
                        ComparaLts = comparaLts,
                        Observacion = observacion,
                        DiferenciaCantidades = (objCv.ValorNumerico - ObjRegistroFactura.Cantidad) < 0 ? (objCv.ValorNumerico - ObjRegistroFactura.Cantidad) * -1 : (objCv.ValorNumerico - ObjRegistroFactura.Cantidad)
                    });
                }

            }


        }

        private decimal RedondearCantidad(decimal valor)
        {
            decimal valorAjustado = Math.Round(valor, 4, MidpointRounding.AwayFromZero);
            decimal cuartoDecimal = (valorAjustado * 10000) % 10;

            if (cuartoDecimal >= 6)
            {
                return Math.Round(valor, 3, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Floor(valor * 1000) / 1000;
            }
        }

        private void btnComparar_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) return;
            if (sucursal == (int)Enumeraciones.Sucursales.AIVIC && (LstExcel == null || ListaJson == null))
            {
                MessageBox.Show("No se han cargado los archivos de comparación (Archivo C.V y Reporte Facturacion AIVIC Mensual).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lblEstado.Text = "Comparando archivos...";
            backgroundWorker1.RunWorkerAsync("comparar");
        }

        private void LimpiarFormulario()
        {
            if (MessageBox.Show("Se perderá la información ingresada. ¿Deseas continuar?", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LimpiarControlesFormulario();
                LstExcel = new List<ExVentaPorFecha>();
                ListaJson = new List<clsJsonMensualT>();
                LstResultados = new List<Entidades.cls.clsResultadosMensual>();
            }
        }

        private void LimpiarControlesFormulario()
        {
            foreach (var cont in this.Controls)
            {
                if (cont is GroupBox)
                {
                    foreach (var gctrl in ((GroupBox)cont).Controls)
                    {
                        if (gctrl is TextBox)
                        {
                            ((TextBox)gctrl).Clear();
                        }
                        else if (gctrl is DataGridView)
                        {
                            ((DataGridView)gctrl).DataSource = null;
                        }
                        else if (gctrl is ToolStrip)
                        {
                            ((ToolStrip)gctrl).Items[1].Text = 0.ToString("N0");
                        }
                    }
                }
                else if (cont is TabControl)
                {
                    foreach (var tabPage in ((TabControl)cont).TabPages)
                    {
                        foreach (var gctrl in ((TabPage)tabPage).Controls)
                        {
                            if (gctrl is Panel)
                            {
                                ((Panel)gctrl).Controls.Clear();
                            }
                            if (gctrl is GroupBox)
                            {
                                foreach (var tgctrl in ((GroupBox)gctrl).Controls)
                                {
                                    if (tgctrl is TextBox)
                                    {
                                        ((TextBox)tgctrl).Clear();
                                    }
                                    else if (tgctrl is DataGridView)
                                    {
                                        ((DataGridView)tgctrl).DataSource = null;
                                    }
                                    else if (tgctrl is ToolStrip)
                                    {
                                        ((ToolStrip)tgctrl).Items[1].Text = 0.ToString("N0");
                                    }
                                }
                            }
                            else if (gctrl is TextBox)
                            {
                                ((TextBox)gctrl).Clear();
                            }
                            else if (gctrl is DataGridView)
                            {
                                ((DataGridView)gctrl).DataSource = null;
                            }
                            else if (gctrl is ToolStrip)
                            {
                                ((ToolStrip)gctrl).Items[1].Text = 0.ToString("N0");
                            }
                        }
                    }

                }
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            argumentoBackground = e.Argument.ToString();
            switch (argumentoBackground)
            {
                case "importarExcel":
                    LeerExcel(rutaExcel);
                    break;

                case "comparar":
                    ListaJsonAux = ListaJson;
                    ComparacionAivic();
                    break;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (argumentoBackground)
            {
                case "importarExcel":
                    if (LstExcel.Count >= 1)
                    {
                        dgvManuales.DataSource = LstExcel;
                        tsManuales.Text = LstExcel.Count.ToString("N0");
                        MessageBox.Show("¡Información lista!.","Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    break;

                case "comparar":

                    if (!chkTodo.Checked)
                    {
                        LstResultados = LstResultados.Where(x => x.DiferenciaCantidades >= 0.01M).ToList();
                    }
                    progressBar1.Value = 95;

                    dgvErrores.DataSource = LstResultados.OrderBy(x => x.NombreCliente).ThenBy(x => x.UUID).ThenBy(x => x.Cant).ToList(); ;
                    tsErrores.Text = dgvErrores.RowCount.ToString();
                    progressBar1.Value = 100;

                    MessageBox.Show("Comparación realizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblEstado.Text = "¡Listo!";
                    progressBar1.Value = 0;
                    break;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }
    }
}
