using System.ComponentModel.DataAnnotations;

namespace Asteroides.Models
{
    public class AsteroideDto
    {
        /// <summary>
        /// Nombre del Asteroide
        /// </summary>
        [Required]
        public string? Nombre { get; set; }
        /// <summary>
        /// Longitud del diametro medio del asteroide
        /// </summary>
        [Required]
        public double DiametroMetros { get; set; }
        /// <summary>
        /// Velocidad a la que va el asteroide en km/h
        /// </summary>
        [Required]
        public string? Velocidad { get; set; }
        /// <summary>
        /// Fecha que pasara el asteroide por la tierra
        /// </summary>
        [Required]
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Planeta por el que pasara el metorito
        /// </summary>
        [Required]
        public string? Planeta { get; set; }
    }
}
