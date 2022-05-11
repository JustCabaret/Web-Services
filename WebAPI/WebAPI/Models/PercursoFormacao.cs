using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class PercursoFormacao
    {
        public int id_percurso { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }

        public string duracao { get; set; }

        public string competencia { get; set; }
    }
}