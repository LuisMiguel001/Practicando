using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practicando.Server.DAL;
using Practicando.Shared.Models;

namespace Practicando.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IngresosController : ControllerBase
{
	private readonly Context _context;

	public IngresosController(Context context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Ingresos>>> GetIngresos()
	{
		if (_context.Ingresos == null)
		{
			return NotFound();
		}
		return await _context.Ingresos.ToListAsync();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Ingresos>> GetIngresos(int id)
	{
		if(_context.Ingresos == null)
		{
			return NotFound();
		}

		var ingreso = await _context.Ingresos.FirstOrDefaultAsync();

		if(ingreso == null)
		{
			return NotFound();
		}
		return ingreso;
	}

	[HttpPost]
	public async Task<ActionResult<Ingresos>> PostIngresos(Ingresos ingreso)
	{
		if (!Existe(ingreso.IngresoId))
			_context.Ingresos.Add(ingreso);
		else
			_context.Ingresos.Update(ingreso);

		await _context.SaveChangesAsync();
		return Ok(ingreso);
	}

	[HttpDelete]
	public async Task<ActionResult> DeleteIngresos(int id)
	{
		if (_context.Ingresos == null)
		{
			return NotFound();
		}

		var ingresos = await _context.Ingresos.FindAsync(id);

		if(ingresos == null)
		{
			return NotFound();
		}

		_context.Ingresos.Remove(ingresos);
		await _context.AddRangeAsync(ingresos);

		return NoContent();
	}

	public bool Existe(int id)
	{
		return (_context.Ingresos?.Any(i => i.IngresoId == id)).GetValueOrDefault();
	}
}

