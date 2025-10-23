using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("paises")]
public partial class PaisEntity
{
    [Key]
    [Column("id_pais")]
    public int IdPais { get; set; }

    [Column("pais")]
    [StringLength(100)]
    public string Pais { get; set; } = null!;

    [InverseProperty("IdPaisNavigation")]
    public virtual ICollection<BancoEntity> BancoEntity { get; set; } = new List<BancoEntity>();

    [InverseProperty("IdPaisNavigation")]
    public virtual ICollection<ProvinciaEntity> ProvinciaEntity { get; set; } = new List<ProvinciaEntity>();
}
