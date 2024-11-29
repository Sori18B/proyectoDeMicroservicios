namespace TicketsMicroservice.Entities
{
    public class Tickets
    {
        public int TicketID {get; set;}
        public string? Title {get; set;}
        public string? Description {get; set;}
        public DateTime CreationDate {get; set;}
        public DateTime UpdateDate { get; set; }
        public int StatusID {get; set;}
        public int PriorityID {get; set;}
        public int ClientID { get; set; }
        public int RepID { get; set; }


    }
}
