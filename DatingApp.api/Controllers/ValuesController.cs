using Microsoft.AspNetCore.Mvc;
using DatingApp.api.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _cntxt;
        public ValuesController(DataContext cntxt)
        {
            this._cntxt = cntxt;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _cntxt.Values.ToListAsync();
            return Ok(values);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _cntxt.Values.FirstOrDefaultAsync (x => x.Id == id);
            return Ok( value);

        }


    }
}