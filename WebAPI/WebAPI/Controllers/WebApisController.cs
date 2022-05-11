using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class WebApisController : ApiController
    {
        // GET: api/WebApis

        [Route("api/MetodoA")]
        [HttpGet]
        public List<PercursoFormacao> GetListPercursos()
        {
            List<PercursoFormacao> listaPercurso = new List<PercursoFormacao>();

            SqlConnection conn = new SqlConnection("Data Source=CABARET-PC;Initial Catalog=BDApp;Integrated Security=True");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT id_percurso AS ID, nome AS [Nome], descricao AS Descrição, duracao AS Duração, competencia AS Competência FROM percurso_formativo WHERE ativo = 1", conn);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                PercursoFormacao percurso = new PercursoFormacao();

                percurso.id_percurso = Convert.ToInt32(reader[0]);
                percurso.nome = reader[1].ToString();
                percurso.descricao = reader[2].ToString();
                percurso.duracao = reader[3].ToString();
                percurso.competencia = reader[4].ToString();

                listaPercurso.Add(percurso);
            }

            conn.Close();
            return listaPercurso;
        }

        [Route("api/MetodoB")]
        [HttpGet]
        public List<PercursoFormacao> GetModulosFormacao(string id_percurso)
        {
            List<Modulo_Percurso> listaModuloPercurso = new List<Modulo_Percurso>();

            SqlConnection conn = new SqlConnection("Data Source=CABARET-PC;Initial Catalog=BDApp;Integrated Security=True");
            conn.Open();

            if (string.IsNullOrEmpty(id_percurso))
            {
                SqlCommand command = new SqlCommand("SELECT id_mod_percurso AS ID,sequencia AS Sequência, titulo AS Titulo, carga_horaria AS Duração FROM modulo_percurso RIGHT JOIN modulo ON modulo_percurso.FK_id_modulo = modulo.id_modulo ORDER BY sequencia ASC ", conn);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Modulo_Percurso mod_percurso = new Modulo_Percurso();

                    mod_percurso.id_mod_percurso = Convert.ToInt32(reader[0]);
                    mod_percurso.sequencia = Convert.ToInt32(reader[1]);
                    mod_percurso.titulo = reader[2].ToString();
                    mod_percurso.duracao = Convert.ToInt32(reader[3]);

                    listaModuloPercurso.Add(mod_percurso);
                }
            }
            else
            {
            }

            conn.Close();

            return listaModuloPercurso;
        }
    }
}