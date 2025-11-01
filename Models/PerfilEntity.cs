using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("perfiles")]
public partial class PerfilEntity
{
    [Key]
    [Column("id_perfil")]
    public int IdPerfil { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("IdPefilNavigation")]
    public virtual ICollection<PermisoPerfilEntity> PermisoPerfilEntity { get; set; } = new List<PermisoPerfilEntity>();

    [InverseProperty("IdPerfilNavigation")]
    public virtual ICollection<UsuarioPerfilEntity> UsuarioPerfilEntity { get; set; } = new List<UsuarioPerfilEntity>();
}
