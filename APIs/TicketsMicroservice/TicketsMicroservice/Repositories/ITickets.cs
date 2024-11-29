using TicketsMicroservice.Entities;

namespace TicketsMicroservice.Repositories
{
    public interface ITickets
    {
        //Métodos GET <>
        Task<IEnumerable<TicketListModelWithTicketID>> TicketList(int RepID);
        Task<TicketListModel> TicketListById(int TicketID);
        Task<IEnumerable<TicketListModel>> TicketListByStatus(int StatusID);
        Task<IEnumerable<TicketListModel>> TicketListByPriority(int PriorityID);
        Task<IEnumerable<TicketListModel>> TicketListByRepresentative(int RepID);
        Task<IEnumerable<TicketHistoryListModel>> TicketHistoryList(int TicketID);
        Task<IEnumerable<TicketListModel>> TicketAssignmentList(int RepID);
        Task<int> TicketsTotal(int RepID);
        Task<int> OpenTicketsTotal(int RepID);
        Task<int> TicketsIProcessTotal(int RepID);
        Task<int> ClosedTicketsTotal(int RepID);
        Task<IEnumerable<PreliminaryTicketsList>> PreliminaryTicketsList();
        Task<IEnumerable<PreliminaryTicketsListById>> PreliminaryTicketById(int PreliminaryID);
        Task<IEnumerable<MisTicketsModel>> TicketListByClientID(int ClientID);


        //Métodos POST
        Task CreatePreliminaryTicket(CreatePreliminaryTicket model);
        Task CompleteTicket(CompleteTicketModel model);

        //Métodos PUT
        Task UpdateTicket(UpdateTicketModel model);
        Task UpdateRepAssign(UpdateRepAssignModel model);
        Task ReopenTicket(ReopenTicketModel model);

    }
}
