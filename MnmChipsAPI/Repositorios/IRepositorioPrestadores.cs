using MnmChipsAPI.DTOs;
using MnmChipsAPI.Entidades;

namespace MnmChipsAPI.Repositorios
{
    public interface IRepositorioPrestadores
    {
        Task<int> Crear(Prestador prestador);
        Task<List<Prestador>> ObtenerTodos(PaginacionDTO paginacionDTO);
        Task<Prestador?> ObtenerPorId(int id);
        Task<bool> Existe(int id);
        Task<List<int>> Existen(List<int> ids);
        Task Actualizar(Prestador prestador);
        Task Borrar(int id);
    }
}
