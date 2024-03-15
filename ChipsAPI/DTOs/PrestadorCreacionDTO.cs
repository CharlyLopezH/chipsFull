using System.ComponentModel.DataAnnotations;

namespace ChipsAPI.DTOs
{
    public class PrestadorCreacionDTO
    {       
        public int Id { get; set; }

        [Required (ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:50)]
        public required string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public required string PrimerAp { get; set; }
        public string? SegundoAp { get; set; }
        public required TimeOnly HoraEntrada { get; set; }
        public required TimeOnly HoraSalida { get; set; }
    }
}
