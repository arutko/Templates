using Template.Api.Controllers.Models.Base;
using Template.Common.Enums;

namespace Template.Api.Controllers.Models;

public class Account : Entity
{
    public Place EnrolledAt { get; set; }

    public static Account Create(bool active, Place enrolledAt)
        => new Account()
        {
            Active = active,
            EnrolledAt = enrolledAt,
        };
}
