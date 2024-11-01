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
    public class ReadPageModel : PageModel
    {
        public List<UserEndereco> UserEnderecos { get; set; } = new List<UserEndereco>();

        public void OnGet()
        {
            string connectionString = "Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                SELECT 
                    u.id AS UserId,
                    u.firstname,
                    u.surname,
                    e.cep,
                    e.rua,
                    e.numero,
                    e.bairro,
                    e.complemento,
                    e.tipo_residencia,
                    e.cidade,
                    e.estado,
                    e.pais,
                    e.notas
                FROM 
                    users u
                JOIN 
                    endereco e ON u.id = e.UserId;";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserEnderecos.Add(new UserEndereco
                            {
                                UserId = reader.GetInt32(0),
                                firstname = reader.GetString(1),
                                surname = reader.GetString(2),
                                cep = reader.GetString(3),
                                rua = reader.GetString(4),
                                numero = reader.GetString(5),
                                bairro = reader.GetString(6),
                                complemento = reader.GetString(7),
                                tipo_residencia = reader.GetString(8),
                                cidade = reader.GetString(9),
                                estado = reader.GetString(10),
                                pais = reader.GetString(11),
                                notas = reader.GetString(12)
                            });
                        }
                    }
                }
            }
        }
    }

    public class UserEndereco
    {
        public required int UserId { get; set; }
        public required string firstname { get; set; }
        public required string surname { get; set; }
        public required string cep { get; set; }
        public required string rua { get; set; }
        public required string numero { get; set; }
        public required string bairro { get; set; }
        public required string complemento { get; set; }
        public required string tipo_residencia { get; set; }
        public required string cidade { get; set; }
        public required string estado { get; set; }
        public required string pais { get; set; }
        public required string notas { get; set; }
    }
}