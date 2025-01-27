using Budgeter.API.Models.CTL;

namespace Budgeter.API.Services.MetodosPagoService
{
    public interface iMetodosPagosService : CRUD<MetodosPagos>
    {
        Boolean validateName(String newMethod);
    }
}