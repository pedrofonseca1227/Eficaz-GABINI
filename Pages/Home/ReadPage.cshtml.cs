using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace crudd.Pages.Home
{
    public class ReadPageModel : PageModel
    {
        public List<UserInfo> UsersList {get;set;} = [];

        public void OnGet()
        {
            try {
                string connectionString = "Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "SELECT * FROM endereco ORDER BY id_endereco DESC";

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                UserInfo userInfo = new UserInfo();
                                userInfo.id_endereco = reader.GetInt32(0);
                                userInfo.cep = reader.GetString(1);
                                userInfo.rua = reader.GetString(2);
                                userInfo.numero = reader.GetString(3);
                                userInfo.bairro = reader.GetString(4);
                                userInfo.complemento = reader.GetString(5);
                                userInfo.tipo_residencia = reader.GetString(6);
                                userInfo.cidade = reader.GetString(7);
                                userInfo.estado = reader.GetString(8);
                                userInfo.pais = reader.GetString(9);
                                userInfo.notas = reader.GetString(10);

                                UsersList.Add(userInfo);
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
    public class UserInfo {
        public  int id_endereco { get; set; }
        public  string firstname { get; set; }="";
        public  string surname { get; set; }="";
        public  string cep { get; set; }="";
        public  string rua { get; set; }="";
        public  string numero { get; set; }="";
        public  string bairro { get; set; }="";
        public  string complemento { get; set; }="";
        public  string tipo_residencia { get; set; }="";
        public  string cidade { get; set; }="";
        public  string estado { get; set; }="";
        public  string pais { get; set; }="";
        public  string notas { get; set; }="";
    }


    
}