using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budgeter.API.Models.ADM
{
    [Table("Usuarios")]
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 IdUsuario { get; set; }
        public String Usuario { get; set; }
        public String Correo { get; set; }
        public String Contrasenia { get; set; }
        public Boolean EstadoUsuario { get; set; }
        public Int32 IntentosLogin { get; set; }
        public DateTime? UltimoAcceso { get; set; }
    }
}