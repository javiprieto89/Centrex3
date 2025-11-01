using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("sys_claseComprobante")]
public partial class SysClaseComprobanteEntity
{
    [Key]
    [Column("id_claseComprobante")]
    public int IdClaseComprobante { get; set; }

    [Column("descript")]
    [StringLength(100)]
    public string Descript { get; set; } = null!;

    [InverseProperty("IdClaseComprobanteNavigation")]
    public virtual ICollection<TipoComprobanteEntity> TipoComprobanteEntity { get; set; } = new List<TipoComprobanteEntity>();
}
