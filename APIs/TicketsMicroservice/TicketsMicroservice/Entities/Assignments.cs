namespace TicketsMicroservice.Entities
{
    public class Assignments
    {
        public int AssignmentID {get; set;}
        public int TicketID {get; set;}
        public int RepID { get; set; }
        public DateTime AssignmentDate {get; set;}
    }
}
