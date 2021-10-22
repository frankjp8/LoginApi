using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFechas.Models;

namespace ApiFechas.Controllers
{
    [ApiController]
    [Route("api/{Controller}")]
    public class FechaController : Controller
    {
        private readonly FechasDBContext _db;
        public FechaController(FechasDBContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetFechas()
        {
            try
            {
                var fechas = _db.Fechas.Select(x => x.FechaCreacion);

                var items = _db.Fechas
                    //.Where(x => int.Parse((DateTime.Now - DateTime.Parse(x.FechaCreacion.Date.ToString())).ToString(@"dd")) <= 7)
                    .Select(x => int.Parse((DateTime.Now - DateTime.Parse(x.FechaCreacion.Date.ToString())).ToString(@"dd")) <= 7
                    ? x.Cantidad : 0).Select(x => x.Value).ToList().Sum();

                var items1 = _db.Fechas
                    //.Where(x => int.Parse((DateTime.Now - DateTime.Parse(x.FechaCreacion.Date.ToString())).ToString(@"dd")) <= 7)
                    .Select(x => int.Parse((DateTime.Now - DateTime.Parse(x.FechaCreacion.Date.ToString())).ToString(@"dd")) <= 90
                    ? x.Cantidad : 0).Select(x => x.Value).ToList().Sum();

                var items2 = _db.Fechas
                    //.Where(x => int.Parse((DateTime.Now - DateTime.Parse(x.FechaCreacion.Date.ToString())).ToString(@"dd")) <= 7)
                    .Select(x => int.Parse((DateTime.Now - DateTime.Parse(x.FechaCreacion.Date.ToString())).ToString(@"dd")) <= 365
                    ? x.Cantidad : 0).Select(x => x.Value).ToList().Sum();


                return Ok(items2);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
