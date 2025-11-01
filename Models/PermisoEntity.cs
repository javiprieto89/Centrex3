using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("permisos")]
public partial class PermisoEntity
{
    [Key]
    [Column("id_permiso")]
    public int IdPermiso { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdPermisoNavigation")]
    public virtual ICollection<PermisoPerfilEntity> PermisoPerfilEntity { get; set; } = new List<PermisoPerfilEntity>();
}
