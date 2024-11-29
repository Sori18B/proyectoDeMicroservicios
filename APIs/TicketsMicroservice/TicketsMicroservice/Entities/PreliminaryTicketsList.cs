namespace TicketsMicroservice.Entities
{
    public class PreliminaryTicketsList
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ClientID { get; set; }
        public DateTime? CreationDate { get; set; }
        public int PreliminaryID { get; set; }
    }
}
