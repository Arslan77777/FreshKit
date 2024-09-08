using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entity;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShirstsController : ControllerBase
    {
        private readonly FreshKit _dbcontext;

        public ShirstsController(FreshKit dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpPost("AddShirts")]
        public IActionResult AddShirts([FromBody]Shirsts shirsts)
        {
            _dbcontext.Shakes.Add(shirsts);
            bool Result= _dbcontext.SaveChanges()>0;
            return Ok(Result);
        }
        [HttpPost("UpdateShirts")]
        public IActionResult UpdateShirts([FromBody] Shirsts shirsts)
        {
            _dbcontext.Shakes.Update(shirsts);
            bool Result = _dbcontext.SaveChanges() > 0;
            return Ok(Result);
        }
        [HttpGet("GetAllShirts")]
        public IActionResult GetAllShirts()
        {
            return Ok(_dbcontext.Shakes.ToList());
        }
        [HttpGet("GetShirtDetails")]
        public IActionResult GetShirtDetails(int ShirtId)
        {
            var shirt = _dbcontext.Shakes.Find(ShirtId);
            if(shirt == null)
            {
                return NotFound();
            }
            return Ok(shirt);
        }
        [HttpGet("Delete")]
        public IActionResult Delete(int ShirtId)
        {
            var shirt = _dbcontext.Shakes.Find(ShirtId);
            if (shirt == null)
            {
                return NotFound();
            }
            _dbcontext.Shakes.Remove(shirt);
            bool Result = _dbcontext.SaveChanges() > 0;
            return Ok(Result);
        }
    }
}
