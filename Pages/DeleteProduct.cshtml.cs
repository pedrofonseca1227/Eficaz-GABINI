using System;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace crudd.Pages
{
    public class DeleteProductModel : PageModel
    {
        [BindProperty]
        public Produto Produto { get; set; } = new Produto();

        public string ErrorMessage { get; set; } = "";
        public string? ImagemUrl { get; private set; }

        // Carrega o produto a ser excluído
        public void OnGet(int id)
        {
            try
            {
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
                                Produto.ProdutoId = reader.GetInt32(0);
                                Produto.Nome_produto = reader.GetString(1);
                                Produto.Marca = reader.GetString(2);
                                Produto.Descricao = reader.GetString(3);
                                Produto.Preco = reader.GetDecimal(4);
                                Produto.Estoque = reader.GetInt32(5);
                                Produto.DataCadastro = reader.GetDateTime(6).ToString("MM/dd/yyyy");
                                ImagemUrl = reader.GetString(7);
                            }
                            else
                            {
                                ErrorMessage = "Produto não encontrado.";
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

        // Método para deletar o produto
        public IActionResult OnPost()
        {
            if (Produto.ProdutoId <= 0)
            {
                ErrorMessage = "Produto inválido.";
                return Page();
            }

            try
            {
                string connectionString = "Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM produtos WHERE ProdutoId = @ProdutoId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProdutoId", Produto.ProdutoId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            ErrorMessage = "Nenhum produto encontrado para excluir.";
                            return Page();
                        }
                    }
                }

                return RedirectToPage("/ListProducts");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Erro ao excluir o produto: " + ex.Message;
                return Page();
            }
        }
    }
}
