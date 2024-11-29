namespace TicketsMicroservice.Entities
{
    public class PreliminaryTicketsListById
    {
        public int PreliminaryID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ClientID { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
