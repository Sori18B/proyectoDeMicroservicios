namespace TicketsMicroservice.Entities
{
    public class UpdateTicketModel
    {
        public int TicketID { get; set; }
        public int StatusID { get; set; }
        public int PriorityID { get; set; }
        public int UserID { get; set; }
        public string? ChangeDescription { get; set; }
    }
}
