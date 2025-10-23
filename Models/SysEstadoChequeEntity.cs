using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("sysestados_cheques")]
public partial class SysEstadoChequeEntity
{
    [Key]
    [Column("id_estadoch")]
    public int IdEstadoch { get; set; }

    [Column("estado")]
    [StringLength(50)]
    public string Estado { get; set; } = null!;

    [InverseProperty("IdEstadochNavigation")]
    public virtual ICollection<ChequeEntity> ChequeEntity { get; set; } = new List<ChequeEntity>();
}
