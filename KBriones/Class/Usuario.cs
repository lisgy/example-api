using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBriones.Class
{
    public class Usuario
    {

        public Usuario()
        {
            Tipo = new Tipo();
        }

        public int IdUsuario { get; set; }

        public string User { get; set; }

        public string Pass { get; set; }

        public Tipo Tipo { get; set; }
    }
}