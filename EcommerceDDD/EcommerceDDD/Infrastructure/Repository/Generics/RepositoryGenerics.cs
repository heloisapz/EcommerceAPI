using Domain.Interfaces.Generics;
//importa a interface genérica que define os métodos Add, Delete, etc.
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
//importa a classe base para segurança de handle de sistema (para Dispose seguro)
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//necessário para interoperabilidade (neste caso usado com SafeHandle)
using System.Threading.Tasks;

namespace Infrastructure.Repository.Generics
{
    public class RepositoryGenerics<T> : IGeneric<T>, IDisposable where T : class
    //classe repositório genérico que implementa interface genérica IGeneric<T> e IDisposable

    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;
        //armazena as configurações do DbContext (como a connection string)
        public RepositoryGenerics()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        //método assíncrono para adicionar um objeto ao banco
        public async Task Add(T Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            //cria uma instância do contexto e adiciona o objeto
            {
                await data.Set<T>().AddAsync(Objeto); 
                //adiciona o objeto ao DbSet correspondente
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                data.Set<T>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<T> GetEntityById(int Id)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                return await data.Set<T>().FindAsync(Id);
            }
        }
        public async Task<List<T>> List()
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();
                //AsNoTracking evita que o EF Core acompanhe as alterações (melhora performance em leitura)

            }
        }

        public async Task Update(T Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                data.Set<T>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        bool disposed = false;
        //flag para evitar chamada dupla do método Dispose

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        //cria um SafeHandle (protege recursos não gerenciados)

        public void Dispose()
        //método público Dispose chamado externamente para liberar recursos
        {
            Dispose(true); 
            //libera recursos gerenciados e não gerenciados
            GC.SuppressFinalize(this); //evita que o coletor de lixo chame o finalizador
        }
        protected virtual void Dispose(bool disposing)
        //método protegido que executa a lógica real de liberação
        {
            if (disposed) return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }
        #endregion
    }
}
