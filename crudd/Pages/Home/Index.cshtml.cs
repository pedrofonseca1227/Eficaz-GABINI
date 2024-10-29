using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;

namespace crudd.Pages.Home
{
    public class Index : PageModel
    {   
        public List<CustomerInfo> CustomersList {get;set;} = [];
        public void OnGet()
        {
            try {
                string  connectionString ="Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "SELECT * FROM users";
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()){
                                CustomerInfo customerInfo = new CustomerInfo();
                                customerInfo.id = reader.GetInt32(0);
                                customerInfo.firstname = reader.GetString(1);
                                customerInfo.surname = reader.GetString(2);
                                customerInfo.register_date = reader.GetDateTime(3). ToString("mm/dd/year");
                                customerInfo.phone = reader.GetString(4);
                                customerInfo.security_number = reader.GetString(5);
                                customerInfo.gender = reader.GetString(6);
                                customerInfo.nacionalidade = reader.GetString(7);
                                customerInfo.username = reader.GetString(8);
                                customerInfo.email = reader.GetString(9);
                                customerInfo.senha = reader.GetString(10);

                                CustomersList.Add(customerInfo);
                            }
                        }

                    }
                }
            }
            catch(Exception ex) {
                Console.WriteLine("We have an error:" + ex.Message);
            }
        }
    }
    public class CustomerInfo {
        public int id {get;set;}
        public string firstname {get;set;} = "";
        public string surname {get;set;} = "";
        public string register_date {get;set;} ="";
        public string phone {get;set;} = "";
        public string security_number {get;set;} = "";
        public string gender {get;set;} ="";
        public string nacionalidade {get;set;} = "";
        public string username {get;set;} = "";
        public string email {get;set;} = "";
        public string senha {get;set;} = "";
    }


}