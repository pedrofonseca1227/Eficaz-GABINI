using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace crudd.Pages
{
    public class ListProducts : PageModel
    {
        public List<Produto> ProdutosList { get; set; } = new List<Produto>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT ProdutoId, Nome_produto, Marca, Descricao, Preco, Estoque, DataCadastro, ImagemUrl FROM produtos";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Produto produto = new Produto
                                {
                                    ProdutoId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                    Nome_produto = reader.IsDBNull(1) ? "N/A" : reader.GetString(1),
                                    Marca = reader.IsDBNull(2) ? "N/A" : reader.GetString(2),
                                    Descricao = reader.IsDBNull(3) ? "Sem descrição" : reader.GetString(3),
                                    Preco = reader.IsDBNull(4) ? 0.0m : reader.GetDecimal(4),
                                    Estoque = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                                    DataCadastro = reader.IsDBNull(6) 
                                        ? "Data indisponível" 
                                        : reader.GetDateTime(6).ToString("MM/dd/yyyy"),
                                    ImagemUrl = reader.IsDBNull(7) || string.IsNullOrWhiteSpace(reader.GetString(7))
                                        ? null 
                                        : reader.GetString(7)
                                };

                                ProdutosList.Add(produto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao carregar produtos: " + ex.Message);
            }
        }
    }

    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome_produto { get; set; } = "";
        public string Marca { get; set; } = "";
        public string Descricao { get; set; } = "";
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string DataCadastro { get; set; } = "";
        public string? ImagemUrl { get; internal set; }

        public bool Validar()
        {
            return ProdutoId > 0 && 
                   !string.IsNullOrWhiteSpace(Nome_produto) && 
                   !string.IsNullOrWhiteSpace(Marca) && 
                   Preco >= 0 && 
                   Estoque >= 0;
        }
    }
}
