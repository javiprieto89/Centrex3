using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("empresa")]
public partial class EmpresaEntity
{
    [Key]
    [Column("id_empresa")]
    public int IdEmpresa { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("direccion")]
    public string Direccion { get; set; } = null!;

    [Column("cuit")]
    [StringLength(50)]
    public string Cuit { get; set; } = null!;

    [Column("ingresos_brutos")]
    [StringLength(50)]
    public string? IngresosBrutos { get; set; }

    [Column("inicio_actividades")]
    [StringLength(10)]
    public string? InicioActividades { get; set; }

    [Column("logo", TypeName = "image")]
    public byte[]? Logo { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }
}
