using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace crudd.Pages
{
    public class ListProductsModel : PageModel
    {
        // Propriedade para armazenar a lista de produtos
        public List<Produto> Produtos { get; set; } = new List<Produto>();

        public void OnGet()
        {
            // String de conexão
            string connectionString = "Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT ProdutoId, Nome_produto, Marca, Descricao, Preco, Estoque, DataCadastro FROM produtos";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Produto produto = new Produto
                                {
                                    ProdutoId = reader.GetInt32(0),
                                    Nome_produto = reader.GetString(1),
                                    Marca = reader.GetString(2),
                                    Descricao = reader.GetString(3),
                                    Preco = reader.GetDecimal(4),
                                    Estoque = reader.GetInt32(5),
                                    DataCadastro = reader.GetDateTime(6)
                                };

                                Produtos.Add(produto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Trate o erro, exiba mensagens conforme necessário
                Console.WriteLine(ex.Message);
            }
        }
    }

    // Classe Produto usada para representar cada produto na lista
    public class Produto
    {
        public required int ProdutoId { get; set; }
        public required string Nome_produto { get; set; }
        public required string Marca { get; set; }
        public required string Descricao { get; set; }
        public required decimal Preco { get; set; }
        public required int Estoque { get; set; }
        public required DateTime DataCadastro { get; set; }
    }
}