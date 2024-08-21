namespace MnmChipsAPI.DTOs
{
    public class PrestadorDTO
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public required string Nombres { get; set; } = null!;
        public required string PrimerAp { get; set; } = null!;
        public string? SegundoAp { get; set; }
        public required TimeOnly HoraEntradaBase { get; set; }
        public required TimeOnly HoraSalidaBase { get; set; }
        public required string Email { get; set; }

        public string? Foto { get; set; }
    }
}
