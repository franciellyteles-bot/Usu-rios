using Microsoft.AspNetCore.Mvc;
using Usuários.Data;
using Usuários.Models;
namespace Usuários.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {

        private readonly UsuarioContext _context;

        public PessoaController(UsuarioContext context)
        {
            _context = context;
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Pessoa pessoa)
        {
            var pessoasDoBanco = _context.Pessoas.Where(p => p.Email.Equals(pessoa.Email) && p.Senha.Equals(pessoa.Senha)).ToList();
            if (pessoasDoBanco.Count == 0)
                return Unauthorized(new { mensagem = "Login inválido" });

            //criar sessão
            HttpContext.Session.SetString("UsuarioLogado", pessoasDoBanco[0].Id.ToString());

            //criar cookie com email
            Response.Cookies.Append("UsuarioLogado", pessoasDoBanco[0].Id.ToString(),
                new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30),
                    Secure = true,
                    HttpOnly = true,
                    SameSite = SameSiteMode.None

                });
            

            return Ok(new { mensagem = "Login realizado com sucesso" });
            
        }

        [HttpPost]
        public IActionResult CadastraPessoa(Pessoa pessoa)
        {
            _context.Add(pessoa);
            _context.SaveChanges();
            return Created("", pessoa);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaPessoa(int id, Pessoa pessoa)
        {
            var PessoasDoBanco = _context.Pessoas.Find(id);
            if (PessoasDoBanco == null)
            {
                return NotFound("Cliente não existe no banco!");
            }
            PessoasDoBanco.Nome = pessoa.Nome;
            PessoasDoBanco.Email = pessoa.Email;
            PessoasDoBanco.Senha = pessoa.Senha;
            _context.SaveChanges();
            return Ok("Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaPessoa(int id)
        {
            var PessoasDoBanco = _context.Pessoas.Find(id);
            if (PessoasDoBanco == null)
            {
                return NotFound("Não encontrado!");
            }
            _context.Remove(PessoasDoBanco);
            _context.SaveChanges();
            return Ok("Deletado");
        }
    }


}

