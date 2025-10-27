using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models
{
    public partial class TmpOcItemEntity
    {
        [Column("id_item_recibido")]
        public int? IdItemRecibido { get; set; }
    }
}
