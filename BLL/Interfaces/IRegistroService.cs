using System;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IRegistroService
    {
        void CreateRegistro(Registro registro);
    }
}
