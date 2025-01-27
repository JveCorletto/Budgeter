using Budgeter.API.Management;
using Budgeter.API.Models.ADM;
using Microsoft.AspNetCore.Mvc;
using Budgeter.API.Services.UsuariosService;

namespace Budgeter.API.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly iUsuariosService iUsuarios;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, iUsuariosService usuariosService)
        {
            iUsuarios = usuariosService;
            _configuration = configuration;
        }


        [HttpPost("login")]
        public IActionResult login([FromBody] LoginRequest request)
        {
            APIResponses response = new APIResponses();

            try
            {
                Usuarios user = iUsuarios.login(request);
                if (user == null) {
                    response.Message = "Credenciales inválidas";
                    return Unauthorized(response);
                }

                JWTHandler _jwtHandler = new JWTHandler(_configuration);
                String newToken = _jwtHandler.GenerateToken(user);
                response.Message = $"Bienvenid@ {user.Usuario}";
                response.Data = newToken;

                return Ok(response);
            }
            catch (Exception)
            {
                response.Message = "No se pudo procesar la solicitud.";
                return BadRequest(response);
            }
        }
    }
}

public class LoginRequest
{
    public String? User { get; set; }
    public String? Email { get; set; }

    required
    public String Password { get; set; }
}