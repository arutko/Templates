namespace Template.Api.Controllers.Models.Base;

public abstract class Entity
{
    public Guid Id { get; set; }
    public bool Active { get; set; }
    public DateTime? DateCreated { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? DateModified { get; set; }
    public string? ModifiedBy { get; set; }
}
