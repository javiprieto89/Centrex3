using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[PrimaryKey("IdPermiso", "IdPefil")]
[Table("permisos_perfiles")]
public partial class PermisoPerfilEntity
{
    [Key]
    [Column("id_permiso")]
    public int IdPermiso { get; set; }

    [Key]
    [Column("id_pefil")]
    public int IdPefil { get; set; }

    [ForeignKey("IdPefil")]
    [InverseProperty("PermisoPerfilEntity")]
    public virtual PerfilEntity IdPefilNavigation { get; set; } = null!;

    [ForeignKey("IdPermiso")]
    [InverseProperty("PermisoPerfilEntity")]
    public virtual PermisoEntity IdPermisoNavigation { get; set; } = null!;
}
