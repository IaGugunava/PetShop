using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShop.DTO;
using PetShop.Interfaces;
using PetShop.Models;

namespace PetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetRepository _petRepository;

        public PetController(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pet>> GetAllPets() {
            try
            {
                var pets = _petRepository.GetAllPets();
                return Ok(pets);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Pet> GetPetById(int id)
        {
            try
            {
                var pet = _petRepository.GetPetById(id);
                return Ok(pet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreatePet(Pet pet)
        {
            try
            {
                _petRepository.CreatePet(pet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdatePet(Pet pet)
        {
            try
            {
                _petRepository.UpdatePet(pet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeletePet(int id)
        {
            try
            {
                _petRepository.DeletePetById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
