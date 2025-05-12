using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces.InterfaceProduct;
using Infrastructure.Repository.Repositories;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;

namespace UnitTestEcommerceDDD
{
    [TestClass]
    public class UnitTestEcommerce
    {
        // testa se um produto válido é adicionado com sucesso
        [TestMethod]
        public async Task AddProductComSucesso()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                IServiceProduct _IServiceProduct = new ServiceProduct(_IProduct);

                var produto = new Produto
                {
                    Descricao = string.Concat("Descrição Test TDD", DateTime.Now.ToString()),
                    QtdEstoque = 10,
                    Nome = string.Concat("Nome Test TDD", DateTime.Now.ToString()),
                    Valor = 20,
                    UserId = "7e771b40-562b-4a7d-a1cc-42bdc678cdce"
                };

                await _IServiceProduct.AddProduct(produto);

                // se não houver notificações de erro, o produto foi validado corretamente
                Assert.IsFalse(produto.Notitycoes.Any());
            }
            catch (Exception)
            {
                Assert.Fail(); // falha caso ocorra qualquer exceção
            }
        }

        // testa se a validação de campos obrigatórios está funcionando
        [TestMethod]
        public async Task AddProductComValidacaoCampoObrigatorio()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                IServiceProduct _IServiceProduct = new ServiceProduct(_IProduct);

                var produto = new Produto(); // produto sem propriedades preenchidas
                await _IServiceProduct.AddProduct(produto);

                // deve retornar notificações porque campos obrigatórios estão faltando
                Assert.IsTrue(produto.Notitycoes.Any());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        // testa se a listagem de produtos de um usuário retorna resultados
        [TestMethod]
        public async Task ListarProdutosUsuario()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();

                var listaProdutos = await _IProduct.ListarProdutosUsuario("7e771b40-562b-4a7d-a1cc-42bdc678cdce");

                Assert.IsTrue(listaProdutos.Any()); // deve haver pelo menos um produto
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        // testa se um produto pode ser recuperado por seu ID
        [TestMethod]
        public async Task GetEntityById()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();

                var listaProdutos = await _IProduct.ListarProdutosUsuario("7e771b40-562b-4a7d-a1cc-42bdc678cdce");

                var produto = await _IProduct.GetEntityById(listaProdutos.LastOrDefault()?.Id ?? 0);

                Assert.IsNotNull(produto); // produto deve existir
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        // testa se um produto pode ser deletado
        [TestMethod]
        public async Task Delete()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();

                var listaProdutos = await _IProduct.ListarProdutosUsuario("7e771b40-562b-4a7d-a1cc-42bdc678cdce");

                var ultimoProduto = listaProdutos.LastOrDefault();

                if (ultimoProduto != null)
                {
                    await _IProduct.Delete(ultimoProduto);
                }

                Assert.IsTrue(true); // sucesso se chegou até aqui
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
