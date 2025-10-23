using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("impuestos")]
public partial class ImpuestoEntity
{
    [Key]
    [Column("id_impuesto")]
    public int IdImpuesto { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("esRetencion")]
    public bool? EsRetencion { get; set; }

    [Column("esPercepcion")]
    public bool? EsPercepcion { get; set; }

    [Column("porcentaje", TypeName = "decimal(18, 4)")]
    public decimal Porcentaje { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("IdImpuestoNavigation")]
    public virtual ICollection<CobroRetencionEntity> CobroRetencionEntity { get; set; } = new List<CobroRetencionEntity>();

    [InverseProperty("IdImpuestoNavigation")]
    public virtual ICollection<ComprobanteCompraImpuestoEntity> ComprobanteCompraImpuestoEntity { get; set; } = new List<ComprobanteCompraImpuestoEntity>();

    [InverseProperty("IdImpuestoNavigation")]
    public virtual ICollection<ItemImpuestoEntity> ItemImpuestoEntity { get; set; } = new List<ItemImpuestoEntity>();

    [InverseProperty("IdImpuestoNavigation")]
    public virtual ICollection<TmpCobroRetencionEntity> TmpCobroRetencionEntity { get; set; } = new List<TmpCobroRetencionEntity>();
}
