using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("ajustes_stock")]
public partial class AjusteStockEntity
{
    [Key]
    [Column("id_ajusteStock")]
    public int IdAjusteStock { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("id_item")]
    public int IdItem { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("notas")]
    public string? Notas { get; set; }

    [ForeignKey("IdItem")]
    [InverseProperty("AjusteStockEntity")]
    public virtual ItemEntity IdItemNavigation { get; set; } = null!;
}
