using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("cajas")]
public partial class CajaEntity
{
    [Key]
    [Column("id_caja")]
    public int IdCaja { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [Column("esCC")]
    public bool EsCc { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }
}
