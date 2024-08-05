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

            // Only assign a new ID if none is provided
            if (string.IsNullOrEmpty(registro.ID))
            {
                registro.ID = Guid.NewGuid().ToString();
            }

            _context.Registros.Add(registro);
            _context.SaveChanges();
        }
    }
}
