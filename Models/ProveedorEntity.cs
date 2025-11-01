using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("proveedores")]
public partial class ProveedorEntity
{
    [Key]
    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [Column("taxNumber")]
    [StringLength(50)]
    public string? TaxNumber { get; set; }

    [Column("razon_social")]
    [StringLength(100)]
    public string RazonSocial { get; set; } = null!;

    [Column("contacto")]
    [StringLength(100)]
    public string? Contacto { get; set; }

    [Column("telefono")]
    [StringLength(50)]
    public string? Telefono { get; set; }

    [Column("celular")]
    [StringLength(50)]
    public string? Celular { get; set; }

    [Column("email")]
    [StringLength(100)]
    public string? Email { get; set; }

    [Column("id_provincia_fiscal")]
    public int? IdProvinciaFiscal { get; set; }

    [Column("direccion_fiscal")]
    [StringLength(200)]
    public string? DireccionFiscal { get; set; }

    [Column("localidad_fiscal")]
    [StringLength(200)]
    public string? LocalidadFiscal { get; set; }

    [Column("cp_fiscal")]
    [StringLength(20)]
    public string? CpFiscal { get; set; }

    [Column("id_provincia_entrega")]
    public int? IdProvinciaEntrega { get; set; }

    [Column("direccion_entrega")]
    [StringLength(200)]
    public string? DireccionEntrega { get; set; }

    [Column("localidad_entrega")]
    [StringLength(200)]
    public string? LocalidadEntrega { get; set; }

    [Column("cp_entrega")]
    [StringLength(20)]
    public string? CpEntrega { get; set; }

    [Column("notas")]
    public string? Notas { get; set; }

    [Column("esInscripto")]
    public bool EsInscripto { get; set; }

    [Column("vendedor")]
    [StringLength(100)]
    public string? Vendedor { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [Column("id_tipoDocumento")]
    public int IdTipoDocumento { get; set; }

    [Column("id_claseFiscal")]
    public int? IdClaseFiscal { get; set; }

    [Column("id_pais_fiscal")]
    public int? IdPaisFiscal { get; set; }

    [Column("id_pais_entrega")]
    public int? IdPaisEntrega { get; set; }

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<CcProveedorEntity> CcProveedorEntity { get; set; } = new List<CcProveedorEntity>();

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<ChequeEntity> ChequeEntity { get; set; } = new List<ChequeEntity>();

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<ComprobanteCompraEntity> ComprobanteCompraEntity { get; set; } = new List<ComprobanteCompraEntity>();

    [ForeignKey("IdClaseFiscal")]
    [InverseProperty("ProveedorEntity")]
    public virtual SysClaseFiscalEntity? IdClaseFiscalNavigation { get; set; }

    [ForeignKey("IdProvinciaEntrega")]
    [InverseProperty("ProveedorEntityIdProvinciaEntregaNavigation")]
    public virtual ProvinciaEntity? IdProvinciaEntregaNavigation { get; set; }

    [ForeignKey("IdProvinciaFiscal")]
    [InverseProperty("ProveedorEntityIdProvinciaFiscalNavigation")]
    public virtual ProvinciaEntity? IdProvinciaFiscalNavigation { get; set; }

    [ForeignKey("IdTipoDocumento")]
    [InverseProperty("ProveedorEntity")]
    public virtual TipoDocumentoEntity IdTipoDocumentoNavigation { get; set; } = null!;

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<ItemEntity> ItemEntity { get; set; } = new List<ItemEntity>();

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<OrdenCompraEntity> OrdenCompraEntity { get; set; } = new List<OrdenCompraEntity>();

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<PagoEntity> PagoEntity { get; set; } = new List<PagoEntity>();

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<ProduccionEntity> ProduccionEntity { get; set; } = new List<ProduccionEntity>();

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<RegistroStockEntity> RegistroStockEntity { get; set; } = new List<RegistroStockEntity>();

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<TmpRegistroStockEntity> TmpRegistroStockEntity { get; set; } = new List<TmpRegistroStockEntity>();

    [ForeignKey("IdPaisFiscal")]
    [InverseProperty("ProveedorEntityIdPaisFiscalNavigation")]
    public virtual PaisEntity? IdPaisFiscalNavigation { get; set; }

    [ForeignKey("IdPaisEntrega")]
    [InverseProperty("ProveedorEntityIdPaisEntregaNavigation")]
    public virtual PaisEntity? IdPaisEntregaNavigation { get; set; }
}
