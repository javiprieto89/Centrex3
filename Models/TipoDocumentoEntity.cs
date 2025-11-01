using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("tipos_documentos")]
public partial class TipoDocumentoEntity
{
    [Key]
    [Column("id_tipoDocumento")]
    public int IdTipoDocumento { get; set; }

    [Column("documento")]
    [StringLength(50)]
    public string Documento { get; set; } = null!;

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("IdTipoDocumentoNavigation")]
    public virtual ICollection<ClienteEntity> ClienteEntity { get; set; } = new List<ClienteEntity>();

    [InverseProperty("IdTipoDocumentoNavigation")]
    public virtual ICollection<ProveedorEntity> ProveedorEntity { get; set; } = new List<ProveedorEntity>();
}
