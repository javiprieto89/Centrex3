using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("consultas_personalizadas")]
public partial class ConsultaPersonalizadaEntity
{
    [Key]
    [Column("id_consulta")]
    public int IdConsulta { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("consulta")]
    public string Consulta { get; set; } = null!;

    [Column("activo")]
    public bool Activo { get; set; }
}
