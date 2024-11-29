namespace TicketsMicroservice.Entities
{
    public class CreateTicketModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int StatusID { get; set; }
        public int PriorityID { get; set; }
        public int ClientID { get; set; }
        public int RepID { get; set; }
    }
}
