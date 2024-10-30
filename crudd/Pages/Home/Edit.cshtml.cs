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
    public class EditModel : PageModel
    {
        [BindProperty]
        public int id {get;set;}

       [BindProperty, Required(ErrorMessage ="The firstname is required")]
        public string firstname {get;set;} = "";
        
        [BindProperty, Required(ErrorMessage ="The surname is required")]
        public string surname {get;set;} = "";

        [BindProperty, Required(ErrorMessage ="The Date is required")]
        public string register_date {get;set;} ="";

        [BindProperty, Phone]
        public string phone {get;set;} = "";
        public string security_number {get;set;} = "";
        public string gender {get;set;} ="";
        public string nacionalidade {get;set;} = "";

        [BindProperty, Required(ErrorMessage ="The username is required")]
        public string username {get;set;} = "";

        [BindProperty, Required, EmailAddress(ErrorMessage ="The email is required")]
        public string email {get;set;} = "";

        [BindProperty, Required(ErrorMessage ="The password is required")]
        public string senha {get;set;} = ""; 

        public string ErrorMessage {get;set;} = "";

        public void OnGet(int id)
        {
            try{
                string  connectionString ="Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "SELECT * FROM users WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) 
                            {
                                id = reader.GetInt32(0);
                                firstname = reader.GetString(1);
                                surname = reader.GetString(2);
                                register_date = reader.GetString(3);
                                phone = reader.GetString(4);
                                security_number = reader.GetString(5);
                                gender = reader.GetString(6);
                                nacionalidade = reader.GetString(7);
                                username = reader.GetString(8);
                                email = reader.GetString(9);
                                senha = reader.GetString(10);
                            }
                            else
                            {
                                Response.Redirect("/Home/Index");
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
                ErrorMessage = ex.Message;
            }
        }
    }
}