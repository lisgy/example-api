using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBriones.Class
{
    public class Persona
    {
        public int IdPersona { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int? IdUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}