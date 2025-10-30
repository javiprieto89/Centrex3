using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("comprobantes")]
public partial class ComprobanteEntity
{
    [Key]
    [Column("id_comprobante")]
    public int IdComprobante { get; set; }

    [Column("comprobante")]
    [StringLength(100)]
    public string Comprobante { get; set; } = null!;

    [Column("id_tipoComprobante")]
    public int IdTipoComprobante { get; set; }

    [Column("numeroComprobante")]
    public int NumeroComprobante { get; set; }

    [Column("puntoVenta")]
    public int PuntoVenta { get; set; }

    [Column("esFiscal")]
    public bool? EsFiscal { get; set; }

    [Column("esElectronica")]
    public bool? EsElectronica { get; set; }

    [Column("esManual")]
    public bool? EsManual { get; set; }

    [Column("esPresupuesto")]
    public bool? EsPresupuesto { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [Column("testing")]
    public bool Testing { get; set; }

    [Column("maxItems")]
    public int? MaxItems { get; set; }

    [Column("comprobanteRelacionado")]
    public int? ComprobanteRelacionado { get; set; }

    [Column("esMiPyME")]
    public bool EsMiPyMe { get; set; }

    [Column("CBU_emisor")]
    [StringLength(22)]
    public string? CbuEmisor { get; set; }

    [Column("alias_CBU_emisor")]
    [StringLength(50)]
    public string? AliasCbuEmisor { get; set; }

    [Column("anula_MiPyME")]
    [StringLength(1)]
    public string? AnulaMiPyMe { get; set; }

    [Column("contabilizar")]
    public bool Contabilizar { get; set; }

    [Column("mueveStock")]
    public bool MueveStock { get; set; }

    [Column("id_modoMiPyme")]
    public int IdModoMiPyme { get; set; }
        
    [ForeignKey("IdModoMiPyme")]
    [InverseProperty("ComprobanteEntity")]
    public virtual SysModoMiPymeEntity IdModoMiPymeNavigation { get; set; } = null!;

    [ForeignKey("IdTipoComprobante")]
    [InverseProperty("ComprobanteEntity")]
    public virtual TipoComprobanteEntity IdTipoComprobanteNavigation { get; set; } = null!;

    [InverseProperty("IdComprobanteNavigation")]
    public virtual ICollection<PedidoEntity> PedidoEntity { get; set; } = new List<PedidoEntity>();

    [NotMapped]
    private string _prefijo;

    [NotMapped]
    public string Prefijo
    {
        get => !string.IsNullOrEmpty(_prefijo)
            ? _prefijo
            : (IdTipoComprobanteNavigation?.NombreAbreviado ?? "");
        set => _prefijo = value;
    }
}