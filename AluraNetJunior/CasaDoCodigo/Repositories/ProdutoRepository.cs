using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        ICategoriaRepository _categoriaRepository;
        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) : base(contexto)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IList<Produto> GetProdutos()
        {
            return dbSet.ToList();
        }

        public IList<Produto> GetProdutosComCategorias()
        {
            return dbSet.Include(p => p.Categoria).ToList();
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!dbSet.Include(p => p.Categoria).Where(p => p.Categoria.Nome == livro.Categoria).Any())
                {
                    await _categoriaRepository.SaveCategoria(livro.Categoria);                   
                }
                Categoria categoriaLivro = contexto.Set<Categoria>().Where(p => p.Nome == livro.Categoria).SingleOrDefault();
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoriaLivro));
                }
            }
            await contexto.SaveChangesAsync();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
