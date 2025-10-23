using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[PrimaryKey("IdItem", "IdItemAsoc")]
[Table("asocItems")]
public partial class AsocItemEntity
{
    [Key]
    [Column("id_item")]
    public int IdItem { get; set; }

    [Key]
    [Column("id_item_asoc")]
    public int IdItemAsoc { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [ForeignKey("IdItemAsoc")]
    [InverseProperty("AsocItemEntityIdItemAsocNavigation")]
    public virtual ItemEntity IdItemAsocNavigation { get; set; } = null!;

    [ForeignKey("IdItem")]
    [InverseProperty("AsocItemEntityIdItemNavigation")]
    public virtual ItemEntity IdItemNavigation { get; set; } = null!;

    [InverseProperty("AsocItemEntity")]
    public virtual ICollection<ProduccionAsocItemEntity> ProduccionAsocItemEntity { get; set; } = new List<ProduccionAsocItemEntity>();

    [InverseProperty("AsocItemEntity")]
    public virtual ICollection<TmpProduccionAsocItemEntity> TmpProduccionAsocItemEntity { get; set; } = new List<TmpProduccionAsocItemEntity>();
}
