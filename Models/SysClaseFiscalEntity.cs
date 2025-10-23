using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("sys_ClasesFiscales")]
public partial class SysClaseFiscalEntity
{
    [Key]
    [Column("id_claseFiscal")]
    public int IdClaseFiscal { get; set; }

    [Column("descript")]
    [StringLength(100)]
    public string Descript { get; set; } = null!;

    [InverseProperty("IdClaseFiscalNavigation")]
    public virtual ICollection<ClienteEntity> ClienteEntity { get; set; } = new List<ClienteEntity>();

    [InverseProperty("IdClaseFiscalNavigation")]
    public virtual ICollection<ProveedorEntity> ProveedorEntity { get; set; } = new List<ProveedorEntity>();
}
