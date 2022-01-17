using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CasaDoCodigo.Models.ViewModels;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        ICategoriaRepository _categoriaRepository;
        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) : base(contexto)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<BuscaDeProdutosViewModel> BuscaDeProdutoViewModel(string termo)
        {
            var produtos = await this.GetProdutos(termo);
            var categoriasDeProdutosIds = this.GetCategoriasDeProdutosIds(produtos);
            var categorias = _categoriaRepository.GetCategoriaPorListaId(categoriasDeProdutosIds);

            return new BuscaDeProdutosViewModel(produtos, categorias, termo);
        }

        public  IList<int> GetCategoriasDeProdutosIds(IList<Produto> produtos)
        {
            var listaIds =  from produto in produtos select produto.CategoriaId;
            return listaIds.Distinct().ToList();
        }

        public IList<Produto> GetProdutos()
        {
            return dbSet.ToList();
        }

        public async Task<IList<Produto>> GetProdutos(string termo)
        {
            IEnumerable<Produto> produtos = new List<Produto>();
            produtos = await this.GetProdutosComCategoriasAsync();

            if (termo != null && termo != "")
            {
                produtos = from produto in produtos
                           where produto.Nome.ToUpper().Contains(termo.ToUpper()) ||
                           produto.Categoria.Nome.ToUpper().Contains(termo.ToUpper())
                           select produto;            
            }
            
            if (produtos == null)
            {
                //throw new ArgumentException("Nenhum produto encontrado");
                return null;
            }
            return produtos.ToList();


        }

        public async Task<IList<Produto>> GetProdutosComCategoriasAsync()
        {
            return await dbSet.Include(p => p.Categoria).ToListAsync();
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
