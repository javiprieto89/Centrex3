using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("items")]
public partial class ItemEntity
{
    [Key]
    [Column("id_item")]
    public int IdItem { get; set; }

    [Column("item")]
    [StringLength(50)]
    public string Item { get; set; } = null!;

    [Column("descript")]
    [StringLength(54)]
    public string? Descript { get; set; }

    [Column("cantidad")]
    public int? Cantidad { get; set; }

    [Column("costo", TypeName = "decimal(18, 6)")]
    public decimal Costo { get; set; }

    [Column("precio_lista", TypeName = "decimal(18, 6)")]
    public decimal PrecioLista { get; set; }

    [Column("id_tipo")]
    public int IdTipo { get; set; }

    [Column("id_marca")]
    public int IdMarca { get; set; }

    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [Column("factor", TypeName = "decimal(18, 6)")]
    public decimal? Factor { get; set; }

    [Column("esDescuento")]
    public bool EsDescuento { get; set; }

    [Column("esMarkup")]
    public bool EsMarkup { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [NotMapped]
    public int IdItemTemporal { get; set; }

    [InverseProperty("IdItemNavigation")]
    public virtual ICollection<AjusteStockEntity> AjusteStockEntity { get; set; } = new List<AjusteStockEntity>();

    [InverseProperty("IdItemAsocNavigation")]
    public virtual ICollection<AsocItemEntity> AsocItemEntityIdItemAsocNavigation { get; set; } = new List<AsocItemEntity>();

    [InverseProperty("IdItemNavigation")]
    public virtual ICollection<AsocItemEntity> AsocItemEntityIdItemNavigation { get; set; } = new List<AsocItemEntity>();

    [InverseProperty("IdItemNavigation")]
    public virtual ICollection<ComprobanteCompraItemEntity> ComprobanteCompraItemEntity { get; set; } = new List<ComprobanteCompraItemEntity>();

    [ForeignKey("IdMarca")]
    [InverseProperty("ItemEntity")]
    public virtual MarcaItemEntity IdMarcaNavigation { get; set; } = null!;

    [ForeignKey("IdProveedor")]
    [InverseProperty("ItemEntity")]
    public virtual ProveedorEntity IdProveedorNavigation { get; set; } = null!;

    [ForeignKey("IdTipo")]
    [InverseProperty("ItemEntity")]
    public virtual TipoItemEntity IdTipoNavigation { get; set; } = null!;

    [InverseProperty("IdItemNavigation")]
    public virtual ICollection<ItemImpuestoEntity> ItemImpuestoEntity { get; set; } = new List<ItemImpuestoEntity>();

    [InverseProperty("IdItemNavigation")]
    public virtual ICollection<OrdenCompraItemEntity> OrdenCompraItemEntity { get; set; } = new List<OrdenCompraItemEntity>();

    [InverseProperty("IdItemNavigation")]
    public virtual ICollection<PedidoItemEntity> PedidoItemEntity { get; set; } = new List<PedidoItemEntity>();

    [InverseProperty("IdItemNavigation")]
    public virtual ICollection<ProduccionItemEntity> ProduccionItemEntityIdItemNavigation { get; set; } = new List<ProduccionItemEntity>();

    [InverseProperty("IdItemRecibidoNavigation")]
    public virtual ICollection<ProduccionItemEntity> ProduccionItemEntityIdItemRecibidoNavigation { get; set; } = new List<ProduccionItemEntity>();

    [InverseProperty("IdItemNavigation")]
    public virtual ICollection<RegistroStockEntity> RegistroStockEntity { get; set; } = new List<RegistroStockEntity>();

    [InverseProperty("IdItemNavigation")]
    public virtual ICollection<TmpProduccionItemEntity> TmpProduccionItemEntity { get; set; } = new List<TmpProduccionItemEntity>();

    [InverseProperty("IdItemRecibidoNavigation")]
    public virtual ICollection<TmpProduccionItemEntity> TmpProduccionItemRecibidoEntity { get; set; } = new List<TmpProduccionItemEntity>();

    [InverseProperty("IdItemNavigation")]
    public virtual ICollection<TmpRegistroStockEntity> TmpRegistroStockEntity { get; set; } = new List<TmpRegistroStockEntity>();
}
