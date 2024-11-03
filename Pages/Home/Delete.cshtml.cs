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
    public class Delete : PageModel
    {


        public void OnPost(int id){
            deleteCustomer(id);
            Response.Redirect("/Home/Index");
        }

        private void deleteCustomer(int id) {
            try{
                string connectionString ="Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //  Delete the customer of database

                    string sql = "DELETE FROM users WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
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