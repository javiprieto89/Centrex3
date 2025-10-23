using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("sysMoneda")]
public partial class SysMonedaEntity
{
    [Key]
    [Column("id_moneda")]
    public int IdMoneda { get; set; }

    [Column("moneda")]
    [StringLength(5)]
    public string Moneda { get; set; } = null!;

    [InverseProperty("IdMonedaNavigation")]
    public virtual ICollection<CcClienteEntity> CcClienteEntity { get; set; } = new List<CcClienteEntity>();

    [InverseProperty("IdMonedaNavigation")]
    public virtual ICollection<CcProveedorEntity> CcProveedorEntity { get; set; } = new List<CcProveedorEntity>();

    [InverseProperty("IdMonedaNavigation")]
    public virtual ICollection<ComprobanteCompraEntity> ComprobanteCompraEntity { get; set; } = new List<ComprobanteCompraEntity>();

    [InverseProperty("IdMonedaNavigation")]
    public virtual ICollection<CuentaBancariaEntity> CuentaBancariaEntity { get; set; } = new List<CuentaBancariaEntity>();
}
