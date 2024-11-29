using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using TicketsMicroservice.Entities;

namespace TicketsMicroservice.Repositories
{
    public class TicketsRepository : ITickets
    {
        private readonly string connectionString;
        public TicketsRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("CadenaSQL");
        }

        //Metodos <>
        public async Task<IEnumerable<TicketListModelWithTicketID>> TicketList(int RepID)
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "TicketList";
            var parameters = new DynamicParameters();
            parameters.Add("@RepID", RepID);

            try
            {
                var tickets = await connection.QueryAsync<TicketListModelWithTicketID>(procedure,parameters, commandType: CommandType.StoredProcedure);
                return tickets;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar la lista de tickets {ex.Message}");
                throw;
            }
        }

        public async Task<TicketListModel> TicketListById(int TicketID)
        {
            using var connection = new SqlConnection(connectionString);

            var procedure = "TicketListById";

            var parameters = new DynamicParameters();
            parameters.Add("@TicketID", TicketID);

            try
            {
                var ticket = await connection.QueryFirstOrDefaultAsync<TicketListModel>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return ticket;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar la lista {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<TicketListModel>> TicketListByStatus(int StatusID)
        {
            using var connection = new SqlConnection( connectionString);

            var procedure = "TicketListByStatus";
            var parameters = new DynamicParameters();
            parameters.Add("@StatusID", StatusID);

            try
            {
                var List = await connection.QueryAsync<TicketListModel>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return List;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar la lista {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<TicketListModel>> TicketListByPriority(int PriorityID)
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "TicketListByPriority";

            var parameters = new DynamicParameters();
            parameters.Add("@PriorityID", PriorityID);

            try
            {
                var List = await connection.QueryAsync<TicketListModel>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return List;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar la lista {ex.Message}");
                throw;
            }
        }
        public async Task<IEnumerable<TicketListModel>> TicketListByRepresentative(int RepID)
        {
            using var connection = new SqlConnection(connectionString);

            var procedure = "TicketListByRepresentative";

            var parameters = new DynamicParameters();
            parameters.Add("@RepID", RepID);

            try
            {
                var List = await connection.QueryAsync<TicketListModel>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return List;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar la lista {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<TicketHistoryListModel>> TicketHistoryList(int TicketID)
        {
            using var connection = new SqlConnection( connectionString);
            var procedure = "TicketHistoryList";

            var parameters = new DynamicParameters();
            parameters.Add("@TicketID", TicketID);

            try
            {
                var List = await connection.QueryAsync<TicketHistoryListModel>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return List;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar la lista {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<TicketListModel>> TicketAssignmentList(int RepID)
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "TicketAssignmentList";

            var parameters = new DynamicParameters();
            parameters.Add("@RepID", RepID);

            try
            {
                var List = await connection.QueryAsync<TicketListModel>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return List;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar la lista {ex.Message}");
                throw;
            }
        }

        public async Task<int> TicketsTotal(int RepID)
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "TicketsTotal";

            var parameters = new DynamicParameters();
            parameters.Add("@RepID", RepID);

            try
            {
                var Total = await connection.QuerySingleAsync<int>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return Total;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar el total {ex.Message}");
                throw;
            }
        }

        public async Task<int> OpenTicketsTotal(int RepID)
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "OpenTicketsTotal";

            var parameters = new DynamicParameters();
            parameters.Add("@RepID", RepID);

            try
            {
                var Total = await connection.QuerySingleAsync<int>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return Total;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar el total {ex.Message}");
                throw;
            }
        }

        public async Task<int> TicketsIProcessTotal(int RepID)
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "TicketsInProcess";

            var parameters = new DynamicParameters();
            parameters.Add("@RepID", RepID);

            try
            {
                var Total = await connection.QuerySingleAsync<int>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return Total;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar el total {ex.Message}");
                throw;
            }
        }


        public async Task<int> ClosedTicketsTotal(int RepID)
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "ClosedTickets";

            var parameters = new DynamicParameters();
            parameters.Add("@RepID", RepID);

            try
            {
                var Total = await connection.QuerySingleAsync<int>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return Total;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar el total {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<PreliminaryTicketsList>> PreliminaryTicketsList()
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "PreliminaryTicketsList";
            var parameters = new DynamicParameters();

            try
            {
                var List = await connection.QueryAsync<PreliminaryTicketsList>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return List;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar la lista {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<PreliminaryTicketsListById>> PreliminaryTicketById(int PreliminaryID)
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "PreliminaryTicketById";

            var parameters = new DynamicParameters();
            parameters.Add("@PreliminaryID", PreliminaryID);

            try
            {
                var List = await connection.QueryAsync<PreliminaryTicketsListById>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return List;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar la lista {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<MisTicketsModel>> TicketListByClientID(int ClientID)
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "TicketListByClientID";

            var parameters = new DynamicParameters();
            parameters.Add("@ClientID", ClientID);

            try
            {
                var List = await connection.QueryAsync<MisTicketsModel>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return List;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar la lista {ex.Message}");
                throw;
            }
        }

        //POOOOOST

        public async Task CreatePreliminaryTicket(CreatePreliminaryTicket model)
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "CreatePreliminaryTicket";

            var parameters = new DynamicParameters();
            parameters.Add("@Title", model.Title);
            parameters.Add("@Description", model.Description);
            parameters.Add("@ClientID", model.ClientID);

            try
            {
                await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo crear el ticket preliminar {ex.Message}");
                throw;
            }
        }

        public async Task CompleteTicket(CompleteTicketModel model)
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "CompleteTicket";

            var parameters = new DynamicParameters();
            parameters.Add("@PreliminaryID", model.PreliminaryID);
            parameters.Add("@StatusID", model.StatusID);
            parameters.Add("@PriorityID", model.PriorityID);
            parameters.Add("@RepID", model.RepID);


            try
            {
                await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo crear el ticket preliminar {ex.Message}");
                throw;
            }
        }

        public async Task UpdateTicket(UpdateTicketModel model)
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "UpdateTicket";

            var parameters = new DynamicParameters();
            parameters.Add("@TicketID", model.TicketID);
            parameters.Add("@StatusID", model.StatusID);
            parameters.Add("@PriorityID", model.PriorityID);
            parameters.Add("@UserID", model.UserID);
            parameters.Add("@ChangeDescription", model.ChangeDescription);

            try
            {
                await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo actualizar el ticket {ex.Message}");
                throw;
            }
        }

        public async Task UpdateRepAssign(UpdateRepAssignModel model)
        {
            using var connection = new SqlConnection( connectionString);
            var procedure = "UpdateRepAssign";

            var parameters = new DynamicParameters();
            parameters.Add("@RepID", model.RepID);
            parameters.Add("@TicketID", model.TicketID);


            try
            {
                await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo actualizar el representante asignado {ex.Message}");
                throw;
            }
        }

        public async Task ReopenTicket(ReopenTicketModel model)
        {
            using var connection = new SqlConnection(connectionString);
            var procedure = "ReopenTicket";

            var parameters = new DynamicParameters();
            parameters.Add("@TicketID", model.TicketID);
            parameters.Add("@UserID", model.UserID);

            try
            {
                await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo reabrir el ticket {ex.Message}");
                throw;
            }
        }
    }
}
