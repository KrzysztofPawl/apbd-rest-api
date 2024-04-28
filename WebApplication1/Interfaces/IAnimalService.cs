using WebApplication1.DTO;

namespace WebApplication1.Interfaces;

public interface IAnimalService
{
    List<AnimalReadDto> getAnimals(string orderBy);
    bool createAnimal(AnimalSaveDto animalSave);
    bool updateAnimal(int animalId, AnimalUpdateDto animalUpdate);
    bool deleteAnimal(int animalId);
}