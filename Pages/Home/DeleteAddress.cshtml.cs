using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace crudd.Pages.Home
{
    public class DeleteAddress : PageModel
    {
        public void OnPost(int id_endereco){
            deleteAddress(id_endereco);
            Response.Redirect("/Home/ReadPage");
        }

        private void deleteAddress(int id_endereco) {
            try{
                string connectionString ="Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    string sql = "DELETE FROM endereco WHERE id_endereco=@id_endereco";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id_endereco", id_endereco);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex) {
                Console.WriteLine("Cannot delete customer: " + ex.Message);
            }
        }
    }
}