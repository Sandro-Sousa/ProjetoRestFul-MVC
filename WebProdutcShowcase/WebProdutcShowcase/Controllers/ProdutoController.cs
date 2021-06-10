using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebProdutcShowcase.Models;


namespace WebProdutcShowcase.Controllers
{
    public class ProdutoController : Controller
    {
        private string baseUrl = "https://localhost:44314/api/";
        public async Task<ActionResult> Index()
        {
            IEnumerable<Produto> produtos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("produto");
                if (res.IsSuccessStatusCode)
                {
                    var produtoLista = res.Content.ReadAsStringAsync().Result;
                    produtos = JsonConvert.DeserializeObject<List<Produto>>(produtoLista);
                }
            }
            return View(produtos);
        }

        public ActionResult CriarProduto()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CriarProduto(Produto produto)
        {
            string message = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.PostAsJsonAsync<Produto>("produto", produto);
                if (res.IsSuccessStatusCode)
                {
                    message = "Dados Criados com Sucesso";
                }
                else
                {
                    message = "Falha ao Criar Dados";
                }
                ViewBag.message = message;
            }
            return View();
        }


        public async Task<ActionResult> Edit(int id)
        {
            Produto produtos = new Produto();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("produto/" + id, HttpCompletionOption.ResponseContentRead);
                if (res.IsSuccessStatusCode)
                {
                    var dados = res.Content.ReadAsStringAsync().Result;
                    produtos = JsonConvert.DeserializeObject<Produto>(dados);
                }
            }
            return View(produtos);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Produto produto)
        {
            string message = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.PutAsJsonAsync<Produto>($"produto/{produto.CodigoId}", produto);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    message = "Falha ao Atualizar os Dados";
                }
                ViewBag.message = message;
            }
            return View(produto);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int produtoId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.DeleteAsync("produto/" + produtoId
                    );
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
