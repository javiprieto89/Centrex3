using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("produccion_asocItems")]
public partial class ProduccionAsocItemEntity
{
    [Column("id_produccion")]
    public int IdProduccion { get; set; }

    [Column("id_item")]
    public int IdItem { get; set; }

    [Column("id_item_asoc")]
    public int IdItemAsoc { get; set; }

    [Column("cantidad_item_asoc_enviada")]
    public int CantidadItemAsocEnviada { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey("IdItem, IdItemAsoc")]
    [InverseProperty("ProduccionAsocItemEntity")]
    public virtual AsocItemEntity AsocItemEntity { get; set; } = null!;

    [ForeignKey("IdProduccion")]
    [InverseProperty("ProduccionAsocItemEntity")]
    public virtual ProduccionEntity IdProduccionNavigation { get; set; } = null!;
}
