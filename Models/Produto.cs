using System;
using System.Collections.Generic;

namespace crudd.Models;

public partial class Produto
{
    public int ProdutoId { get; set; }

    public string NomeProduto { get; set; } = null!;

    public string? Marca { get; set; }

    public string? Descricao { get; set; }

    public decimal Preco { get; set; }

    public int? Estoque { get; set; }

    public DateOnly? DataCadastro { get; set; }

    public string? ImagemUrl { get; set; }
}
