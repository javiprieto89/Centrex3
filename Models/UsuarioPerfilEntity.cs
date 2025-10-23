using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[PrimaryKey("IdUsuario", "IdPerfil")]
[Table("usuarios_perfiles")]
public partial class UsuarioPerfilEntity
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Key]
    [Column("id_perfil")]
    public int IdPerfil { get; set; }

    [ForeignKey("IdPerfil")]
    [InverseProperty("UsuarioPerfilEntity")]
    public virtual PerfilEntity IdPerfilNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("UsuarioPerfilEntity")]
    public virtual UsuarioEntity IdUsuarioNavigation { get; set; } = null!;
}
