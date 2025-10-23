using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("tipos_comprobantes")]
public partial class TipoComprobanteEntity
{
    [Key]
    [Column("id_tipoComprobante")]
    public int IdTipoComprobante { get; set; }

    [Column("comprobante_AFIP")]
    [StringLength(100)]
    public string ComprobanteAfip { get; set; } = null!;

    [Column("id_claseFiscal")]
    [StringLength(100)]
    public string? IdClaseFiscal { get; set; }

    [Column("signoProveedor")]
    [StringLength(1)]
    [Unicode(false)]
    public string? SignoProveedor { get; set; }

    [Column("signoCliente")]
    [StringLength(1)]
    [Unicode(false)]
    public string? SignoCliente { get; set; }

    [Column("discriminaIVA")]
    public bool? DiscriminaIva { get; set; }

    [Column("esRemito")]
    public bool? EsRemito { get; set; }

    [Column("nombreAbreviado")]
    [StringLength(10)]
    public string? NombreAbreviado { get; set; }

    [Column("id_claseComprobante")]
    public int IdClaseComprobante { get; set; }

    [Column("id_anulaTipoComprobante")]
    public int? IdAnulaTipoComprobante { get; set; }

    [Column("contabilizar")]
    public bool Contabilizar { get; set; }

    [InverseProperty("IdTipoComprobanteNavigation")]
    public virtual ICollection<ComprobanteCompraEntity> ComprobanteCompraEntity { get; set; } = new List<ComprobanteCompraEntity>();

    [InverseProperty("IdTipoComprobanteNavigation")]
    public virtual ICollection<ComprobanteEntity> ComprobanteEntity { get; set; } = new List<ComprobanteEntity>();

    [ForeignKey("IdClaseComprobante")]
    [InverseProperty("TipoComprobanteEntity")]
    public virtual SysClaseComprobanteEntity IdClaseComprobanteNavigation { get; set; } = null!;

    [InverseProperty("IdTipoComprobanteNavigation")]
    public virtual ICollection<TransaccionEntity> TransaccionEntity { get; set; } = new List<TransaccionEntity>();
}
