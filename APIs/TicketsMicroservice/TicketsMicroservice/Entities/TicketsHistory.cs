namespace TicketsMicroservice.Entities
{
    public class TicketsHistory
    {
        public int HistoryID { get; set; }
        public int TicketID { get; set; } 
        public DateTime EntryDate { get; set; }
        public string? ChangeDescription { get; set; }
        public int UserID { get; set; }
    }
}
