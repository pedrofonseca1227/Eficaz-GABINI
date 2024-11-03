using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace crudd.Pages
{
    public class Login : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Username or Email is required")]
        public required string UsernameOrEmail { get; set; }

        [BindProperty, Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public required string senha { get; set; }

        public string ErrorMessage { get; set; } = "";

        public IActionResult OnPost() {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string connectionString = "Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT id FROM [users] WHERE (username = @UsernameOrEmail OR email = @UsernameOrEmail) AND senha = @Password";
                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UsernameOrEmail", UsernameOrEmail);
                        command.Parameters.AddWithValue("@Password", senha);

                        var userId = command.ExecuteScalar();
                        if (userId != null)
                        {
                            return RedirectToPage("/Home/Dashboard", new { id = (int)userId });
                        }
                        else
                        {
                            ErrorMessage = "Invalid username/email or password";
                        }
                    }
                }    
            }

            catch (Exception ex)
            {
                ErrorMessage = "An error occurred: " + ex.Message;
            }
    
            return Page();
        }
    }
}