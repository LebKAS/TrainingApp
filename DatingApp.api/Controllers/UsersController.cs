using AutoMapper;
using DatingApp.api.Data;
using DatingApp.api.Dtos;
using DatingApp.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.api.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repo, IMapper mapper )
        {
           _repo = repo;
           _mapper = mapper;
        }
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(){
            var users = await _repo.GetUsers();
          
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersToReturn);
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
           

            var userToReturn =_mapper.Map<UserForDetailDto>(user);

            return Ok(userToReturn);

        }
    }
}