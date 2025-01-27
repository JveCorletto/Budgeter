using Budgeter.API.Models.CTL;
using Budgeter.API.Management;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Budgeter.API.Services.MetodosPagoService;

namespace Budgeter.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("API/[controller]")]
    public class MetodosPagosController : ControllerBase
    {
        private readonly iMetodosPagosService iMetodosPagosService;
        public MetodosPagosController( iMetodosPagosService metodosPagosService)
        {
            iMetodosPagosService = metodosPagosService;
        }

        [HttpPost]
        [Route("CreateMethod")]
        public async Task<IActionResult> CreateMethod([FromBody] MetodosPagos request)
        {
            APIResponses response = new APIResponses();

            try
            {
                if (string.IsNullOrEmpty(request.MetodoPago))
                {
                    response.Message = "El nombre del método de pago es obligatorio.";
                    return BadRequest(response);
                }

                if (iMetodosPagosService.validateName(request.MetodoPago))
                {
                    response.Message = "El método de pago ya existe.";
                    return Conflict(response);
                }

                var metodoPago = new MetodosPagos
                {
                    MetodoPago = request.MetodoPago,
                    Descripcion = request.Descripcion
                };

                iMetodosPagosService.Create(metodoPago);

                if (metodoPago.IdMetodoPago > 0)
                {
                    response.Message = "Método Creado Exitosamente";
                    return Ok(response);
                }
                else
                {
                    response.Message = "No se ha podido crear el método de pago, intente nuevamente";
                    return BadRequest(response);
                }
            }
            catch (Exception)
            {
                response.Message = "No se ha podido crear el método de pago, intente nuevamente";
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("GetMethods")]
        public async Task<IActionResult> GetMethods()
        {
            APIResponses response = new APIResponses();

            try
            {
                List<MetodosPagos> metodosPagos = iMetodosPagosService.Read();
                response.Data = metodosPagos;
                return Ok(response);
            }
            catch (Exception)
            {
                response.Message = "No se pudo obtener la lista de métodos de pago, intente nuevamente.";
                return BadRequest(response);
            }
        }
    }
}