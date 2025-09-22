using AgendaContatos.Conexao;
using AgendaContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class AgendaController : ControllerBase
{
    private readonly DataBaseConexao _context;

    public AgendaController(DataBaseConexao context)
    {
        _context = context;
    }

    [HttpGet("list")]
    public async Task<IActionResult> List([FromQuery] string? filtroNome)
    {
        var contatos = from c in _context.Contatos select c;

        if (!string.IsNullOrEmpty(filtroNome))
        {
            contatos = contatos.Where(c => c.Nome.Contains(filtroNome));
        }
        return Ok(await contatos.ToListAsync());
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] Contatos contato)
    {
        if (ModelState.IsValid)
        {
            contato.CPF = contato.CPF.Replace(".", "").Replace("-", "");
            contato.Telefone = contato.Telefone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");

            contato.DataCadastro = DateTime.UtcNow;
            contato.UltimaAlteracao = DateTime.UtcNow;
            _context.Contatos.Add(contato);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao salvar alterações no banco de dados.", ex);
            }

            return Ok(contato);
        }
        return BadRequest(ModelState);
    }

    [HttpGet("getOne/{id}")]
    public async Task<IActionResult>GetOne(int id)
    {
        var contato = await _context.Contatos.FindAsync(id);
        if (contato == null) return NotFound();
        return Ok(contato);
    }
    [HttpPost("edit/{id}")]
    public async Task<IActionResult> EditConfirmed(int id, [FromBody] Contatos contato)
    {
        if (id != contato.Id) return BadRequest();

        if (ModelState.IsValid)
        {
            contato.CPF = contato.CPF.Replace(".", "").Replace("-", "");
            contato.Telefone = contato.Telefone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
            contato.UltimaAlteracao = DateTime.UtcNow;

            try
            {
                _context.Update(contato);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Contatos.Any(e => e.Id == contato.Id)) return NotFound();
                throw;
            }

            return Ok(contato);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var contato = await _context.Contatos.FindAsync(id);
        if (contato == null) return NotFound();

        _context.Contatos.Remove(contato);
        await _context.SaveChangesAsync();
        return Ok("Deletado");
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var contato = await _context.Contatos.FirstOrDefaultAsync(m => m.Id == id);
        if (contato == null) return NotFound();
        return Ok(contato);
    }
}
