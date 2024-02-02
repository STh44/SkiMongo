using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ski_ServiceNoSQL.Models;
using Ski_ServiceNoSQL.Services;


namespace Ski_ServiceNoSQL.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService, ILogger<OrdersController> logger)
        {
            _ordersService = ordersService;
            _logger = logger;
        }

        /// <summary>
        /// Alle Registrationen
        /// </summary>
        /// <returns>Liste aller Registrationen</returns>
        [AllowAnonymous]
        [HttpGet ("All")]
        public ActionResult<List<Orders>> Get()
        {
            try
            {
                return _ordersService.Get();
            }catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }
        }

        public List<Orders> list;
        /// <summary>
        /// Alle Registrationen geordnet nach ihrer Priorität
        /// </summary>
        /// <returns>Liste aller Registrationen geordnet nach ihrer Priorität</returns>
        [AllowAnonymous]
        [HttpGet("byPriority")]
        public ActionResult<List<Orders>> GetPriority()
        {
            try
            {
                var orders = _ordersService.GetPriority();
                list = orders.Result.ToList();
                return list;
            }catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }
        }

        /// <summary>
        /// Registration nach Id
        /// </summary>
        /// <param name="id">Id von der Registration</param>
        /// <returns>Die Registration mit der gesuchten Id</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<Orders> Get(string id)
        {
            try
            {
                return _ordersService.Get(id);
            }catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }
        }

        /// <summary>
        /// Neuen Einträge für Registrationen, ohnen Autorisierung möglich
        /// </summary>
        /// <param name="value">Der Datensatz einer Bestellung</param>
        /// <returns>Die Registration die gemacht wurde</returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post([FromBody] Orders value)
        {
            try { 
                 _ordersService.Create(value);
                return CreatedAtAction(nameof(Get), new { id = value.Id }, value);

            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }
        }

        /// <summary>
        /// Registration updaten
        /// </summary>
        /// <param name="id">Id von der Registration</param>
        /// <param name="value">Änderungen für diesen Eintrag</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Orders value)
        {
            try { 
            _ordersService.Update(id, value);
            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }
        }

        /// <summary>
        /// Löscht einen Eintrag
        /// </summary>
        /// <param name="id">Id des zu löschenden Eintrag</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try 
            {
                var test = _ordersService.Get(id);
                if(test == null)
                {
                    return NotFound();
                }
                _ordersService.Remove(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }
        }
    }
}
