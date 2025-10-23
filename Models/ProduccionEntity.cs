using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("produccion")]
public partial class ProduccionEntity
{
    [Key]
    [Column("id_produccion")]
    public int IdProduccion { get; set; }

    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [Column("fecha_carga")]
    public DateOnly FechaCarga { get; set; }

    [Column("fecha_envio")]
    public DateOnly? FechaEnvio { get; set; }

    [Column("fecha_recepcion")]
    public DateOnly? FechaRecepcion { get; set; }

    [Column("enviado")]
    public bool? Enviado { get; set; }

    [Column("recibido")]
    public bool? Recibido { get; set; }

    [Column("notas")]
    public string? Notas { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [ForeignKey("IdProveedor")]
    [InverseProperty("ProduccionEntity")]
    public virtual ProveedorEntity IdProveedorNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("ProduccionEntity")]
    public virtual UsuarioEntity IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdProduccionNavigation")]
    public virtual ICollection<ProduccionAsocItemEntity> ProduccionAsocItemEntity { get; set; } = new List<ProduccionAsocItemEntity>();

    [InverseProperty("IdProduccionNavigation")]
    public virtual ICollection<ProduccionItemEntity> ProduccionItemEntity { get; set; } = new List<ProduccionItemEntity>();

    [InverseProperty("IdProduccionNavigation")]
    public virtual ICollection<TmpProduccionAsocItemEntity> TmpProduccionAsocItemEntity { get; set; } = new List<TmpProduccionAsocItemEntity>();

    [InverseProperty("IdProduccionNavigation")]
    public virtual ICollection<TmpProduccionItemEntity> TmpProduccionItemEntity { get; set; } = new List<TmpProduccionItemEntity>();
}
