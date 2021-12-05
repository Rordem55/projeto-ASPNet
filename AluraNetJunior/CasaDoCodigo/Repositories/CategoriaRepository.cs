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
        IList<Categoria> GetCategorias();
        Categoria GetCategoriaPorNome(string categoriaNome);
        Categoria GetCategoriaPorId(int categoriaId);
        IList<Categoria> GetCategoriaPorListaId(IList<int> categoriaIds);
        Task SaveCategoria(string categoriaNome);


    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {


        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public Categoria GetCategoriaPorNome(string categoriaNome)
        {
            var categoria = dbSet.Where(c => c.Nome == categoriaNome).SingleOrDefault();
            return categoria;
        }

        public Categoria GetCategoriaPorId(int categoriaId)
        {
            var categoria = dbSet.Where(c => c.Id == categoriaId).SingleOrDefault();
            return categoria;
        }

        public IList<Categoria> GetCategoriaPorListaId(IList<int> categoriaIds)
        {
            var listaCategorias = new List<Categoria>();
            foreach (int id in categoriaIds)
            {
                listaCategorias.Add(this.GetCategoriaPorId(id));
            }
            return listaCategorias;
        }

        public async Task SaveCategoria(string categoriaNome)
        {
            var  categoriadB = GetCategoriaPorNome(categoriaNome);
            if (categoriadB == null)
            {
                dbSet.Add(new Categoria(categoriaNome));
                await contexto.SaveChangesAsync();
            }
            
        }

        public IList<Categoria> GetCategorias()
        {
            return dbSet.ToList();
        }
    }
}