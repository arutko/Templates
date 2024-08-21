using Template.Common.Enums;

namespace Template.Dto.Responses;

public class AccountResponseDto
{
    public Guid Id { get; set; }
    public bool Active { get; set; }
    public Place EnrolledAt { get; set; }
}
