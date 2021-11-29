using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Categoria GetCategoria(string categoriaNome);
        Task SaveCategoria(string categoriaNome);
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {


        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public Categoria GetCategoria(string categoriaNome)
        {
            var categoria = dbSet.Where(c => c.Nome == categoriaNome).SingleOrDefault();
            return categoria;
        }

        public async Task SaveCategoria(string categoriaNome)
        {
            var  categoriadB = GetCategoria(categoriaNome);
            if (categoriadB == null)
            {
                dbSet.Add(new Categoria(categoriaNome));
                await contexto.SaveChangesAsync();
            }
            
        }
    }
}