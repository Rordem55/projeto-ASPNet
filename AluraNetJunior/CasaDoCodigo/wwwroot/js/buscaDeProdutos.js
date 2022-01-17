class BuscaDeProdutos {
    updatePesquisa(btn) {
        let data = this.getData(btn);
        this.postPesquisa(data.Termo);
    }

    getData(elemento) {
        var linhaDoItem = $(elemento).parents('[id]');
        var novoTermo = $(linhaDoItem).find('input').val();
        return {
            Termo: novoTermo
        };
    }

    postPesquisa(data) {

        $.ajax({
            url: '/pedido/BuscaDeProdutosPost',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
        }).done(function (response) {

            let produtos = response.produtos
            let categorias = response.categorias
            let termo = response.termo
            let linhaCarroussels = document.querySelector("#carroussels");

            let indiceCarroussel = 1;

            if (produtos.length == 0) {
                linhaCarroussels.innerHTML = "<div><h1>Nenhum produto encontrado<h1></div>";
            }

            else {
                linhaCarroussels.innerHTML = "";
                categorias.forEach(categoria => {
                    let produtos_categoria = [];
                    linhaCarroussels.insertAdjacentHTML("beforeend",
                        "<div id=CatId-" + categoria.id + " >" +
                        "<div>" + 
                        "<h3>" + categoria.Nome + "</h3>" +
                        "</div>" + 
                        "</div>"
                    );
                    produtos.forEach(produto => {
                        if (produto.categoriaId == categoria.id) {
                            produtos_categoria.push(produto);
                        }
                    });
                    let linhaCategoria = linhaCarroussels.querySelector("#CatId-" + categoria.id);
                    linhaCategoria.insertAdjacentHTML("beforeend",
                        "<div id='my-carousel-" + indiceCarroussel + "' class='carousel slide' data-ride='carousel'>" +
                        "<div class='carousel-inner' role='listbox'>" + 
                        "</div>" +
                        "</div>"
                    );
                    
                    let linhaCategoriaCarroussel = linhaCategoria.querySelector(".carousel-inner");
                    console.log(linhaCategoriaCarroussel);

                    let itemAtual = 1;
                    let active = " active";
                    produtos_categoria.forEach((produto, i) => {
                        console.log(i);
                        if (i == 4 * itemAtual - 4) {                            
                            linhaCategoriaCarroussel.insertAdjacentHTML("beforeend",
                                "<div class='item" + active + "'>" +
                                "<div class='row'>");
                            active = "";
                            itemAtual++;
                        }
                       
                        let linhaAtual = linhaCategoriaCarroussel.querySelectorAll(".row");
                        linhaAtual[linhaAtual.length - 1].insertAdjacentHTML("beforeend",
                            "<div class='col-md-3 col-sm-3 col-lg-3' id='ProdId-" + produto.id + "'>" +
                            "<div class='panel panel-default'>" +
                            "<div class='panel-body' id='imagem'>" +
                            "<img class='img-produto-carrossel' src='/images/produtos/large_" + produto.codigo + ".jpg' />" +
                            "</div>" +
                            "<div class='panel-footer produto-footer'>" +
                            "<div class='produto-nome'>" + produto.nome + "</div>" +
                            "<div><h4><strong>R$ " + produto.preco + "</strong></h4></div>" +
                            "<div class='text-center'>" +
                            "<a class='btn btn-success'>Adicionar</a>" +
                            "</div>" +
                            "</div>" +
                            "</div>" +
                            "</div>");                  
                    });                        
                    
                    linhaCategoriaCarroussel.insertAdjacentHTML("beforeend",
                        "<a class='left carousel-control' href='#my-carousel-" + indiceCarroussel + "' role='button' data-slide='prev'>" +
                        "<span class='glyphicon glyphicon-chevron-left' aria-hidden='true'></span>" +
                        "<span class='sr-only'>Previous</span>" +
                        "</a>" +
                        "<a class='right carousel-control' href='#my-carousel-" + indiceCarroussel + "' role='button' data-slide='next'>" +
                        "<span class='glyphicon glyphicon-chevron-right' aria-hidden='true'></span>" +
                        "<span class='sr-only'>Next</span>" +
                        "</a>");
                    indiceCarroussel++;
                });

            }
        });
    }
}

        var _buscaDeProdutos = new BuscaDeProdutos();