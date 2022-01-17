using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

        [DataMember]
        //[JsonProperty("Produto")]
        public  IList<Produto> Produtos { get; }

        [DataMember]
        //[JsonProperty("Categoria")]
        public IList<Categoria> Categorias { get; }
        
        [DataMember]
        //[JsonProperty("Termo")]
        public string Termo { get; }

    }
}
