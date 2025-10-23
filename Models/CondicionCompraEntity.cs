using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("condiciones_compra")]
public partial class CondicionCompraEntity
{
    [Key]
    [Column("id_condicion_compra")]
    public int IdCondicionCompra { get; set; }

    [Column("condicion")]
    [StringLength(100)]
    public string Condicion { get; set; } = null!;

    [Column("vencimiento")]
    public int Vencimiento { get; set; }

    [Column("recargo", TypeName = "decimal(18, 4)")]
    public decimal Recargo { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("IdCondicionCompraNavigation")]
    public virtual ICollection<ComprobanteCompraEntity> ComprobanteCompraEntity { get; set; } = new List<ComprobanteCompraEntity>();
}
