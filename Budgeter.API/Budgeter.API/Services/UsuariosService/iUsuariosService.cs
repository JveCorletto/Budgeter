using Budgeter.API.Models.ADM;

namespace Budgeter.API.Services.UsuariosService
{
    public interface iUsuariosService : CRUD<Usuarios>
    {
        Usuarios login(LoginRequest loginRequest);
    }
}