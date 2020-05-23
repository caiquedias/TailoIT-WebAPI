using WEB_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;

namespace WEB_API.DAL
{
    public class FuncionarioDAL
    {
        private FuncionarioContext db = new FuncionarioContext();
        public IQueryable<Funcionario> TodosFuncionarios()
        {
            try
            {
                return db.Set<Funcionario>();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void CadastrarFuncionario(Funcionario func)
        {
            try
            {
                db.Funcionarios.Add(func);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public Funcionario FuncionarioPorId(int id)
        {
            try
            {
                Funcionario funcionario = db.Funcionarios.Find(id);
                return funcionario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void EditarFuncionario(Funcionario funcionario)
        {
            db.Entry(funcionario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(funcionario.ID))
                {
                    throw new Exception("Funcionário inexistente");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool FuncionarioExists(int id)
        {
            return db.Funcionarios.Count(e => e.ID == id) > 0;
        }

        public void DeletarFuncionario(int id)
        {
            try
            {
                Funcionario funcionario = db.Funcionarios.Find(id);

                db.Entry(funcionario).State = EntityState.Deleted;

                db.Funcionarios.Remove(funcionario);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}