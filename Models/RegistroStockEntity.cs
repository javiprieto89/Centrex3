using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("registros_stock")]
public partial class RegistroStockEntity
{
    [Key]
    [Column("id_registro")]
    public int IdRegistro { get; set; }

    [Column("id_ingreso")]
    public int IdIngreso { get; set; }

    [Column("fecha")]
    public DateOnly? Fecha { get; set; }

    [Column("fecha_ingreso")]
    public DateOnly FechaIngreso { get; set; }

    [Column("factura")]
    [StringLength(50)]
    public string? Factura { get; set; }

    [Column("archivofc", TypeName = "image")]
    public byte[]? Archivofc { get; set; }

    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [Column("id_item")]
    public int IdItem { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("costo", TypeName = "decimal(18, 2)")]
    public decimal Costo { get; set; }

    [Column("precio_lista", TypeName = "decimal(18, 2)")]
    public decimal PrecioLista { get; set; }

    [Column("factor", TypeName = "decimal(18, 2)")]
    public decimal Factor { get; set; }

    [Column("cantidad_anterior")]
    public int CantidadAnterior { get; set; }

    [Column("costo_anterior", TypeName = "decimal(18, 2)")]
    public decimal CostoAnterior { get; set; }

    [Column("precio_lista_anterior", TypeName = "decimal(18, 2)")]
    public decimal PrecioListaAnterior { get; set; }

    [Column("factor_anterior", TypeName = "decimal(18, 2)")]
    public decimal FactorAnterior { get; set; }

    [Column("nota")]
    public string? Nota { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [ForeignKey("IdItem")]
    [InverseProperty("RegistroStockEntity")]
    public virtual ItemEntity IdItemNavigation { get; set; } = null!;

    [ForeignKey("IdProveedor")]
    [InverseProperty("RegistroStockEntity")]
    public virtual ProveedorEntity IdProveedorNavigation { get; set; } = null!;
}
