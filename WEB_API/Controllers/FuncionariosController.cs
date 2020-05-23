using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WEB_API.BLL;
using WEB_API.Models;

namespace WEB_API.Controllers
{
    public class FuncionariosController : ApiController
    {
        private FuncionarioBLL funcBLL = new FuncionarioBLL();

        // GET: api/Funcionarios
        [HttpGet]
        public IHttpActionResult GetFuncionarios()
        {
            return Ok(funcBLL.TodosFuncionarios());
        }

        // GET: api/Funcionarios/5
        [HttpGet]
        [ResponseType(typeof(Funcionario))]
        public IHttpActionResult GetFuncionario(int id)
        {
            Funcionario funcionario = funcBLL.FuncionarioPorId(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        // PUT: api/Funcionarios/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFuncionario(int id, Funcionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != funcionario.ID)
            {
                return BadRequest();
            }

            funcBLL.EditarFuncionario(funcionario);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Funcionarios
        [HttpPost]
        [ResponseType(typeof(Funcionario))]
        public IHttpActionResult PostFuncionario(Funcionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            funcBLL.CadastrarFuncionario(funcionario);

            return CreatedAtRoute("DefaultApi", new { id = funcionario.ID }, funcionario);
        }

        // DELETE: api/Funcionarios/5
        [HttpDelete]
        [ResponseType(typeof(Funcionario))]
        public IHttpActionResult DeleteFuncionario(int id)
        {
            funcBLL.DeletarFuncionario(id);

            return Ok();
        }
        
    }
}