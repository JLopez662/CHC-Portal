using BLL.Interfaces;
using DAL.Models;
using DAL;
using System;

namespace BLL.Services
{
    public class RegistroService : IRegistroService
    {
        private readonly ApplicationDbContext _context;

        public RegistroService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateRegistro(Registro registro)
        {
            if (registro == null)
            {
                throw new ArgumentNullException(nameof(registro));
            }

            // Generate a new ID for the registro if necessary
            registro.ID = Guid.NewGuid().ToString();

            _context.Registros.Add(registro);
            _context.SaveChanges();
        }
    }
}
