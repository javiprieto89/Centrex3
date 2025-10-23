using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("bancos")]
public partial class BancoEntity
{
    [Key]
    [Column("id_banco")]
    public int IdBanco { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("id_pais")]
    public int IdPais { get; set; }

    [Column("n_banco")]
    public int? NBanco { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("IdBancoNavigation")]
    public virtual ICollection<ChequeEntity> ChequeEntity { get; set; } = new List<ChequeEntity>();

    [InverseProperty("IdBancoNavigation")]
    public virtual ICollection<CuentaBancariaEntity> CuentaBancariaEntity { get; set; } = new List<CuentaBancariaEntity>();

    [ForeignKey("IdPais")]
    [InverseProperty("BancoEntity")]
    public virtual PaisEntity IdPaisNavigation { get; set; } = null!;
}
