using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Template.Api.Controllers.DatabaseContext.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        var role1Id = "cac43a6e-f7bb-4448-baaf-1add431ccbbf";
        var role2Id = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf";

        builder.HasData(
            new IdentityRole
            {
                Id = role1Id,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = role2Id,
                Name = "Person",
                NormalizedName = "PERSON"
            }
        );
    }
}
