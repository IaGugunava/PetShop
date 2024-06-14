using PetShop.Models;

namespace PetShop.Interfaces
{
    public interface IPetRepository
    {
        Pet GetPetById(int id);
        IEnumerable<Pet> GetAllPets();
        void CreatePet(Pet pet);
        void UpdatePet(Pet pet);
        void DeletePetById(int id);

    }
}
