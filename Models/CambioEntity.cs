using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("cambios")]
public partial class CambioEntity
{
    [Key]
    [Column("id_cambio")]
    public int IdCambio { get; set; }

    [Column("cambio")]
    [Unicode(false)]
    public string Cambio { get; set; } = null!;

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }
}
