﻿namespace TicketsMicroservice.Entities
{
    public class TicketResponseModel
    {
        public int TicketID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? StatusName { get; set; }
        public string? PriorityName { get; set; }
        public string? ChangeDescription { get; set; }
        public UserModel? Client { get; set; }
        public UserModel? Representative { get; set; }
    }
}
