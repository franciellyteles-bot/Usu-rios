using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Usuários.Data;
using Usuários.Models;

namespace TarefaController.cs
{
    [ApiController]
    [Route("{controller}")]
   public class TarefaController : ControllerBase
    {
        private readonly UsuarioContext _context;

        public TarefaController(UsuarioContext context)
        {
            _context = context;
        }
     
        [HttpGet]
        public IActionResult BuscarTarefas()
        {
            var UsuarioLogado = HttpContext.Session.GetString("UsuarioLogado");
            if (UsuarioLogado == null)
            {
                return Unauthorized("Faça login antes!");
            }
            var idPessoasLogado = Request.Cookies["UsuarioLogado"];
            if (idPessoasLogado!= null)
            {
                var Usuários = from u in _context.Pessoas
                               join t in _context.Tarefas
                               on u.Id equals t.IdPessoa
                               where u.Id== int.Parse(idPessoasLogado)
                               select new
                               {
                                   Pessoas = u.Nome,u.Email,
                                   Tarefa = t.Id, t.Descricao,t.Status, 
                               };
                return Ok(Usuários.ToList());
            }
            return Unauthorized("Faça login antes!");
        }
        [HttpPost]
        public IActionResult CadastraTarefa(Tarefa tarefa)
        {

            var UsuarioLogado = HttpContext.Session.GetString("UsuarioLogado");
            if (UsuarioLogado == null)
            {
                return Unauthorized("Faça login antes!");
            }
            tarefa.IdPessoa = int.Parse(UsuarioLogado);
            _context.Add(tarefa);
            _context.SaveChanges();
            return Created("", tarefa);
        }
        [HttpPut("{id}")]
        public IActionResult AtualizaTarefa(int id, Tarefa tarefa)
        {
            var TarefasDoBanco = _context.Tarefas.Find(id);
            if (TarefasDoBanco == null)
            {
                return NotFound("Cliente não existe no banco!");
            }
            TarefasDoBanco.Descricao = tarefa.Descricao;
            TarefasDoBanco.Status = tarefa.Status;

            _context.SaveChanges();
            return Ok("Atualizado");
        }
        [HttpDelete("{id}")]
        public IActionResult DeletaTarefas(int id)
        {
            var TarefasDoBanco = _context.Tarefas.Find(id);
            if (TarefasDoBanco == null)
            {
                return NotFound("Não encontrado!");
            }
            _context.Remove(TarefasDoBanco);
            _context.SaveChanges();
            return Ok("Deletado");
        }



    }
}

