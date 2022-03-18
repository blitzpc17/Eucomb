﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.XMLMensual
{
    public class ControlesVolumetricos
    {          
        public string Version { get; set; }
        public string RfcContribuyente { get; set; }
        public string RfcRepresentanteLegal { get; set; }
        public string RfcProveedor { get; set; }
        public Caracter Caracter { get; set; }
        public string ClaveInstalacion { get; set; }
        public string DescripcionInstalacion { get; set; }
        public int NumeroPozos { get; set; }
        public int NumeroTanques { get; set; }
        public int NumeroDuctosEntradaSalida { get; set; }
        public int NumeroDuctosTransporteDistribucion { get; set; }
        public int NumeroDispensarios { get; set; }
        public DateTime FechaYHoraCorte { get; set; }
        public List<PRODUCTO> PRODUCTO { get; set; }







    }
}
