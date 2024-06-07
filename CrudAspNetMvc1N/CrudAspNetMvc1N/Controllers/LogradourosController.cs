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
    public class LogradourosController : Controller
    {        
        HttpClient client = new HttpClient();
        public LogradourosController()
        {
            client.BaseAddress = new Uri("https://localhost:7187");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Logradouros
        public async Task<ActionResult> Index()
        {
            List<Logradouro> logradouros = new List<Logradouro>();

            HttpResponseMessage response = client.GetAsync("/api/Logradouros").Result;
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                logradouros = System.Text.Json.JsonSerializer.Deserialize<List<Logradouro>>
                    (stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
            }

            return View(logradouros);
        }

        // GET: Logradouros/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            HttpResponseMessage response = client.GetAsync($"/api/Logradouros/{id}").Result;

            var stringResponse = await response.Content.ReadAsStringAsync();

            var logradouro = System.Text.Json.JsonSerializer.Deserialize<Logradouro>
                    (stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

            if (logradouro != null)
                return View(logradouro);
            else
                return HttpNotFound();
        }

        // GET: Logradouros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logradouros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome")] Logradouro logradouro)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(logradouro);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync("/api/Logradouros", byteContent);
                
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Erro ao cadastrar o logradouro.";
                    return View();
                }
            }

            return View(logradouro);
        }

        // GET: Logradouros/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage response = client.GetAsync($"/api/Logradouros/{id}").Result;

            var stringResponse = await response.Content.ReadAsStringAsync();

            var logradouro = System.Text.Json.JsonSerializer.Deserialize<Logradouro>
                    (stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

            if (logradouro == null)
            {
                return HttpNotFound();
            }
            return View(logradouro);
        }

        // POST: Logradouros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Endereco")] Logradouro logradouro)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(logradouro);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");                

                HttpResponseMessage response = client.PutAsync($"/api/Logradouros/{logradouro.Id}", byteContent).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Erro ao editar o logradouro.";
                    return View();
                }
            }
            return View(logradouro);
        }

        // GET: Logradouros/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage response = client.GetAsync($"/api/Logradouros/{id}").Result;

            var stringResponse = await response.Content.ReadAsStringAsync();

            var logradouro = System.Text.Json.JsonSerializer.Deserialize<Logradouro>
                    (stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

            if (logradouro == null)
            {
                return HttpNotFound();
            }
            return View(logradouro);
        }

        // POST: Logradouros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/Logradouros/{id}");

            if (!response.IsSuccessStatusCode)
                return HttpNotFound();

            return RedirectToAction("Index");
        }
    }
}
