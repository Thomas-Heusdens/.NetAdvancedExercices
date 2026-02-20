namespace FormData.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public required string Besteller { get; set; }
        public required string Name { get; set; }
        public required string Kaas {  get; set; }
        public required string BetaalWijze { get; set; }
    }
}
