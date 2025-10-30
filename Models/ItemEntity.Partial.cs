using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models
{
    public partial class ItemEntity
    {
        [NotMapped] // ✅ EF no la persiste en la base de datos
        public int IdTmpPedidoItem { get; set; }
    }
}