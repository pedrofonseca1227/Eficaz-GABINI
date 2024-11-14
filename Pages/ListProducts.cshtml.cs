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
                                    Preco = reader.IsDBNull(4) ? 0.0m : reader.GetDecimal(4), // Alterado de GetFloat para GetDecimal
                                    Estoque = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                                    DataCadastro = reader.IsDBNull(6) 
                                        ? "Data indisponível" 
                                        : reader.GetDateTime(6).ToString("MM/dd/yyyy")
                                };
        
                                ProdutosList.Add(produto);
                                Console.WriteLine($"Produto carregado: {produto.Nome_produto} - {produto.Marca}");
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

        // Método para validar o produto
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
