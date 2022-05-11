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

            SqlCommand command = new SqlCommand("SELECT id_percurso AS ID, nome AS [Nome], descricao AS Descrição, duracao AS Duração, competencia AS Competência FROM percurso_formativo WHERE ativo = 0", conn);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                PercursoFormacao percurso = new PercursoFormacao();

                percurso.nome = reader[1].ToString();
                percurso.descricao = reader[2].ToString();
                percurso.competencia = reader[4].ToString();

                listaPercurso.Add(percurso);
            }

            conn.Close();
            return listaPercurso;
        }

        // GET: api/WebApis/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/WebApis
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/WebApis/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/WebApis/5
        public void Delete(int id)
        {
        }
    }
}