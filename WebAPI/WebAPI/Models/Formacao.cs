using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Formacao
    {
        public int id_formacao { get; set; }

        public int FK_id_modulo { get; set; }

        public int FK_id_funcionario { get; set; }

        public int FK_id_estado_formacao { get; set; }

        public DateTime data_inicio { get; set; }

        public string percentagem { get; set; }

        public string nota { get; set; }

        public string conclusão_dada { get; set; }
    }
}