using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Serializer : JsonTextWriter
    {
        public Serializer(TextWriter textWriter) : base(textWriter)
        {
            
        }

        public override void WritePropertyName(string name)
        {
            base.WritePropertyName(name.Replace("Covol:", string.Empty));
            
        }
    }
}
