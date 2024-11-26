using System;
using System.Collections.Generic;

namespace crudd.Models;

public partial class User
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateTime? RegisterDate { get; set; }

    public string Phone { get; set; } = null!;

    public string SecurityNumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Nacionalidade { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public virtual ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
}
