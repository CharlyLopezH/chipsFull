namespace ChipsAPI.DTOs
{
    public class PrestadorDTO
    {
        public int Id { get; set; }
        public required string Nombres { get; set; }
        public required string PrimerAp { get; set; }
        public string? SegundoAp { get; set; }
        public required TimeOnly HoraEntrada { get; set; }
        public required TimeOnly HoraSalida { get; set; }
        public  string? Email { get; set; }
        public string? Apellidos { get; set; }
    }
}
