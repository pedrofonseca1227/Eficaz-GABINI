using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace crudd.Pages.Home
{
    public class CreateEndereco : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Zip code is required")]
        public string cep { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "The street is required")]
        public string rua { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "The number is required")]
        public string numero { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Neighborhood is required")]
        public string bairro { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Complement is required")]
        public string complemento { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Type of residence is required")]
        public string tipo_residencia { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "The city is required")]
        public string cidade { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "The state is required")]
        public string estado { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Country is required")]
        public string pais { get; set; } = "";

        [BindProperty]
        public string notas { get; set; } = "";

        public void OnGet()
        {
        }

        public string ErrorMessage { get; set; } = "";

        public void OnPost()
        {
            if (!ModelState.IsValid) {
                return;
            }

            if (cep == null) cep="";

            try {
                string  connectionString ="Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql ="INSERT INTO endereco" +
                    "(cep, rua, numero, bairro, complemento, tipo_residencia, cidade, estado, pais, notas) VALUES" +
                    "(@cep, @rua, @numero, @bairro, @complemento, @tipo_residencia, @cidade, @estado, @pais, @notas);";

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@cep", cep);
                        command.Parameters.AddWithValue("@rua", rua);
                        command.Parameters.AddWithValue("@numero", numero);
                        command.Parameters.AddWithValue("@bairro", bairro);
                        command.Parameters.AddWithValue("@complemento", complemento);
                        command.Parameters.AddWithValue("@tipo_residencia", tipo_residencia);
                        command.Parameters.AddWithValue("@cidade", cidade);
                        command.Parameters.AddWithValue("@estado", estado);
                        command.Parameters.AddWithValue("@pais", pais);
                        command.Parameters.AddWithValue("@notas", notas);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex) {
                ErrorMessage =ex.Message;
                return;
            }
            

            Response.Redirect("/Home/ReadPage");
        }
    }
}



