using System;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace crudd.Pages
{
    public class CreateModel : PageModel
{
    [BindProperty, Required(ErrorMessage = "Product name is required")]
    public string Nome_produto { get; set; } = "";

    [BindProperty, Required(ErrorMessage = "Mark is required")]
    public string Marca { get; set; } = "";

    [BindProperty, Required(ErrorMessage = "Description is required")]
    public string Descricao { get; set; } = "";

    [BindProperty, Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
    public decimal Preco { get; set; }

    [BindProperty, Required(ErrorMessage = "Stock is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Stock must be at least 1")]
    public int Estoque { get; set; }

    [BindProperty, Required(ErrorMessage = "The date is required")]
    public string DataCadastro { get; set; } = "";

    [BindProperty, Url(ErrorMessage = "Invalid image URL")]
    public string ImagemUrl { get; set; } = "";

    public string ErrorMessage { get; set; } = "";

    public void OnGet()
    {
    }

    public void OnPost()
    {
        if (!ModelState.IsValid)
        {
            return;
        }

        try
        {
            string connectionString = "Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO produtos (Nome_produto, Marca, Descricao, Preco, Estoque, DataCadastro, ImagemUrl) VALUES " +
                             "(@Nome_produto, @Marca, @Descricao, @Preco, @Estoque, @DataCadastro, @ImagemUrl);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nome_produto", Nome_produto);
                    command.Parameters.AddWithValue("@Marca", Marca);
                    command.Parameters.AddWithValue("@Descricao", Descricao);
                    command.Parameters.AddWithValue("@Preco", Preco);
                    command.Parameters.AddWithValue("@Estoque", Estoque);
                    command.Parameters.AddWithValue("@DataCadastro", DataCadastro);
                    command.Parameters.AddWithValue("@ImagemUrl", ImagemUrl); // Adiciona a URL da imagem

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return;
        }

        Response.Redirect("ListProducts");
    }
}

}
