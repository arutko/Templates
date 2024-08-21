namespace Template.Api.Controllers.Mapping;

public static class MapsterConfig
{
    public static void Configure()
    {
        //TypeAdapterConfig<AccountRequestDto, Account>.NewConfig()
        //    .Map(
        //        dest => dest.AccountAgreements,
        //        src => src.AccountAgreementsRequestDto.Select(a => a.Adapt<AccountAgreement>()).ToList());
    }
}
