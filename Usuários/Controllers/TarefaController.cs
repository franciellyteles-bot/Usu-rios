using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Usuários.Data;
using Usuários.Models;

namespace TarefaController.cs
{
   public class TarefaController : ControllerBase
    {
        private readonly UsuarioContext _context;

        public TarefaController(UsuarioContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult RetornaTarefa(int id)
        {
            var Tarefa = _context.Tarefas.Find(id);
            if (Tarefa == null)
            {
                return NotFound("Cliente não encontrada");
            }
            return Ok(Tarefa);
        }
        [HttpPost]
        public IActionResult CadastraTarefa(Tarefa tarefa)
        {
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
            TarefasDoBanco.IdPessoa= tarefa.IdPessoa;
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

