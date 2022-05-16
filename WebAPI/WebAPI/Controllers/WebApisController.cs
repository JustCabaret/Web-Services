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
        public string msg = "Não foi substituido";

        //Listar Dados da Tabela
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

        //Listar Dados da Tabela de acordo com o id
        [Route("api/MetodoB")]
        [HttpGet]
        public List<Modulo_Percurso> GetModulosFormacao(string id_percurso)
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
                SqlCommand command = new SqlCommand($"SELECT id_mod_percurso AS ID,sequencia AS Sequência, titulo AS Titulo, carga_horaria AS Duração FROM modulo_percurso RIGHT JOIN modulo ON modulo_percurso.FK_id_modulo = modulo.id_modulo WHERE modulo_percurso.FK_id_percurso = '{id_percurso}' ORDER BY sequencia ASC ", conn);

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

            conn.Close();

            return listaModuloPercurso;
        }

        //Inserir Dados na Tabela
        [Route("api/MetodoC")]
        [HttpPost]
        public string PostFormacao(int IdModulo, int NFuncionario, int Estado, DateTime Data, string Percentagem, string Classificacao, string Avaliacao)
        {
            SqlConnection conn = new SqlConnection("Data Source=CABARET-PC;Initial Catalog=BDApp;Integrated Security=True");
            conn.Open();

            SqlCommand command = new SqlCommand("INSERT INTO formacao(FK_id_modulo, FK_id_funcionario, FK_id_estado_formacao, data_inicio, percentagem, nota , conclusao_dada, ativo) " +
                "VALUES ('" + IdModulo + "', '" + NFuncionario + "', '" + Estado + "', '" + Data.ToString("yyyy-MM-dd") + "', '" + Percentagem + "','" + Classificacao + "', '" + Avaliacao + "', 1); ", conn);

            int result = command.ExecuteNonQuery();

            if (result == 0)
            {
                msg = "Erro na Inserção!";
            }
            else
            {
                msg = "Inserido com sucesso!";
            }

            conn.Close();
            return msg;
        }

        //Listar Dados da Tabela de acordo com o id
        [Route("api/MetodoD")]
        [HttpGet]
        public List<Formacao> GetFormacao(string id_func)
        {
            List<Formacao> listaFormacao = new List<Formacao>();

            SqlConnection conn = new SqlConnection("Data Source=CABARET-PC;Initial Catalog=BDApp;Integrated Security=True");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT id_formacao AS ID, FK_id_modulo AS [ID do Módulo], FK_id_funcionario AS [ID do Funcionário], FK_id_estado_formacao AS [Estado da Formação], data_inicio AS [Data de Início], percentagem AS Percentagem, nota AS Classificação, conclusao_dada AS Avaliação FROM formacao WHERE FK_id_funcionario = '" + id_func + "' AND ativo = 1", conn);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Formacao form = new Formacao();

                form.id_formacao = Convert.ToInt32(reader[0]);
                form.FK_id_modulo = Convert.ToInt32(reader[1]);
                form.FK_id_funcionario = Convert.ToInt32(reader[2]);
                form.FK_id_estado_formacao = Convert.ToInt32(reader[3]);
                form.data_inicio = Convert.ToDateTime(reader[4]);
                form.percentagem = reader[5].ToString();
                form.nota = reader[6].ToString();
                form.conclusão_dada = reader[7].ToString();

                listaFormacao.Add(form);
            }

            conn.Close();

            return listaFormacao;
        }

        //Atualizar Dados da Tabela
        [Route("api/MetodoE")]
        [HttpGet]
        public string UpdFormacao(int IDFormacao, int IDModulo, int NFuncionario, int Estado, DateTime Data, int Percentagem, int Classificacao, string Avaliacao)
        {
            SqlConnection conn = new SqlConnection("Data Source=CABARET-PC;Initial Catalog=BDApp;Integrated Security=True");
            conn.Open();

            SqlCommand command = new SqlCommand("UPDATE formacao SET FK_id_modulo = '" + IDModulo + "', FK_id_funcionario = '" + NFuncionario + "', FK_id_estado_formacao = '" + Estado + "', data_inicio = '" + Data.ToString("yyyy-MM-dd") + "', percentagem = '" + Percentagem + "', nota = '" + Classificacao + "', conclusao_dada = '" + Avaliacao + "' WHERE id_formacao = '" + IDFormacao + "'", conn);

            int result = command.ExecuteNonQuery();

            string msg = "Atualizado com sucesso!";

            if (result == 0)
            {
                msg = "Erro na Atualização!";
            }
            else
            {
                msg = "Atualizado com sucesso!";
            }

            conn.Close();
            return msg;
        }

        //Listar Dados da Tabela
        [Route("api/MetodoF")]
        [HttpGet]
        public List<Modulo> GetModulo()
        {
            List<Modulo> listaModulo = new List<Modulo>();

            SqlConnection conn = new SqlConnection("Data Source=CABARET-PC;Initial Catalog=BDApp;Integrated Security=True");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT id_modulo AS ID, titulo AS Titulo, carga_horaria AS [Carga Horária], descricao AS Descrição, competencia AS Competência FROM modulo WHERE ativo = 1", conn);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Modulo mod = new Modulo();

                mod.id_modulo = Convert.ToInt32(reader[0]);
                mod.titulo = reader[1].ToString();
                mod.carga_horaria = reader[2].ToString();
                mod.descricao = reader[3].ToString();
                mod.competencia = reader[4].ToString();

                listaModulo.Add(mod);
            }

            conn.Close();

            return listaModulo;
        }
    }
}