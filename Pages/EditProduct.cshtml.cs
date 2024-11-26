using System;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace crudd.Pages
{
    public class EditProduct : PageModel
    {
        [BindProperty]
        public Produto Produto { get; set; } = new Produto();

        public string ErrorMessage { get; set; } = "";

        public void OnGet(int id)
        {
            try
            {
                Console.WriteLine("ProdutoId recebido na requisição: " + id);

                string connectionString = "Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM produtos WHERE ProdutoId = @ProdutoId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProdutoId", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Produto = new Produto
                                {
                                    ProdutoId = reader.GetInt32(0),
                                    Nome_produto = reader.GetString(1),
                                    Marca = reader.GetString(2),
                                    Descricao = reader.GetString(3),
                                    Preco = (decimal)reader.GetDecimal(4),
                                    Estoque = reader.GetInt32(5),
                                    DataCadastro = reader.GetDateTime(6).ToString("MM/dd/yyyy")
                                };
                            }
                            else
                            {
                                ErrorMessage = "Produto não encontrado.";
                                Console.WriteLine("Produto não encontrado para o id: " + id);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Erro ao carregar o produto: " + ex.Message;
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Produto.Preco = decimal.Parse(Produto.Preco.ToString().Replace(',', '.'));

                Console.WriteLine("Atualizando produto com ProdutoId: " + Produto.ProdutoId);
                Console.WriteLine("Nome_produto: " + Produto.Nome_produto);
                Console.WriteLine("Marca: " + Produto.Marca);
                Console.WriteLine("Preco: " + Produto.Preco);
                Console.WriteLine("Estoque: " + Produto.Estoque);

                string connectionString = "Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE produtos SET Nome_produto = @Nome_produto, Marca = @Marca, Descricao = @Descricao, Preco = @Preco, Estoque = @Estoque WHERE ProdutoId = @ProdutoId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nome_produto", Produto.Nome_produto);
                        command.Parameters.AddWithValue("@Marca", Produto.Marca);
                        command.Parameters.AddWithValue("@Descricao", Produto.Descricao);
                        command.Parameters.AddWithValue("@Preco", Produto.Preco);
                        command.Parameters.AddWithValue("@Estoque", Produto.Estoque);
                        command.Parameters.AddWithValue("@ProdutoId", Produto.ProdutoId);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine("Linhas afetadas na atualização: " + rowsAffected);
                    }
                }

                return RedirectToPage("/ListProducts");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Erro ao atualizar o produto: " + ex.Message;
                return Page();
            }
        }

    }
}
