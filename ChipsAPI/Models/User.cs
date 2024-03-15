using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChipsAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(maximumLength:20)]
        public required string Nombre { get; set; }


        [StringLength(maximumLength: 20)]

        public string? Apellidos { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo Email no tiene un formato válido.")]
        [StringLength(maximumLength: 40)]
        //[Index(IsUnique = true)] obsoleto, no usar...
        public required string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo Email debe tener entre 6 y 20 caracteres.")]
        public required string Password { get; set; }
        public string? Perfil { get; set; }
        public DateTime? LastLogin { get; set; }

    }
}
