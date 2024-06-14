using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShop.Interfaces;
using PetShop.Repository;
using PetShop.Models;
using Azure.Core;
using PetShop.DTO;

namespace PetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public IActionResult Register(UserDto user)
        {
            var userAcc = _mapper.Map<User>(user);

            try
            {
                var createdUser = _userRepository.CreateUser(userAcc, user.Password);
                return Ok(createdUser);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(string email, string password) {
            var user = _userRepository.Login(email, password);

            if(user == null) { 
                return Unauthorized("Invalid account");
            }

            var token = _userRepository.GenerateToken(user);
            var refreshToken = _userRepository.GenerateRefreshToken();
            _userRepository.SetRefreshToken(user, refreshToken);

            return Ok(user);
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllAccounts()
        {
            try
            {
                var users = _userRepository.GetUsers();
                var usersAcc = _mapper.Map<IEnumerable<User>>(users);
                return Ok(usersAcc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);
                var userAcc = _mapper.Map<User>(user);
                return Ok(userAcc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateUser(User user) {
            try
            {
                _userRepository.UpdateUser(user);
                return Ok();
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userRepository.DeleteUserById(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Order")]
        public IActionResult OrderPet(int userId, int petId) {
            try
            {
                var order = _userRepository.OrderPet(userId, petId);
                return Ok(order);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
