using Entity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductShowcase.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly ProdutoService produtoService;

        public ProdutoController(ProdutoService _produtoService)
        {
            produtoService = _produtoService;
        }

        [HttpGet]
        public async Task<IEnumerable<Produto>> GetProduto()
        {
            return await produtoService.BuscarProdutos();
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Produto>> GetId(int? id)
        {
            if (id == null)
                return BadRequest("Por favor, Forneça o Id Corretamente");
            else
            {
                return await produtoService.BuscarId(id);
            }
        }

       [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto([FromBody] Produto data)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Verifique os Dados Enviados e Tente Novamente!");
            }
            else
            {
                try
                {
                    var datas = await produtoService.CriarProduto(data);
                    return Ok(datas);
                }
                catch (Exception e)
                {

                    return BadRequest(e);
                }
            }
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult<Produto>> PutProduto(int id, [FromBody] Produto produto)
        {
            if (id != produto.CodigoId)
            {
                return BadRequest("Produto Não Encontrado");
            }
            else
            {
                try
                {
                    var datas = await produtoService.AlterarProduto(produto);
                    return Ok(datas);
                }
                catch (Exception e)
                {

                    return BadRequest(e);
                }
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<Produto>> Delete(int? id)
        {
            if (id == null)
                return BadRequest("Por favor Forneça o Id");
            else
            {
                try
                {
                    var data = await produtoService.DeletarProduto(id);
                    return Ok(data);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }
    }
}
