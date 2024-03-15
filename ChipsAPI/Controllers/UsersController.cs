using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChipsAPI.Context;
using ChipsAPI.Models;
using AutoMapper;
using ChipsAPI.DTOs;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace ChipsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;

        public UsersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var user = await _context.Users.ToListAsync();
            return mapper.Map<List<UserDTO>>(user);
            //return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] UserCreacionDTO userCreacionDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        //Endpoint para creación de usuarios
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] UserCreacionDTO userCreacionDTO)
        {            
            try
            {
               


                User user = mapper.Map<User>(userCreacionDTO);

                // Hashear la contraseña
                string hashedPassword = BCryptNet.HashPassword(user.Password);                
                user.Password = hashedPassword;               

                _context.Add(user);
                await _context.SaveChangesAsync();
                var responseData = new
                {
                    Mensaje = "Usuario creado exitosamente",
                    Usuario = user // o cualquier otro dato que desees enviar
                };

                //return CreatedAtAction("GetUser", new { responseData.Usuario.Id }, user);
                // Devuelve un objeto simple o un mensaje
                return Ok(new { //Mensaje = "Usuario creado correctamente"
                                //
                                responseData
                                });

            } 
            catch (Exception ex) 
          
            {
                // Maneja la excepción específica de Entity Framework para violación de restricción única
                //if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException &&  sqlException.Number == 2601) // Código de error específico para violación de restricción única                
                //{
                // Puedes lanzar una excepción personalizada o manejarla de alguna manera                    
                //throw new InvalidOperationException("Error al insertar debido a duplicidad de datos"+ex.Message);
                //}
                // Manejo de otras excepciones o rethrow si no son de interés
                //return StatusCode(500, new { Mensaje = "Error en la operación", Detalles = ex.Message });
                var errorResponse = new
                {
                    Mensaje = "Error al procesar la solicitud.---> "+ex.Message
                };

                return StatusCode(500, errorResponse);
            }
            //return NoContent();            
        }

        // DELETE: api/Users/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                //404
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            //return NoContent();
            var responseData = new
            {
                msg = "Registro eliminado correctamente: " + id
            };
            return Ok( responseData );
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
