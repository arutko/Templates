using Template.Common.Enums;

namespace Template.Dto.Responses;

public class AccountRequestDto
{
    public Guid Id { get; set; }
    public Place EnrolledAt { get; set; }
}
