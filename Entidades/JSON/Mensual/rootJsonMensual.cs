using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.JSON.Mensual
{
    public class rootJsonMensual
    {
        public decimal Version { get; set; }
        public string RfcContribuyente { get; set; }
        public string RfcRepresentanteLegal { get; set; }
        public string RfcProveedor { get; set; }
        public string Caracter { get; set; }
        public string ModalidadPermiso { get; set; }
        public string NumPermiso { get; set; }
        public string ClaveInstalacion { get; set; }
        public string DescripcionInstalacion { get; set; }
        public int NumeroPozos { get; set; }
        public int NumeroTanques { get; set; }
        public int NumeroDuctosEntradaSalida { get; set; }
        public int NumeroDuctosTransporteDistribucion { get; set; }
        public int NumeroDispensarios { get; set; }
        public DateTime FechaYHoraReporteMes { get; set; }
        public List<Producto> Producto { get; set; }
        public List<BitacoraMensual> BitacoraMensual { get; set; }
    }

    public class Producto
    {
        public string ClaveProducto { get; set; }
        public string ClaveSubProducto { get; set; }
        public int ComposOctanajeGasolina { get; set; }
        public string GasolinaConCombustibleNoFosil { get; set; }
        public ReporteDeVolumenMensual ReporteDeVolumenMensual { get; set; }
    }

    public class ReporteDeVolumenMensual
    {
        public ControlDeExistencias ControlDeExistencias { get; set; }
        public Recepciones Recepciones { get; set; }
        public Entregas Entregas { get; set; }
    }

    public class ControlDeExistencias
    {
        public decimal VolumenExistenciasMes { get; set; }
        public DateTime FechaYHoraEstaMedicionMes { get; set; }
    }

    public class Recepciones
    {
        public int TotalRecepcionesMes { get; set; }
        public SumaVolumenRecepcionMes SumaVolumenRecepcionMes { get; set; }
        public int TotalDocumentosMes { get; set; }
        public decimal ImporteTotalRecepcionesMensual { get; set; }
        public List<ComplementoRecepcion> Complemento { get; set; }
    }

    public class SumaVolumenRecepcionMes
    {
        public string UnidadDeMedida { get; set; }
        public decimal ValorNumerico { get; set; }
    }

    public class ComplementoRecepcion
    {
        public string TipoComplemento { get; set; }
        public List<NacionalRecepcion> Nacional { get; set; }
    }

    public class NacionalRecepcion
    {
        public string RfcClienteOProveedor { get; set; }
        public string NombreClienteOProveedor { get; set; }
        public string PermisoProveedor { get; set; }
        public List<CfdiRecepcion> CFDIs { get; set; }
    }

    public class CfdiRecepcion
    {
        public string Cfdi { get; set; }
        public string TipoCfdi { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioDeVentaAlPublico { get; set; }
        public decimal PrecioVenta { get; set; }
        public DateTime FechaYHoraTransaccion { get; set; }
        public VolumenDocumentado VolumenDocumentado { get; set; }
    }

    public class VolumenDocumentado
    {
        public string UnidadDeMedida { get; set; }
        public decimal ValorNumerico { get; set; }
    }

    public class Entregas
    {
        public int TotalEntregasMes { get; set; }
        public SumaVolumenEntregadoMes SumaVolumenEntregadoMes { get; set; }
        public int TotalDocumentosMes { get; set; }
        public decimal ImporteTotalEntregasMes { get; set; }
        public List<ComplementoEntrega> Complemento { get; set; }
    }

    public class SumaVolumenEntregadoMes
    {
        public string UnidadDeMedida { get; set; }
        public decimal ValorNumerico { get; set; }
    }

    public class ComplementoEntrega
    {
        public string TipoComplemento { get; set; }
        public List<NacionalEntrega> Nacional { get; set; }
    }

    public class NacionalEntrega
    {
        public string RfcClienteOProveedor { get; set; }
        public string NombreClienteOProveedor { get; set; }
        public List<CfdiEntrega> CFDIs { get; set; }
    }

    public class CfdiEntrega
    {
        public string Cfdi { get; set; }
        public string TipoCfdi { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioDeVentaAlPublico { get; set; }
        public decimal PrecioVenta { get; set; }
        public DateTime FechaYHoraTransaccion { get; set; }
        public VolumenDocumentado VolumenDocumentado { get; set; }
    }

    public class BitacoraMensual
    {
        public int NumeroRegistro { get; set; }
        public DateTime FechaYHoraEvento { get; set; }
        public string UsuarioResponsable { get; set; }
        public int TipoEvento { get; set; }
        public string DescripcionEvento { get; set; }
    }
}
