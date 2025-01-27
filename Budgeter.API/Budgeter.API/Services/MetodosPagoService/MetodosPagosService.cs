using Budgeter.API.dbContext;
using Budgeter.API.Models.CTL;

namespace Budgeter.API.Services.MetodosPagoService
{
    public class MetodosPagosService : iMetodosPagosService
    {
        private readonly DataContext context;

        public MetodosPagosService(DataContext context)
        {
            this.context = context;
        }

        #region CRUD

        public void Create(MetodosPagos entity)
        {
            entity.FechaCreacion = DateTime.Now;
            context.MetodosPagos.Add(entity);
            context.SaveChanges();
        }

        public void Delete(MetodosPagos entity)
        {
            throw new NotImplementedException();
        }

        public MetodosPagos getById(long? id)
        {
            return context.MetodosPagos.FirstOrDefault(m => m.IdMetodoPago == id);
        }

        public List<MetodosPagos> Read()
        {
            return context.MetodosPagos.ToList();
        }

        public void Update(MetodosPagos entity)
        {
            entity.FechaModificacion = DateTime.Now;
            context.SaveChanges();
        }

        #endregion

        public bool validateName(string newMethod)
        {
            return context.MetodosPagos.Any(mp => mp.MetodoPago.ToLower() == newMethod.ToLower());
        }
    }
}