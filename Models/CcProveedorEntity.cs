using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("cc_proveedores")]
public partial class CcProveedorEntity
{
    [Key]
    [Column("id_cc")]
    public int IdCc { get; set; }

    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [Column("id_moneda")]
    public int IdMoneda { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [Column("saldo", TypeName = "decimal(18, 3)")]
    public decimal Saldo { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("IdCcNavigation")]
    public virtual ICollection<ComprobanteCompraEntity> ComprobanteCompraEntity { get; set; } = new List<ComprobanteCompraEntity>();

    [ForeignKey("IdMoneda")]
    [InverseProperty("CcProveedorEntity")]
    public virtual SysMonedaEntity IdMonedaNavigation { get; set; } = null!;

    [ForeignKey("IdProveedor")]
    [InverseProperty("CcProveedorEntity")]
    public virtual ProveedorEntity IdProveedorNavigation { get; set; } = null!;

    [InverseProperty("IdCcNavigation")]
    public virtual ICollection<PagoEntity> PagoEntity { get; set; } = new List<PagoEntity>();
}
