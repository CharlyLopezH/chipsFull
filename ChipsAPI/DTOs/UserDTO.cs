using System.ComponentModel.DataAnnotations;

namespace ChipsAPI.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public string? Apellidos { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
        public string? Perfil { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
