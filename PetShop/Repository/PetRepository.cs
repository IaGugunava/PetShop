using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;

namespace PetShop.Repository
{
    public class PetRepository: IPetRepository
    {
        private readonly PetShopContext _context;
        public PetRepository(PetShopContext context)
        {
            _context = context;
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return _context.Pets.ToList();
        }

        public Pet GetPetById(int id) {
            return _context.Pets.SingleOrDefault(x => x.PetId == id);
        }

        public void CreatePet(Pet pet)
        {
            if(pet == null)
            {
                throw new ArgumentNullException(nameof(pet));
            }
            _context.Pets.Add(pet);
        }

        public void UpdatePet(Pet pet)
        {
            if (PetExists(pet.PetId))
            {
                _context.Pets.Add(pet);
                _context.SaveChanges();
            } else
            {
                throw new DllNotFoundException(nameof(pet));
            }
        }

        public void DeletePetById(int id)
        {
            Pet pet = _context.Pets.SingleOrDefault(p => p.PetId == id);
            if(pet != null)
            {
                _context.Remove(pet);
            } else
            {
                throw new ArgumentNullException(nameof(pet));
            }
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.PetId == id);
        }
    }
}
