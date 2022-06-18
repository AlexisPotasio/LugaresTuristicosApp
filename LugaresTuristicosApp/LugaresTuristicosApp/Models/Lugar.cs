using System;
using System.Collections.Generic;
using System.Text;

namespace LugaresTuristicosApp.Models
{
    public class Lugar
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public string Dirección { get; set; }
        public string Imagen { get; set; } = "https://cdn-icons-png.flaticon.com/512/854/854878.png";
    }
}
