using WebApplication1.DTO;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public List<AnimalReadDto> getAnimals(string orderBy)
    {
        var animals = _animalRepository.getAnimals(orderBy);
        return animals.Select(a => new AnimalReadDto
        {
            IdAnimal = a.IdAnimal,
            Name = a.Name,
            Description = a.Description,
            Category = a.Category,
            Area = a.Area
        }).ToList();
    }

    public bool createAnimal(AnimalSaveDto animalSaveDto)
    {
        var animal = new Animal
        {
            Name = animalSaveDto.Name,
            Description = animalSaveDto.Description,
            Category = animalSaveDto.Category,
            Area = animalSaveDto.Area
        };
        return _animalRepository.createAnimal(animal);
    }
    
    public bool updateAnimal(int animalId, AnimalUpdateDto animalUpdateDto)
    {
        var animal = new Animal
        {
            IdAnimal = animalId,
            Name = animalUpdateDto.Name,
            Description = animalUpdateDto.Description,
            Category = animalUpdateDto.Category,
            Area = animalUpdateDto.Area
        };
        return _animalRepository.updateAnimal(animalId, animal);
    }
    
    public bool deleteAnimal(int animalId)
    {
        return _animalRepository.deleteAnimal(animalId);
    }
}