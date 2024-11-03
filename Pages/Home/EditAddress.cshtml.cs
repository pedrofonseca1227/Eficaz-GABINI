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
    public class EditAddress : PageModel
    {
        [BindProperty]
        public int id_endereco {get;set;}

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

        public string ErrorMessage {get;set;} = "";

        public void OnGet(int id_endereco)
        {
            try{
                string  connectionString ="Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "SELECT * FROM endereco WHERE id_endereco=@id_endereco";
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@id_endereco", id_endereco);

                        using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) 
                            {
                                id_endereco = reader.GetInt32(0);
                                cep = reader.GetString(1);
                                rua = reader.GetString(2);
                                numero = reader.GetString(3);
                                bairro = reader.GetString(4);
                                complemento = reader.GetString(5);
                                tipo_residencia = reader.GetString(6);
                                cidade = reader.GetString(7);
                                estado = reader.GetString(8);
                                pais = reader.GetString(9);
                                notas = reader.GetString(10);
                            }
                            else
                            {
                                Response.Redirect("/Home/ReadPage");
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
                ErrorMessage = ex.Message;
            }
        }

        public void OnPost() {
            if (!ModelState.IsValid) {
                return;
            }

            if (cep == null) cep ="";

            try {
                string connectionString ="Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "UPDATE endereco SET cep=@cep, rua=@rua, numero=@numero, bairro=@bairro," +
                        "complemento=@complemento, tipo_residencia=@tipo_residencia, cidade=@cidade, estado=@estado, pais=@pais, notas=@notas WHERE id_endereco=@id_endereco;";

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
                        command.Parameters.AddWithValue("@id_endereco", id_endereco);

                        command.ExecuteNonQuery();
                    }    
                }
            }
            catch(Exception ex) {
                ErrorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Home/ReadPage");
        }
    }
}