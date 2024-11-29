using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TicketsMicroservice.Entities;
using TicketsMicroservice.Repositories;
using System.Net.Http.Headers;
using System.Net.Http;


namespace TicketsMicroservice.Controllers
{
    [EnableCors("ReglasCors")] //Habilitar los Cors
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TicketsController> _logger;
        public readonly ITickets _repository;

        private readonly IHttpClientFactory _httpClientFactory; // Cliente HTTP para consumir la API Users


        public TicketsController(IConfiguration configuration, ILogger<TicketsController> logger, ITickets repository, IHttpClientFactory httpClient)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientFactory = httpClient;
            _repository = repository;
        }

        //Métodos del controlador
        //[HttpGet("TicketsList")]
        //public async Task<IActionResult> TicketList()
        //{
        //    try
        //    {
        //        var list = await _repository.TicketList();
        //        return Ok(list);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"No se pudo recuperar la lista {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        [HttpGet("TicketsList/{RepID}")]
        public async Task<IActionResult> TicketList(int RepID)
        {
            try
            {

                // Obtener la lista de tickets desde el repositorio
                var tickets = await _repository.TicketList(RepID);

                // Lista para almacenar los tickets con la información complementada
                var ticketsWithUserInfo = new List<object>();

                // Crear el cliente HTTP a través de IHttpClientFactory
                var httpClient = _httpClientFactory.CreateClient();

                // Obtener el token JWT del contexto
                var token = HttpContext.Items["JWTToken"] as string;
                if (token == null)
                {
                    return Unauthorized("El jwt no ha sido encontrado.");
                }

                // Incluir el token JWT en los encabezados de autorización
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Iterar sobre cada ticket para complementar la información
                foreach (TicketListModelWithTicketID ticket in tickets)
                {
                    // Consumir el endpoint de la API Users para obtener la información del cliente
                    var clientResponse = await httpClient.GetAsync($"http://localhost:5279/api/Users/InfoClient/{ticket.ClientID}");
                    UserModel? clientInfo = null;
                    if (clientResponse.IsSuccessStatusCode && clientResponse.Content.Headers.ContentLength > 0)
                    {
                        clientInfo = await clientResponse.Content.ReadFromJsonAsync<UserModel>();
                    }

                    // Consumir el endpoint de la API Users para obtener la información del representante (si existe)
                    UserModel? repInfo = null;
                    if (ticket.RepID != 0)
                    {
                        var repResponse = await httpClient.GetAsync($"http://localhost:5279/api/Users/InfoRep/{ticket.RepID}");
                        if (repResponse.IsSuccessStatusCode && repResponse.Content.Headers.ContentLength > 0)
                        {
                            repInfo = await repResponse.Content.ReadFromJsonAsync<UserModel>();
                        }
                    }

                    // Combinar la información del ticket con la del cliente y el representante
                    var ticketWithUserInfo = new
                    {
                        ticket.TicketID ,
                        ticket.Title,
                        ticket.Description,
                        ticket.CreationDate,
                        ticket.UpdateDate,
                        ticket.StatusName,
                        ticket.PriorityName,
                        ticket.ChangeDescription,
                        Client = clientInfo, // Info del cliente
                        Representative = repInfo // Info del representante (puede ser null)
                    };

                    // Agregar el ticket con la información complementada
                    ticketsWithUserInfo.Add(ticketWithUserInfo);
                }

                return Ok(ticketsWithUserInfo); // Devolver la lista de tickets con la información complementada
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la lista de tickets {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("TicketListById/{TicketID}")]
        public async Task<IActionResult> TicketListById(int TicketID)
        {
            try
            {
                // Obtener el ticket desde el repositorio
                var ticket = await _repository.TicketListById(TicketID);

                if (ticket == null)
                {
                    return NotFound("Ticket no encontrado");
                }

                // Crear el cliente HTTP a través de IHttpClientFactory
                var httpClient = _httpClientFactory.CreateClient();

                // Obtener el token JWT del contexto
                var token = HttpContext.Items["JWTToken"] as string;
                if (token == null)
                {
                    return Unauthorized("El jwt no ha sido encontrado.");
                }

                // Incluir el token JWT en los encabezados de autorización
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Consumir el endpoint de la API Users para obtener la información del cliente
                var clientResponse = await httpClient.GetAsync($"http://localhost:5279/api/Users/InfoClient/{ticket.ClientID}");
                UserModel? clientInfo = null;
                if (clientResponse.IsSuccessStatusCode && clientResponse.Content.Headers.ContentLength > 0)
                {
                    clientInfo = await clientResponse.Content.ReadFromJsonAsync<UserModel>();
                }

                // Consumir el endpoint de la API Users para obtener la información del representante (si existe)
                UserModel? repInfo = null;
                if (ticket.RepID != 0)
                {
                    var repResponse = await httpClient.GetAsync($"http://localhost:5279/api/Users/InfoRep/{ticket.RepID}");
                    if (repResponse.IsSuccessStatusCode && repResponse.Content.Headers.ContentLength > 0)
                    {
                        repInfo = await repResponse.Content.ReadFromJsonAsync<UserModel>();
                    }
                }

                // Combinar la información del ticket con la del cliente y el representante
                var ticketWithUserInfo = new
                {
                    ticket.Title,
                    ticket.Description,
                    ticket.CreationDate,
                    ticket.UpdateDate,
                    ticket.StatusName,
                    ticket.PriorityName,
                    ticket.ChangeDescription,
                    Client = clientInfo, // Info del cliente
                    Representative = repInfo // Info del representante (puede ser null)
                };

                return Ok(ticketWithUserInfo); // Devolver el ticket con la información complementada
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar el ticket {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet("TicketListByStatus/{StatusID}")]
        public async Task<IActionResult> TicketListByStatus(int StatusID)
        {
            try
            {
                // Obtener la lista de tickets desde el repositorio según el StatusID
                var tickets = await _repository.TicketListByStatus(StatusID);

                // Lista para almacenar los tickets con la información complementada
                var ticketsWithUserInfo = new List<object>();

                // Crear el cliente HTTP a través de IHttpClientFactory
                var httpClient = _httpClientFactory.CreateClient();

                // Obtener el token JWT del contexto
                var token = HttpContext.Items["JWTToken"] as string;
                if (token == null)
                {
                    return Unauthorized("JWT Token is missing.");
                }

                // Incluir el token JWT en los encabezados de autorización
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Iterar sobre cada ticket para complementar la información
                foreach (TicketListModel ticket in tickets)
                {
                    // Consumir el endpoint de la API Users para obtener la información del cliente
                    var clientResponse = await httpClient.GetAsync($"http://localhost:5279/api/Users/InfoClient/{ticket.ClientID}");
                    UserModel? clientInfo = null;
                    if (clientResponse.IsSuccessStatusCode && clientResponse.Content.Headers.ContentLength > 0)
                    {
                        clientInfo = await clientResponse.Content.ReadFromJsonAsync<UserModel>();
                    }

                    // Consumir el endpoint de la API Users para obtener la información del representante (si existe)
                    UserModel? repInfo = null;
                    if (ticket.RepID != 0)
                    {
                        var repResponse = await httpClient.GetAsync($"http://localhost:5279/api/Users/InfoRep/{ticket.RepID}");
                        if (repResponse.IsSuccessStatusCode && repResponse.Content.Headers.ContentLength > 0)
                        {
                            repInfo = await repResponse.Content.ReadFromJsonAsync<UserModel>();
                        }
                    }

                    // Combinar la información del ticket con la del cliente y el representante
                    var ticketWithUserInfo = new
                    {
                        ticket.Title,
                        ticket.Description,
                        ticket.CreationDate,
                        ticket.UpdateDate,
                        ticket.StatusName,
                        ticket.PriorityName,
                        ticket.ChangeDescription,
                        Client = clientInfo, // Info del cliente
                        Representative = repInfo // Info del representante (puede ser null)
                    };

                    // Agregar el ticket con la información complementada
                    ticketsWithUserInfo.Add(ticketWithUserInfo);
                }

                return Ok(ticketsWithUserInfo); // Devolver la lista de tickets con la información complementada
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("TicketListByRepresentative/{RepID}")]
        public async Task<IActionResult> TicketListByRepresentative(int RepID)
        {
            try
            {
                // Obtener la lista de tickets desde el repositorio según el RepID
                var tickets = await _repository.TicketListByRepresentative(RepID);

                // Lista para almacenar los tickets con la información complementada
                var ticketsWithUserInfo = new List<object>();

                // Crear el cliente HTTP a través de IHttpClientFactory
                var httpClient = _httpClientFactory.CreateClient();

                // Obtener el token JWT del contexto
                var token = HttpContext.Items["JWTToken"] as string;
                if (token == null)
                {
                    return Unauthorized("JWT Token is missing.");
                }

                // Incluir el token JWT en los encabezados de autorización
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Iterar sobre cada ticket para complementar la información
                foreach (TicketListModel ticket in tickets)
                {
                    // Consumir el endpoint de la API Users para obtener la información del cliente
                    var clientResponse = await httpClient.GetAsync($"http://localhost:5279/api/Users/InfoClient/{ticket.ClientID}");
                    UserModel? clientInfo = null;
                    if (clientResponse.IsSuccessStatusCode && clientResponse.Content.Headers.ContentLength > 0)
                    {
                        clientInfo = await clientResponse.Content.ReadFromJsonAsync<UserModel>();
                    }

                    // Consumir el endpoint de la API Users para obtener la información del representante (si existe)
                    UserModel? repInfo = null;
                    if (ticket.RepID != 0)
                    {
                        var repResponse = await httpClient.GetAsync($"http://localhost:5279/api/Users/InfoRep/{ticket.RepID}");
                        if (repResponse.IsSuccessStatusCode && repResponse.Content.Headers.ContentLength > 0)
                        {
                            repInfo = await repResponse.Content.ReadFromJsonAsync<UserModel>();
                        }
                    }

                    // Combinar la información del ticket con la del cliente y el representante
                    var ticketWithUserInfo = new
                    {
                        ticket.Title,
                        ticket.Description,
                        ticket.CreationDate,
                        ticket.UpdateDate,
                        ticket.StatusName,
                        ticket.PriorityName,
                        ticket.ChangeDescription,
                        Client = clientInfo, // Info del cliente
                        Representative = repInfo // Info del representante (puede ser null)
                    };

                    // Agregar el ticket con la información complementada
                    ticketsWithUserInfo.Add(ticketWithUserInfo);
                }

                return Ok(ticketsWithUserInfo); // Devolver la lista de tickets con la información complementada
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("TicketListByClientID/{ClientID}")]
        public async Task<IActionResult> TicketListByClientID(int ClientID)
        {
            try
            {
                // Obtener la lista de tickets desde el repositorio según el RepID
                var tickets = await _repository.TicketListByClientID(ClientID);

                return Ok(tickets); // Devolver la lista de tickets con la información complementada
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("TicketHistoryList/{TicketID}")]
        public async Task<IActionResult> TicketHistoryList(int TicketID)
        {
            try
            {
                var list = await _repository.TicketHistoryList(TicketID);
                return Ok(list);

            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("TicketAssignmentList/{RepID}")]
        public async Task<IActionResult> TicketAssignmentList(int RepID)
        {
            try
            {
                // Obtener la lista de tickets desde el repositorio según el RepID
                var tickets = await _repository.TicketAssignmentList(RepID);

                // Lista para almacenar los tickets con la información complementada
                var ticketsWithUserInfo = new List<object>();

                // Crear el cliente HTTP a través de IHttpClientFactory
                var httpClient = _httpClientFactory.CreateClient();

                // Obtener el token JWT del contexto
                var token = HttpContext.Items["JWTToken"] as string;
                if (token == null)
                {
                    return Unauthorized("JWT Token is missing.");
                }

                // Incluir el token JWT en los encabezados de autorización
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Iterar sobre cada ticket para complementar la información
                foreach (TicketListModel ticket in tickets)
                {
                    // Consumir el endpoint de la API Users para obtener la información del cliente
                    var clientResponse = await httpClient.GetAsync($"http://localhost:5279/api/Users/InfoClient/{ticket.ClientID}");
                    UserModel? clientInfo = null;
                    if (clientResponse.IsSuccessStatusCode && clientResponse.Content.Headers.ContentLength > 0)
                    {
                        clientInfo = await clientResponse.Content.ReadFromJsonAsync<UserModel>();
                    }

                    // Consumir el endpoint de la API Users para obtener la información del representante (si existe)
                    UserModel? repInfo = null;
                    if (ticket.RepID != 0)
                    {
                        var repResponse = await httpClient.GetAsync($"http://localhost:5279/api/Users/InfoRep/{ticket.RepID}");
                        if (repResponse.IsSuccessStatusCode && repResponse.Content.Headers.ContentLength > 0)
                        {
                            repInfo = await repResponse.Content.ReadFromJsonAsync<UserModel>();
                        }
                    }

                    // Combinar la información del ticket con la del cliente y el representante
                    var ticketWithUserInfo = new
                    {
                        ticket.Title,
                        ticket.Description,
                        ticket.CreationDate,
                        ticket.UpdateDate,
                        ticket.StatusName,
                        ticket.PriorityName,
                        ticket.ChangeDescription,
                        Client = clientInfo, // Info del cliente
                        Representative = repInfo // Info del representante (puede ser null)
                    };

                    // Agregar el ticket con la información complementada
                    ticketsWithUserInfo.Add(ticketWithUserInfo);
                }

                return Ok(ticketsWithUserInfo); // Devolver la lista de tickets con la información complementada
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("TicketsTotal/{RepID}")]
        public async Task<IActionResult> TicketsTotal(int RepID)
        {
            try
            {
                var total = await _repository.TicketsTotal(RepID);
                return Ok(total);
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("OpenTicketsTotal/{RepID}")]
        public async Task<IActionResult> OpenTicketsTotal(int RepID)
        {
            try
            {
                var openTotal = await _repository.OpenTicketsTotal(RepID);
                return Ok(openTotal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("TicketsInProcessTotal/{RepID}")]
        public async Task<IActionResult> TicketsIProcessTotal(int RepID)
        {
            try
            {
                var inProcessTotal = await _repository.TicketsIProcessTotal(RepID);
                return Ok(inProcessTotal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("ClosedTicketsTotal/{RepID}")]
        public async Task<IActionResult> ClosedTicketsTotal(int RepID)
        {
            try
            {
                var closedTotal = await _repository.ClosedTicketsTotal(RepID);
                return Ok(closedTotal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("PreliminaryTicketsList")]
        public async Task<IActionResult> PreliminaryTicketsList()
        {
            try
            {
                var list = await _repository.PreliminaryTicketsList();
                return Ok(list);

            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("PreliminaryTicketById/{PreliminaryID}")]
        public async Task<IActionResult> PreliminaryTicketById(int PreliminaryID)
        {
            try
            {
                var list = await _repository.PreliminaryTicketById(PreliminaryID);
                return Ok(list);

            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //poooooooooooost

        [HttpPost("CreatePreliminaryTicket")]
        public async Task<IActionResult> CreatePreliminaryTicket([FromBody]CreatePreliminaryTicket model)
        {
            try
            {
                await _repository.CreatePreliminaryTicket(model);
                return Ok(new { Message = "El ticket se creo con éxito" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo crear {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("CompleteTicket")]
        public async Task<IActionResult> CompleteTicket(CompleteTicketModel model)
        {
            try
            {
                await _repository.CompleteTicket(model);
                return Ok(new {Message = "El ticket se creo con éxito" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo crear {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateTicket")]
        public async Task<IActionResult> UpdateTicket([FromBody]UpdateTicketModel model)
        {
            try
            {
                await _repository.UpdateTicket(model);
                return Ok(new { Message = "\"El ticket se actualizó con éxito" });

            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo actualizar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateRepAssign")]
        public async Task<IActionResult> UpdateRepAssign([FromBody]UpdateRepAssignModel model)
        {
            try
            {
                await _repository.UpdateRepAssign(model);
                return Ok("El representante se actualizó con éxito");
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo actualizar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("ReopenTicket")]
        public async Task<IActionResult> ReopenTicket([FromBody] ReopenTicketModel model)
        {
            try
            {
                await _repository.ReopenTicket(model);
                return Ok("El ticket se reabrió con éxito");
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo reabrir {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
