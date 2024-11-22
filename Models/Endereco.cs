using System;
using System.Collections.Generic;

namespace crudd.Models;

public partial class Endereco
{
    public int IdEndereco { get; set; }

    public string? Cep { get; set; }

    public string Rua { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public string Bairro { get; set; } = null!;

    public string? Complemento { get; set; }

    public string TipoResidencia { get; set; } = null!;

    public string Cidade { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Notas { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
