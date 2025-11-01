using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("cuentas_bancarias")]
public partial class CuentaBancariaEntity
{
    [Key]
    [Column("id_cuentaBancaria")]
    public int IdCuentaBancaria { get; set; }

    [Column("id_banco")]
    public int IdBanco { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("id_moneda")]
    public int IdMoneda { get; set; }

    [Column("saldo", TypeName = "decimal(18, 3)")]
    public decimal? Saldo { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("IdCuentaBancariaNavigation")]
    public virtual ICollection<ChequeEntity> ChequeEntity { get; set; } = new List<ChequeEntity>();

    [ForeignKey("IdBanco")]
    [InverseProperty("CuentaBancariaEntity")]
    public virtual BancoEntity IdBancoNavigation { get; set; } = null!;

    [ForeignKey("IdMoneda")]
    [InverseProperty("CuentaBancariaEntity")]
    public virtual SysMonedaEntity IdMonedaNavigation { get; set; } = null!;

    [InverseProperty("IdCuentaBancariaNavigation")]
    public virtual ICollection<TmpTransferenciaEntity> TmpTransferenciaEntity { get; set; } = new List<TmpTransferenciaEntity>();

    [InverseProperty("IdCuentaBancariaNavigation")]
    public virtual ICollection<TransferenciaEntity> TransferenciaEntity { get; set; } = new List<TransferenciaEntity>();
}
