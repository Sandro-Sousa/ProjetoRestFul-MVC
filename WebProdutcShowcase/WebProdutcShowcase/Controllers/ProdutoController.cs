using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebProdutcShowcase.Models;


namespace WebProdutcShowcase.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Produto> produtos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44314/api/");
                var responseTask = client.GetAsync("produto");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var res = result.Content.ReadAsAsync<IList<Produto>>();
                    res.Wait();
                    produtos = res.Result;
                }
                else
                {
                    produtos = Enumerable.Empty<Produto>();
                    ModelState.AddModelError(string.Empty, "Ocorreu um Erro no Servidor, Favor entre em contato com o administrador!");
                }
            }
            return View(produtos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44314/api/produto");
                var responseTask = client.PostAsJsonAsync<Produto>("produto", produto);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "erro no servidor. por favor, verifique com o administrador!");

            return View(produto);
        }
        public async Task<ActionResult> Edit(int id)
        {
            Produto produto = new Produto();

            var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:44314/api/produto/{id}");

            var response = await httpClient.SendAsync(request);
            string apiResponse = await response.Content.ReadAsStringAsync();
            
            produto = JsonConvert.DeserializeObject<Produto>(apiResponse);

            return View(produto);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Produto produto)
        {
            Produto resultProduto = new Produto();

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:44314/api/produto/{produto.CodigoId}")
            {
                Content = new StringContent(new JavaScriptSerializer().Serialize(produto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);

            string apiResponse = await response.Content.ReadAsStringAsync();
            ViewBag.Result = "Success";
            resultProduto = JsonConvert.DeserializeObject<Produto>(apiResponse);

            return View(resultProduto);
        }
    }
}
