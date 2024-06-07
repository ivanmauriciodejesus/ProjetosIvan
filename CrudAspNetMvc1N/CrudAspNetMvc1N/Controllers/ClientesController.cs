using CrudAspNetMvc1N.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CrudAspNetMvc1N.Controllers
{
    public class ClientesController : Controller
    {
        HttpClient client = new HttpClient();

        public ClientesController()
        {
            client.BaseAddress = new Uri("https://localhost:7187");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Clientes
        public async Task<ActionResult> Index()
        {
            List<Cliente> clientes = new List<Cliente>();

            HttpResponseMessage response = client.GetAsync("/api/Clientes").Result;
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                clientes = System.Text.Json.JsonSerializer.Deserialize<List<Cliente>>
                    (stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
            }

            return View(clientes);
        }

        // GET: Clientes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            HttpResponseMessage response = client.GetAsync($"/api/Clientes/{id}").Result;

            var stringResponse = await response.Content.ReadAsStringAsync();

            var cliente = System.Text.Json.JsonSerializer.Deserialize<Cliente>
                    (stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

            if (cliente != null)
                return View(cliente);
            else
                return HttpNotFound();
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Email,Logotipo,Logradouros")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(cliente);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync("/api/Clientes", byteContent);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Erro ao cadastrar cliente.";
                    return View();
                }
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage response = client.GetAsync($"/api/Clientes/{id}").Result;

            var stringResponse = await response.Content.ReadAsStringAsync();

            var cliente = System.Text.Json.JsonSerializer.Deserialize<Cliente>
                    (stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Email,Logotipo,Logradouros")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(cliente);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = client.PutAsync($"/api/Clientes/{cliente.Id}", byteContent).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Erro ao editar o cliente.";
                    return View();
                }
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage response = client.GetAsync($"/api/Clientes/{id}").Result;

            var stringResponse = await response.Content.ReadAsStringAsync();

            var cliente = System.Text.Json.JsonSerializer.Deserialize<Cliente>
                    (stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/Clientes/{id}");

            if (!response.IsSuccessStatusCode)
                return HttpNotFound();

            return RedirectToAction("Index");
        }
    }
}
