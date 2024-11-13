using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
                    string sql = "SELECT * FROM produtos ORDER BY ProdutoId DESC";

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
                                    Preco = reader.IsDBNull(4) ? 0.0f : reader.GetFloat(4),
                                    Estoque = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                                    DataCadastro = reader.IsDBNull(6) 
                                        ? "Data indisponível" 
                                        : reader.GetDateTime(6).ToString("MM/dd/yyyy")
                                };

                                // Validações adicionais no código antes de adicionar o produto
                                if (produto.Validar())
                                {
                                    ProdutosList.Add(produto);
                                }
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
        public float Preco { get; set; }
        public int Estoque { get; set; }
        public string DataCadastro { get; set; } = "";

        // Método para validar o produto
        public bool Validar()
        {
            // Exemplo de validações para valores aceitáveis
            if (ProdutoId <= 0) return false;
            if (string.IsNullOrWhiteSpace(Nome_produto) || Nome_produto.Length > 50) return false;
            if (string.IsNullOrWhiteSpace(Marca) || Marca.Length > 30) return false;
            if (Preco < 0) return false;
            if (Estoque < 0) return false;

            return true;
        }
    }
}
