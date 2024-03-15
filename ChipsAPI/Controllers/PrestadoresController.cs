using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChipsAPI.Context;
using ChipsAPI.Models;
using ChipsAPI.DTOs;
using AutoMapper;
using AnticorAPI.Utils;
using ChipsAPI.Utils;

namespace ChipsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestadoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IMapper mapper;

        public PrestadoresController(AppDbContext context, IMapper mapper)
        {
            _context = context;            
            this.mapper = mapper;
        }

        //// GET: api/Prestador (sin paginación)
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PrestadorDTO>>> GetPrestadores()
        //{
        //    //Sin Mapper
        //    //return await _context.Prestadores.ToListAsync();

        //    //Con Mapper (mapeo al respectivo DTO)            
        //    var prestadores = await _context.Prestadores.ToListAsync();
        //    return mapper.Map<List<PrestadorDTO>>(prestadores);
        //}

        // GET: api/Prestador (Con paginación)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrestadorDTO>>> GetPrestadores([FromQuery] PaginacionDTO paginacionDTO)
        {
            //Con Mapper (mapeo al respectivo DTO)            
            var queryable =  _context.Prestadores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var prestadores = await queryable             
                .OrderBy(x=>x.PrimerAp+x.SegundoAp)
                .Paginar(paginacionDTO)
                    //.Select(x => new PrestadorDTO
                    //{
                    //    Id = x.Id, // Inicializamos otros campos necesarios
                    //    Nombres = x.Nombres,
                    //    PrimerAp = x.PrimerAp,
                    //    SegundoAp = x.SegundoAp,
                    //    HoraEntrada = x.HoraEntradaBase,
                    //    HoraSalida = x.HoraSalidaBase,
                    //    Email = x.Email,
                    //    Apellidos = $"{x.PrimerAp} {x.SegundoAp}", // Aquí combinamos PrimerAp y SegundoAp
                    //})
                .ToListAsync();
            return mapper.Map<List<PrestadorDTO>>(prestadores);
        }



        // GET: api/Prestador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestador>> GetPrestador(int id)
        {
            var prestador = await _context.Prestadores.FindAsync(id);

            if (prestador == null)
            {
                return NotFound();
            }

            return prestador;
        }

        // PUT: api/Prestador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestador(int id, Prestador prestador)
        {
            if (id != prestador.Id)
            {
                return BadRequest();
            }

            _context.Entry(prestador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestadorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Prestador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prestador>> PostPrestador(PrestadorCreacionDTO prestadorCreacionDTO)
        {
            var prestador = mapper.Map<Prestador>(prestadorCreacionDTO);
            _context.Prestadores.Add(prestador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrestador", new { id = prestador.Id }, prestador);
        }

        // DELETE: api/Prestador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestador(int id)
        {
            var prestador = await _context.Prestadores.FindAsync(id);
            if (prestador == null)
            {
                return NotFound();
            }

            _context.Prestadores.Remove(prestador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrestadorExists(int id)
        {
            return _context.Prestadores.Any(e => e.Id == id);
        }
    }
}
