using System;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace crudd.Pages
{
    public class IndexModel : PageModel
    {
        public List<Produto> Produtos { get; set; } = new List<Produto>();

        public void OnGet()
        {
            string connectionString = "Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT ProdutoId, Nome_produto, Marca, Preco, Estoque FROM produtos";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Produtos.Add(new Produto
                                {
                                    ProdutoId = reader.GetInt32(0),
                                    Nome_produto = reader.GetString(1),
                                    Marca = reader.GetString(2),
                                    Preco = reader.GetDecimal(3),
                                    Estoque = reader.GetInt32(4)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Trate o erro apropriadamente
                Console.WriteLine("Erro ao carregar produtos: " + ex.Message);
            }
        }
    }
}
