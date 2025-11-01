using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("tmpproduccion_items")]
public partial class TmpProduccionItemEntity
{
    [Key]
    [Column("id_tmpProduccionItem")]
    public int IdTmpProduccionItem { get; set; }

    [Column("id_produccionItem")]
    public int? IdProduccionItem { get; set; }

    [Column("id_produccion")]
    public int? IdProduccion { get; set; }

    [Column("id_item")]
    public int? IdItem { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("activo")]
    public bool? Activo { get; set; }

    [Column("descript")]
    [StringLength(100)]
    public string? Descript { get; set; }

    [Column("id_item_recibido")]
    public int? IdItemRecibido { get; set; }

    [Column("cantidad_recibida")]
    public int? CantidadRecibida { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [ForeignKey("IdItem")]
    [InverseProperty("TmpProduccionItemEntity")]
    public virtual ItemEntity? IdItemNavigation { get; set; }

    [ForeignKey("IdItemRecibido")]
    [InverseProperty("TmpProduccionItemRecibidoEntity")]
    public virtual ItemEntity? IdItemRecibidoNavigation { get; set; }

    [ForeignKey("IdProduccion")]
    [InverseProperty("TmpProduccionItemEntity")]
    public virtual ProduccionEntity? IdProduccionNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("TmpProduccionItemEntity")]
    public virtual UsuarioEntity IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdTmpProduccionItemNavigation")]
    public virtual ICollection<TmpProduccionAsocItemEntity> TmpProduccionAsocItemEntity { get; set; } = new List<TmpProduccionAsocItemEntity>();
}
