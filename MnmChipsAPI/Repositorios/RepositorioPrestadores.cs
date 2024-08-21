using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MnmChipsAPI.DTOs;
using MnmChipsAPI.Entidades;
using MnmChipsAPI.Utilidades;


namespace MnmChipsAPI.Repositorios
{
    public class RepositorioPrestadores(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IRepositorioPrestadores
    {
        private readonly ApplicationDbContext context = context;
        private readonly HttpContext httpContext = httpContextAccessor.HttpContext!;

        public async Task Actualizar(Prestador prestador)
        {
            context.Update(prestador);
            await context.SaveChangesAsync();
        }

        public async Task Borrar(int id)
        {
            await context.Prestadores.Where(p=> p.Id == id).ExecuteDeleteAsync();
        }

        public async Task<int> Crear(Prestador prestador)
        {
            context.Add(prestador);
            await context.SaveChangesAsync();
            return prestador.Id;

        }

        public async Task<bool> Existe(int id)
        {
            return await context.Prestadores.AnyAsync(a => a.Id == id);
        }

        public async Task<List<int>> Existen(List<int> ids)
        {
            return await context.Prestadores.Where(a => ids.Contains(a.Id)).Select(a => a.Id).ToListAsync();
        }

        public async Task<Prestador?> ObtenerPorId(int id)
        {
            return await context.Prestadores.AsNoTracking().FirstOrDefaultAsync(a=>a.Id==id);
        }

        public async Task<List<Prestador>> ObtenerTodos(PaginacionDTO paginacionDTO)
        {
            var queryable = context.Prestadores.AsQueryable();
            await httpContext.InsertarParametrosPaginacionEnCabecera(queryable);           
            return await queryable.OrderBy(a => a.Nombres).Paginar(paginacionDTO).ToListAsync();
        }
    }
}
