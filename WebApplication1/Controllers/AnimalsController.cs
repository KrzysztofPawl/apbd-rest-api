using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.DTO;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers;

[Route("/api/animals")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalService _animalService;

    public AnimalsController(IAnimalService animalService)
    {
        _animalService = animalService;
    }
    
    [HttpGet]
    public IActionResult getAnimals([FromQuery] string orderBy = "name")
    {
        var animals = _animalService.getAnimals(orderBy);

        if (animals.Count.Equals(0))
        {
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }
        
        return Ok(animals);
    }
    
    [HttpPost]
    public IActionResult createAnimal([FromBody] AnimalSaveDto animalDto)
    {
        var result = _animalService.createAnimal(animalDto);

        if (result)
        {
            return StatusCode(StatusCodes.Status201Created);
        }
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
    
    [HttpPut("{id}")]
    public IActionResult updateAnimal(int animalId, [FromBody] AnimalUpdateDto animalDto)
    {
        var result = _animalService.updateAnimal(animalId, animalDto);

        if (result)
        {
            return StatusCode(StatusCodes.Status200OK);
        }
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
    
    [HttpDelete("{id}")]
    public IActionResult deleteAnimal(int animalId)
    {
        var result = _animalService.deleteAnimal(animalId);

        if (result)
        {
            return StatusCode(StatusCodes.Status200OK);
        }
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
}