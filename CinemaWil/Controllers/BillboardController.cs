using Domain.Dtos;
using Domain.Interface;
using Infractructure.Data;
using Infractructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWil.Controllers
{
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

        [HttpGet]
        //Documentar
        public async Task<IActionResult> GetBillboard()
        {
            return Ok(await _billboardServices.GetBillboard());
        }


        [HttpPost]
        public async Task<IActionResult> AddBillboard(BillboardDto addBillboard)
        {
            try
            {

                if (addBillboard is null)
                {
                    return BadRequest(400);
                }
                else
                {
                    await _billboardServices.MapBillboardObject(addBillboard);
                }
                return Ok("Se ha creado correctamente");

            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }

        }

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
