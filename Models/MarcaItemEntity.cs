using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("marcas_items")]
[Index("Marca", Name = "IX_marcas_items_1", IsUnique = true)]
public partial class MarcaItemEntity
{
    [Key]
    [Column("id_marca")]
    public int IdMarca { get; set; }

    [Column("marca")]
    [StringLength(50)]
    public string Marca { get; set; } = null!;

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("IdMarcaNavigation")]
    public virtual ICollection<ItemEntity> ItemEntity { get; set; } = new List<ItemEntity>();
}
