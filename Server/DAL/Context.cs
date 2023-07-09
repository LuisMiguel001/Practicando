using Microsoft.EntityFrameworkCore;
using Practicando.Shared.Models;

namespace Practicando.Server.DAL
{
	public class Context : DbContext
	{
		public Context(DbContextOptions<Context> options)
			: base(options) { }

		public DbSet<Ingresos>	Ingresos { get; set; }
	}
}
