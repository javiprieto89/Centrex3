using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("sys_modoMiPyme")]
public partial class SysModoMiPymeEntity
{
    [Key]
    [Column("id_modoMiPyme")]
    public int IdModoMiPyme { get; set; }

    [Column("modo")]
    [StringLength(100)]
    public string Modo { get; set; } = null!;

    [Column("abreviatura")]
    [StringLength(10)]
    public string Abreviatura { get; set; } = null!;

    [InverseProperty("IdModoMiPymeNavigation")]
    public virtual ICollection<ComprobanteEntity> ComprobanteEntity { get; set; } = new List<ComprobanteEntity>();
}
