namespace MeuProjetoCRUD.Models
{
    public class User
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string Surname { get; set; }
    public required DateTime RegisterDate { get; set; }
    public required string Phone { get; set; }
    public required string SecurityNumber { get; set; }
    public required string Gender { get; set; }
    public required string Nacionalidade { get; set; } 
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; } 
}

}