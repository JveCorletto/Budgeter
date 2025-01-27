using Budgeter.API.dbContext;
using Budgeter.API.Management;
using Budgeter.API.Models.ADM;

namespace Budgeter.API.Services.UsuariosService
{
    public class UsuariosService : iUsuariosService
    {
        private readonly DataContext context;
        public UsuariosService(DataContext context)
        {
            this.context = context;
        }

        #region CRUD

        public void Create(Usuarios entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Usuarios entity)
        {
            throw new NotImplementedException();
        }

        public Usuarios getById(long? id)
        {
            throw new NotImplementedException();
        }

        public List<Usuarios> Read()
        {
            throw new NotImplementedException();
        }

        public void Update(Usuarios entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        public Usuarios login(LoginRequest loginRequest)
        {
            Usuarios user;

            if (loginRequest.Email != null) {
                user = context.Usuarios.FirstOrDefault(u => u.Correo == loginRequest.Email && u.EstadoUsuario);
            }
            else {
                user = context.Usuarios.FirstOrDefault(u => u.Usuario == loginRequest.User && u.EstadoUsuario);
            }

            if (user == null || !crypto.VerifyPasswords(loginRequest.Password, user.Contrasenia))
                return null;

            user.UltimoAcceso = DateTime.Now;
            context.SaveChanges();

            return user;
        }
    }
}
