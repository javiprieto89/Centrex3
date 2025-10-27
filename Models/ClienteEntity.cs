using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("clientes")]
public partial class ClienteEntity
{
    [Key]
    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("taxNumber")]
    [StringLength(50)]
    public string? TaxNumber { get; set; }

    [Column("razon_social")]
    [StringLength(100)]
    public string RazonSocial { get; set; } = null!;

    [Column("nombre_fantasia")]
    [StringLength(100)]
    public string? NombreFantasia { get; set; }

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

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<CcClienteEntity> CcClienteEntity { get; set; } = new List<CcClienteEntity>();

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<ChequeEntity> ChequeEntity { get; set; } = new List<ChequeEntity>();

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<CobroEntity> CobroEntity { get; set; } = new List<CobroEntity>();

    [ForeignKey("IdClaseFiscal")]
    [InverseProperty("ClienteEntity")]
    public virtual SysClaseFiscalEntity? IdClaseFiscalNavigation { get; set; }

    [ForeignKey("IdProvinciaEntrega")]
    [InverseProperty("ClienteEntityIdProvinciaEntregaNavigation")]
    public virtual ProvinciaEntity? IdProvinciaEntregaNavigation { get; set; }

    [ForeignKey("IdProvinciaFiscal")]
    [InverseProperty("ClienteEntityIdProvinciaFiscalNavigation")]
    public virtual ProvinciaEntity? IdProvinciaFiscalNavigation { get; set; }

    [ForeignKey("IdTipoDocumento")]
    [InverseProperty("ClienteEntity")]
    public virtual TipoDocumentoEntity IdTipoDocumentoNavigation { get; set; } = null!;

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<PedidoEntity> PedidoEntity { get; set; } = new List<PedidoEntity>();

    [ForeignKey("IdPaisFiscal")]
    [InverseProperty("ClienteEntityIdPaisFiscalNavigation")]
    public virtual PaisEntity? IdPaisFiscalNavigation { get; set; }

    [ForeignKey("IdPaisEntrega")]
    [InverseProperty("ClienteEntityIdPaisEntregaNavigation")]
    public virtual PaisEntity? IdPaisEntregaNavigation { get; set; }
}
