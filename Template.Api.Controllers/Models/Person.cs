using Template.Api.Controllers.Models.Base;

namespace Template.Api.Controllers.Models;

public class Person : Entity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}
