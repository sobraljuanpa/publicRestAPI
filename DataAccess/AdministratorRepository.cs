using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

using Domain;
using IDataAccess;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess
{
    public class AdministratorRepository: IAdministratorRepository<Administrator>
    {

        readonly Context _context;

        public AdministratorRepository(Context context)
        {
            _context = context;
        }

        public Administrator Authenticate(string email, string password)
        {
            Administrator aux = _context.Administrators.FirstOrDefault(
                a => a.Email == email && a.Password == password);

            if (aux == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Ahora este es nuestro secreto.");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, aux.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                                                             SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            aux.Token = tokenHandler.WriteToken(token);

            aux.Password = null;

            return aux;
        }

        public IQueryable<Administrator> GetAll()
        {
            return _context.Administrators;
        }

        public Administrator Get(int id)
        {
            return _context.Administrators.Find(id);
        }

        public void Add(Administrator administrator)
        {
            _context.Administrators.Add(administrator);
            _context.SaveChanges();
        }

        public void Update(int id, Administrator administrator)
        {
            Get(id).Email = administrator.Email;
            Get(id).Name = administrator.Name;
            Get(id).Password = administrator.Password;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Administrators.Remove(Get(id));
            _context.SaveChanges();
        }
    }
}
