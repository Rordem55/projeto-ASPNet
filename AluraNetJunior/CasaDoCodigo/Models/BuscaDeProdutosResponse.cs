using CasaDoCodigo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    public class BuscaDeProdutosResponse
    {
        public BuscaDeProdutosResponse(string termo, BuscaDeProdutosViewModel buscaDeProdutosViewModel)
        {
            this.termo = termo;
            this.buscaDeProdutosViewModel = buscaDeProdutosViewModel;
        }

        public string termo { get; }
        public BuscaDeProdutosViewModel buscaDeProdutosViewModel { get; }
    }
}
