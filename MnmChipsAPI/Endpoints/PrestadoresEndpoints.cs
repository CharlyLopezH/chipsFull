
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MnmChipsAPI.DTOs;
using MnmChipsAPI.Entidades;
using MnmChipsAPI.Repositorios;
using MnmChipsAPI.Servicios;

namespace MnmChipsAPI.Endpoints
{
    public static class PrestadoresEndpoints
    {
       

        private static readonly string contenedor = "prestadores";
        public static RouteGroupBuilder MapPrestadores(this RouteGroupBuilder group)
        {
            group.MapPost("/", Crear).DisableAntiforgery();
            group.MapGet("/", ObtenerTodos).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(40)).Tag("prestadores-get"));
            group.MapGet("/{id:int}", ObtenerPorId);
            group.MapPut("/prestadores/{id:int}", Actualizar).DisableAntiforgery();
            group.MapDelete("/prestadores/{id:int}", Borrar);
            return group;
        }

        static async Task<Results<NoContent, NotFound>> Borrar(int id, IRepositorioPrestadores repositorio,IOutputCacheStore outputCacheStore,
                                 IAlmacenadorArchivos almacenadorArchivos)
        {
            //Ubica el registro que quieres borrar
            var prestadorDB = await repositorio.ObtenerPorId(id);
            //Si el id no existe, regresa un NotFound
            if (prestadorDB is null)
            {
                return TypedResults.NotFound();
            }
            //Si el id Existe, bórralo
            await repositorio.Borrar(id);
            await almacenadorArchivos.Borrar(prestadorDB.Foto, contenedor);
            await outputCacheStore.EvictByTagAsync("actores-get", default);
            //y regresa un NoContent (sin contenido)
            
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> Actualizar(int id,
            [FromForm] CrearPrestadorDTO crearPrestadorDTO, IRepositorioPrestadores repositorio,
            IAlmacenadorArchivos almacenadorArchivos, IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var prestadorDB = await repositorio.ObtenerPorId(id);

            if (prestadorDB is null)
            {
                return TypedResults.NotFound();
            }

            var prestadorParaActualizar = mapper.Map<Prestador>(crearPrestadorDTO);
            prestadorParaActualizar.Id = id;
            prestadorParaActualizar.Foto = prestadorDB.Foto;

            if (crearPrestadorDTO.Foto is not null)
            {
                var url = await almacenadorArchivos.Editar(prestadorParaActualizar.Foto,
                    contenedor, crearPrestadorDTO.Foto);
                prestadorParaActualizar.Foto = url;
            }

            await repositorio.Actualizar(prestadorParaActualizar);
            await outputCacheStore.EvictByTagAsync("prestadores-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<Ok<PrestadorDTO>, NotFound>> ObtenerPorId(int id,
            IRepositorioPrestadores repositorio, IMapper mapper)
        {
            var prestador = await repositorio.ObtenerPorId(id);

            if (prestador is null)
            {
                return TypedResults.NotFound();
            }

            var prestadorDTO = mapper.Map<PrestadorDTO>(prestador);
            return TypedResults.Ok(prestadorDTO);
        }

        private static async Task<Ok<List<PrestadorDTO>>> ObtenerTodos(IRepositorioPrestadores repositorio, IMapper mapper, 
            int pagina = 1, int recordsPorPagina = 10)
        {
            var paginacion = new PaginacionDTO { Pagina = pagina, RecordsPorPagina = recordsPorPagina };
            var prestadores = await repositorio.ObtenerTodos(paginacion);
            var prestadoresDTO = mapper.Map<List<PrestadorDTO>>(prestadores);
            return TypedResults.Ok(prestadoresDTO);
        }

        static async Task<Created<PrestadorDTO>> Crear([FromForm]CrearPrestadorDTO crearPrestadorDTO,
            IRepositorioPrestadores repositorio,IOutputCacheStore outputCacheStore, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            var prestador = mapper.Map<Prestador>(crearPrestadorDTO);

            if (crearPrestadorDTO.Foto is not null)
            {
                var url = await almacenadorArchivos.Almacenar(contenedor, crearPrestadorDTO.Foto);
                prestador.Foto = url;
            }

            var id = await repositorio.Crear(prestador);
            await outputCacheStore.EvictByTagAsync("prestadores-get", default);
            var prestadorDTO = mapper.Map<PrestadorDTO>(prestador);
            return TypedResults.Created($"/prestadores/{id}", prestadorDTO);
        }

    }
}
