using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("produccion_items")]
public partial class ProduccionItemEntity
{
    [Key]
    [Column("id_produccionItem")]
    public int IdProduccionItem { get; set; }

    [Column("id_produccion")]
    public int IdProduccion { get; set; }

    [Column("id_item")]
    public int IdItem { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("id_item_recibido")]
    public int? IdItemRecibido { get; set; }

    [Column("cantidad_recibida")]
    public int? CantidadRecibida { get; set; }

    [Column("descript")]
    [StringLength(100)]
    public string? Descript { get; set; }

    [Column("activo")]
    public bool? Activo { get; set; }

    [ForeignKey("IdItem")]
    [InverseProperty("ProduccionItemEntityIdItemNavigation")]
    public virtual ItemEntity IdItemNavigation { get; set; } = null!;

    [ForeignKey("IdItemRecibido")]
    [InverseProperty("ProduccionItemEntityIdItemRecibidoNavigation")]
    public virtual ItemEntity? IdItemRecibidoNavigation { get; set; }

    [ForeignKey("IdProduccion")]
    [InverseProperty("ProduccionItemEntity")]
    public virtual ProduccionEntity IdProduccionNavigation { get; set; } = null!;

    [InverseProperty("IdProduccionItemNavigation")]
    public virtual ICollection<TmpProduccionAsocItemEntity> TmpProduccionAsocItemEntity { get; set; } = new List<TmpProduccionAsocItemEntity>();
}
