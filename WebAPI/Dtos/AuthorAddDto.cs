using Core.Entities;

namespace WebAPI.Dtos;

public class AuthorAddDto : IDto
{
    public string Name { get; set; }
    public string Email { get; set; }
}