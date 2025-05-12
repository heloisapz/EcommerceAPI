using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces

// interface: define o contrato que a classe deve seguir, sem implementar a lógica
// service: implementa a lógica de negócio definida na interface
{
    public interface InterfaceGenericaApp<T> where T : class
    {
        Task Add(T Objeto);
        Task Update(T Objeto);
        Task Delete(T Objeto);
        Task<T> GetEntityById(int Id);
        Task<List<T>> List();
    }
}
