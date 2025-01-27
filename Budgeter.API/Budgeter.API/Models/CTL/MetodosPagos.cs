using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budgeter.API.Models.CTL
{
    [Table("MetodosPago")]
    public class MetodosPagos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 IdMetodoPago { get; set; }
        public String MetodoPago { get; set; }
        public String Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}