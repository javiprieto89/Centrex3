using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("tipos_items")]
[Index("Tipo", Name = "IX_tipos_items", IsUnique = true)]
public partial class TipoItemEntity
{
    [Key]
    [Column("id_tipo")]
    public int IdTipo { get; set; }

    [Column("tipo")]
    [StringLength(50)]
    public string Tipo { get; set; } = null!;

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("IdTipoNavigation")]
    public virtual ICollection<ItemEntity> ItemEntity { get; set; } = new List<ItemEntity>();
}
