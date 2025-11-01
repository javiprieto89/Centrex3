using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("usuarios")]
public partial class UsuarioEntity
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("usuario")]
    [StringLength(20)]
    public string Usuario { get; set; } = null!;

    [Column("password")]
    [StringLength(50)]
    public string Password { get; set; } = null!;

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [Column("activo")]
    public bool Activo { get; set; }

    [Column("logueado")]
    public bool Logueado { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<PedidoEntity> PedidoEntity { get; set; } = new List<PedidoEntity>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<ProduccionEntity> ProduccionEntity { get; set; } = new List<ProduccionEntity>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<TmpOcItemEntity> TmpOcItemEntity { get; set; } = new List<TmpOcItemEntity>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<TmpProduccionAsocItemEntity> TmpProduccionAsocItemEntity { get; set; } = new List<TmpProduccionAsocItemEntity>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<TmpProduccionItemEntity> TmpProduccionItemEntity { get; set; } = new List<TmpProduccionItemEntity>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<UsuarioPerfilEntity> UsuarioPerfilEntity { get; set; } = new List<UsuarioPerfilEntity>();
}
