using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[PrimaryKey("IdCc", "IdCliente")]
[Table("cc_clientes")]
public partial class CcClienteEntity
{
    [Key]
    [Column("id_cc")]
    public int IdCc { get; set; }

    [Key]
    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("id_moneda")]
    public int IdMoneda { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [Column("saldo", TypeName = "decimal(18, 3)")]
    public decimal Saldo { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("CcClienteEntity")]
    public virtual ClienteEntity IdClienteNavigation { get; set; } = null!;


    [ForeignKey("IdMoneda")]
    [InverseProperty("CcClienteEntity")]
    public virtual SysMonedaEntity IdMonedaNavigation { get; set; } = null!;

    [InverseProperty("CcClienteEntity")]
    public virtual ICollection<PedidoEntity> PedidoEntity { get; set; } = new List<PedidoEntity>();
}
