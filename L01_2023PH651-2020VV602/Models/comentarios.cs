using System.ComponentModel.DataAnnotations;

namespace L01_2023PH651_2020VV602.Models
{
    public class comentarios
    {
        [Key]
        public int cometarioId { get; set; }
        public int? publicacionId { get; set; }
        public string comentario { get; set; }
        public int? usuarioId { get; set; }
    }
}
