using System;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace crudd.Pages
{
    public class IndexModel : PageModel
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
}