using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IAnimalRepository
{
    List<Animal> getAnimals(string orderBy);
    bool createAnimal(Animal animal);
    bool updateAnimal(int animalId, Animal animal);
    bool deleteAnimal(int animalId);
}