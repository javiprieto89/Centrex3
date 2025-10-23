using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("conceptos_compra")]
public partial class ConceptoCompraEntity
{
    [Key]
    [Column("id_concepto_compra")]
    public int IdConceptoCompra { get; set; }

    [Column("concepto")]
    [StringLength(100)]
    public string Concepto { get; set; } = null!;

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("IdConceptoCompraNavigation")]
    public virtual ICollection<ComprobanteCompraConceptoEntity> ComprobanteCompraConceptoEntity { get; set; } = new List<ComprobanteCompraConceptoEntity>();
}
