using WEB_API.DAL;
using WEB_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WEB_API.BLL
{
    public class FuncionarioBLL
    {
        private FuncionarioDAL funcDAL = new FuncionarioDAL();
        public IQueryable<Funcionario> TodosFuncionarios()
        {
            try
            {
                return funcDAL.TodosFuncionarios();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CadastrarFuncionario(Funcionario func)
        {
            try
            {
                funcDAL.CadastrarFuncionario(func);
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
                return funcDAL.FuncionarioPorId(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void EditarFuncionario(Funcionario funcionario)
        {
            try
            {
                funcDAL.EditarFuncionario(funcionario);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeletarFuncionario(int id)
        {
            try
            {
                funcDAL.DeletarFuncionario(id);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}