using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChipsAPI.Models
{
    public class Prestador
    {
        public int Id { get; set; }
        public required string Nombres { get; set; }
        public required string PrimerAp { get; set; }
        public string? SegundoAp { get; set; }
        public required TimeOnly HoraEntradaBase { get; set; }
        public required TimeOnly HoraSalidaBase { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo Email no tiene un formato válido.")]
        [StringLength(maximumLength: 60)]        
        public required string Email { get; set; }

    }
}
