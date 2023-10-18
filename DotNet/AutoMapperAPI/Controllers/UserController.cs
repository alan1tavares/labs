using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoMapperAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            var user = GetUser();
            var userView = _mapper.Map<UserViewModel>(user);
            return Ok(userView);
        }

        [HttpGet("Other")]
        public IActionResult GetOther()
        {
            var user = GetUser();
            var userViewOther = _mapper.Map<UserViewModelOther>(user);
            return Ok(userViewOther);
        }

        private User GetUser()
        {
            return new User()
            {
                Id = 1,
                FirstName = "Allan",
                LastName = "Turing",
                Address = "Inglaterra",
                Email = "alan_turing@gmail.com"
            };
        }

    }
}