class BuscaDeProdutos {
    updatePesquisa(btn) {  
        let data = this.getData(btn);
        this.postPesquisa(data.Termo);
    }

    getData(elemento) {
        var linhaDoItem = $(elemento).parents('[id]');
        //var itemId = $(linhaDoItem).attr('item-id');
        var novoTermo = $(linhaDoItem).find('input').val();
        return {
            Termo: novoTermo
        };
    }

    incluirProduto(produto, categoria) {

    }

    postPesquisa(data) {

        $.ajax({
            url: '/pedido/BuscaDeProdutosPost',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
        }).done(function (response) {
            let produtos = response.produtos
            let categoria = response.categoria
            let termo = response.termo
            let linhaDoItem = $('[id]').find('input').val(termo)

            //location.reload();

        });
    }
}

var _buscaDeProdutos = new BuscaDeProdutos()