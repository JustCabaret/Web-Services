using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Modulo
    {
        public int id_modulo { get; set; }

        public string titulo { get; set; }

        public string carga_horaria { get; set; }

        public string URL { get; set; }

        public string descricao { get; set; }

        public string competencia { get; set; }
    }
}