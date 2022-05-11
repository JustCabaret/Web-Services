using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Modulo_Percurso
    {
        public int id_mod_percurso { get; set; }

        public int sequencia { get; set; }

        public string titulo { get; set; }

        public int duracao { get; set; }
    }
}