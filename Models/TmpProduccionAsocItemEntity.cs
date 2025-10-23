using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("tmpproduccion_asocItems")]
public partial class TmpProduccionAsocItemEntity
{
    [Key]
    [Column("id_tmpProduccion_asocItem")]
    public int IdTmpProduccionAsocItem { get; set; }

    [Column("id_tmpProduccionItem")]
    public int IdTmpProduccionItem { get; set; }

    [Column("id_produccionItem")]
    public int? IdProduccionItem { get; set; }

    [Column("id_produccion")]
    public int? IdProduccion { get; set; }

    [Column("id_item")]
    public int IdItem { get; set; }

    [Column("id_item_asoc")]
    public int IdItemAsoc { get; set; }

    [Column("cantidad_item_asoc_enviada")]
    public int CantidadItemAsocEnviada { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [ForeignKey("IdItem, IdItemAsoc")]
    [InverseProperty("TmpProduccionAsocItemEntity")]
    public virtual AsocItemEntity AsocItemEntity { get; set; } = null!;

    [ForeignKey("IdProduccionItem")]
    [InverseProperty("TmpProduccionAsocItemEntity")]
    public virtual ProduccionItemEntity? IdProduccionItemNavigation { get; set; }

    [ForeignKey("IdProduccion")]
    [InverseProperty("TmpProduccionAsocItemEntity")]
    public virtual ProduccionEntity? IdProduccionNavigation { get; set; }

    [ForeignKey("IdTmpProduccionItem")]
    [InverseProperty("TmpProduccionAsocItemEntity")]
    public virtual TmpProduccionItemEntity IdTmpProduccionItemNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("TmpProduccionAsocItemEntity")]
    public virtual UsuarioEntity IdUsuarioNavigation { get; set; } = null!;
}
