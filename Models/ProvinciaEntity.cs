using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("provincias")]
public partial class ProvinciaEntity
{
    [Key]
    [Column("id_provincia")]
    public int IdProvincia { get; set; }

    [Column("id_pais")]
    public int IdPais { get; set; }

    [Column("provincia")]
    [StringLength(100)]
    public string Provincia { get; set; } = null!;

    [InverseProperty("IdProvinciaEntregaNavigation")]
    public virtual ICollection<ClienteEntity> ClienteEntityIdProvinciaEntregaNavigation { get; set; } = new List<ClienteEntity>();

    [InverseProperty("IdProvinciaFiscalNavigation")]
    public virtual ICollection<ClienteEntity> ClienteEntityIdProvinciaFiscalNavigation { get; set; } = new List<ClienteEntity>();

    [ForeignKey("IdPais")]
    [InverseProperty("ProvinciaEntity")]
    public virtual PaisEntity IdPaisNavigation { get; set; } = null!;

    [InverseProperty("IdProvinciaEntregaNavigation")]
    public virtual ICollection<ProveedorEntity> ProveedorEntityIdProvinciaEntregaNavigation { get; set; } = new List<ProveedorEntity>();

    [InverseProperty("IdProvinciaFiscalNavigation")]
    public virtual ICollection<ProveedorEntity> ProveedorEntityIdProvinciaFiscalNavigation { get; set; } = new List<ProveedorEntity>();
}
