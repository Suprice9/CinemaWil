using Domain.Dtos;
using Domain.Interface;
using Domain.Models;
using Infractructure.Data;
using Infractructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWil.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BillboardController : Controller
    {
        private readonly DataBaseContext _dbContext;
        private readonly IBillboardServices _billboardServices;

        public BillboardController(DataBaseContext dbContext, IBillboardServices billboardServices)
        {
            _dbContext = dbContext;
            _billboardServices = billboardServices;
        }

        /// <summary>
        /// Retorna toda las carteleras.
        /// </summary>
        /// <response code="200">OK. Se retornaron correctamente las carteleras.</response>        
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="524">DatabaseEmpty. La Base de datos esta vacia.</response>
        [HttpGet]
        public async Task<IActionResult> GetBillboard()
        {
            return Ok(await _billboardServices.GetBillboard());
        }

        /// <summary>
        /// Crear una cartelera.
        /// </summary>
        /// <response code="200">OK. Se Creo correctamente la cartelera.</response>        
        /// <response code="400">BadRequest. Se han encontrado datos incorrectos.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="500">Internal Server Error. Ocurrio un error imprevisto.</response>
        [HttpPost]
        public async Task<IActionResult> AddBillboard(BillboardDto addBillboard)
        {
            try
            {
                var response = await _billboardServices.AddBillboardObject(addBillboard);

                if (response == "Agregado exitosamente")
                {
                    return Ok(response); 
                }
                else
                {
                    return BadRequest(400);
                }
               

            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }

        }

        /// <summary>
        /// Modificar cartelera
        /// </summary>
        /// <param name="code">id de cartelera.</param>
        /// <param name="billboard">Datos requeridos para la modificacion de una cartelera</param>
        /// <response code="200">OK. Se modifico correctamente la cartelera</response>        
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="404">NotFound. El id de la cartelera no existe.</response>        
        /// <response code="500">Internal Server Error. Ocurrio un error imprevisto.</response>
        [HttpPut]
        public async Task<IActionResult> UpdateBillboard(int code, BillboardDto billboard)
        {
            try
            {
                var result = await _billboardServices.UpdateBillboard(code, billboard);

                if (result != "Cartelera no encontrada")
                {
                    return Ok(result);
                }
                else
                {
                    {
                        return BadRequest(result);
                    }
                }

            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }
        }


        /// <summary>
        /// Eliminar cartelera.
        /// </summary>
        /// <param name="Code">Datos requeridos para la eliminacion de una cartelera.</param>
        /// <response code="200">OK. Se elimino correctamente la cartelera.</response> 
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="404">NotFound. La cartelera no existe</response>        
        /// <response code="500">Internal Server Error. Ocurrio un error imprevisto.</response>
        [HttpDelete]
        public async Task<IActionResult> DeleteBillboard(int Code)
        {
            try
            {
                var result = await _billboardServices.DeleteBillboard(Code);

                if (result != "Hubo un error")
                {
                    return Ok(result);
                }
                else
                {
                    {
                        return BadRequest();
                    }
                }

            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }
        }
    }
}
