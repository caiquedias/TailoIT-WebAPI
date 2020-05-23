using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUD_TailorIT.Models;
using Newtonsoft.Json;
using RestSharp;

namespace CRUD_TailorIT.Controllers
{
    public class FuncionariosController : Controller
    {
        // GET: Funcionarios
        public ActionResult Index()
        {
            var client = new RestClient("https://localhost:44324");
            var request = new RestRequest("api/Funcionarios", Method.GET);
            var queryResult = client.Execute(request);

            if (queryResult.IsSuccessful)
            {
                IEnumerable<Funcionario> funcList = JsonConvert.DeserializeObject<IEnumerable<Funcionario>>(queryResult.Content);
                if (!funcList.Count().Equals(0))
                    return View(funcList.AsQueryable<Funcionario>());
                else
                    return View(Enumerable.Empty<Funcionario>().AsQueryable());
            }
            else
            {
                return View(Enumerable.Empty<Funcionario>().AsQueryable());
            }
        }

        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NOME,DATANASC,EMAIL,SEXO,HABILIDADES")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {

                if (!string.IsNullOrEmpty(funcionario.EMAIL))
                    if (!ValidarEmail(funcionario.EMAIL))
                    {
                        ModelState.AddModelError("EMAIL", "Por favor, digite um email válido!");
                        return View(funcionario);
                    }

                if(!ValidaSobrenome(funcionario.NOME))
                {
                    ModelState.AddModelError("NOME", "Por favor, digite o nome completo!");
                    return View(funcionario);
                }

                if(!ValidarIdade(funcionario.DATANASC))
                {
                    ModelState.AddModelError("DATANASC", "Não é possível cadastrar um funcionário com idade inferior a 18 anos");
                    return View(funcionario);
                }

                var client = new RestClient("https://localhost:44324");
                var request = new RestRequest("api/Funcionarios", Method.POST);
                request.AddJsonBody(funcionario);
                client.Execute(request);

                return RedirectToAction("Index");
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = new RestClient("https://localhost:44324");
            var request = new RestRequest(string.Format("api/Funcionarios/{0}", id), Method.GET);
            var queryResult = client.Execute(request);

            Funcionario func = null;

            if (queryResult.IsSuccessful)
            {
                func = JsonConvert.DeserializeObject<Funcionario>(queryResult.Content);
            }

            if (func == null)
            {
                return HttpNotFound();
            }
            else
            {
                int sexo = 0;
                switch (func.SEXO)
                {
                    case "Masculino":
                        sexo = 2;
                        break;
                    case "Feminino":
                        sexo = 1;
                        break;
                    case "Outro":
                        sexo = 0;
                        break;
                }

                var selectList = new SelectList(
                    new List<SelectListItem>
                        {
                    new SelectListItem {Text = "Masculino", Value ="2"},
                    new SelectListItem {Text = "Feminino", Value = "1"},
                    new SelectListItem {Text = "Outro", Value = "0"},
                    }, "Value", "Text", sexo);

                ViewBag.Sexo = selectList;
            }

            return View(func);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOME,DATANASC,EMAIL,SEXO,HABILIDADES")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                var client = new RestClient("https://localhost:44324");
                var request = new RestRequest(String.Format("api/Funcionarios/{0}", funcionario.ID), Method.PUT);

                request.AddJsonBody(funcionario);

                client.Execute(request);

                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = new RestClient("https://localhost:44324");
            var request = new RestRequest(string.Format("api/Funcionarios/{0}", id), Method.GET);
            var queryResult = client.Execute(request);

            Funcionario func = null;

            if (queryResult.IsSuccessful)
            {
                func = JsonConvert.DeserializeObject<Funcionario>(queryResult.Content);
            }

            if (func == null)
            {
                return HttpNotFound();
            }
            return View(func);
        }

        // GET: Funcionarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = new RestClient("https://localhost:44324");
            var request = new RestRequest(string.Format("api/Funcionarios/{0}", id), Method.GET);
            var queryResult = client.Execute(request);

            Funcionario func = null;

            if (queryResult.IsSuccessful)
            {
                func = JsonConvert.DeserializeObject<Funcionario>(queryResult.Content);
            }

            if (func == null)
            {
                return HttpNotFound();
            }
            return View(func);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var client = new RestClient("https://localhost:44324");
            var request = new RestRequest(string.Format("api/Funcionarios/{0}", id), Method.DELETE);

            client.Execute(request);

            return RedirectToAction("Index");
        }

        private static bool ValidarEmail(string strEmail)
        {
            string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (System.Text.RegularExpressions.Regex.IsMatch(strEmail, strModelo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidaSobrenome(string nome)
        {
            string[] nomeCompleto = nome.Split(' ');
            if (nomeCompleto.Length < 2)
                return false;
            else 
            {
                if (nomeCompleto[1].Length < 3)
                    return false;
                else
                {
                    if (nomeCompleto[1].ToUpper().Equals("DOS") || nomeCompleto[1].ToUpper().Equals("DAS"))
                        return false;
                    else
                        return true;
                }
            }
        }

        private bool ValidarIdade(DateTime DataNasc)
        {
            int idade = DateTime.Now.Date.Subtract(DataNasc.Date).Days / 365;

            if (idade < 18)
                return false;
            else
                return
                    true;
        }


    }
}
