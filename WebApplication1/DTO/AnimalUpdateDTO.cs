using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO;

public class AnimalUpdateDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(200, ErrorMessage = "Name cannot be longer than 200 characters")]
    public string Name { get; set; }
    
    [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Category is required")]
    [StringLength(200, ErrorMessage = "Category cannot be longer than 200 characters")]
    public string Category { get; set; }
    
    [Required(ErrorMessage = "Area is required")]
    [StringLength(200, ErrorMessage = "Area cannot be longer than 200 characters")]
    public string Area { get; set; }
}