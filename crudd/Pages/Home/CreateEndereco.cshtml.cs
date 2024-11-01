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
        [BindProperty, Required(ErrorMessage = "O CEP é obrigatório")]
        public string cep { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "A rua é obrigatória")]
        public string rua { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "O número é obrigatório")]
        public string numero { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "O bairro é obrigatório")]
        public string bairro { get; set; } = "";

        [BindProperty]
        public string complemento { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "O tipo de residência é obrigatório")]
        public string tipo_residencia { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "A cidade é obrigatória")]
        public string cidade { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "O estado é obrigatório")]
        public string estado { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "O país é obrigatório")]
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
            

            Response.Redirect("/Home/Index");
        }
    }
}



