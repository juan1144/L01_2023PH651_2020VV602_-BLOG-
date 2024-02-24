using System.ComponentModel.DataAnnotations;

namespace L01_2023PH651_2020VV602.Models
{
    public class publicaciones
    {
        [Key]
        public int publicacionId { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int? usuarioId { get; set; }
    }
}
