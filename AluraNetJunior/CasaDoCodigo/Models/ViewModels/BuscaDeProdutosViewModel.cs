using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscaDeProdutosViewModel
    {
        public BuscaDeProdutosViewModel(IList<Produto> produtos, IList<Categoria> categorias)
        {
            Produtos = produtos;
            Categorias = categorias;
            Termo = "";
            
        }
        public BuscaDeProdutosViewModel(IList<Produto> produtos, IList<Categoria> categorias, string termo)
        {
            Produtos = produtos;
            Categorias = categorias;
            Termo = termo;
        }

       public  IList<Produto> Produtos { get; }
       public IList<Categoria> Categorias { get; }
       public string Termo { get; }

    }
}
