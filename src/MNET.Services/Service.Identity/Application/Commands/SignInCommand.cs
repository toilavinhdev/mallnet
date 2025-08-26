using BuildingBlock.DataExtensions;
using BuildingBlock.Identity;
using BuildingBlock.MongoDB;
using BuildingBlock.Shared.CQRS.Command;
using BuildingBlock.Shared.Localization.Abstractions;
using BuildingBlock.Shared.ValueModels;
using MongoDB.Driver;
using Service.Identity.Application.Responses;
using Service.Identity.Domain.Aggregates.UserAggregate;
using Service.Identity.Resources;

namespace Service.Identity.Application.Commands;

public record SignInCommand : ICommand<IResult>
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}

public class SignInCommandHandler(
    IMongoService mongoService,
    ILocalizationService<IdentityLocalizationResource> localization,
    IdentityOptions identityOptions)
    : ICommandHandler<SignInCommand, IResult>
{
    public async Task<IResult> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var userCursor = await mongoService.Collection<User>()
            .FindAsync(Builders<User>.Filter.Or(
                    Builders<User>.Filter.Eq(x => x.Email, request.UserName),
                    Builders<User>.Filter.Eq(x => x.PhoneNumber, request.UserName)),
                cancellationToken: cancellationToken);
        var user = await userCursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (user is null)
        {
            return Results.NotFound(new ApiResponse
            {
                Message = localization["UserNotFound"]
            });
        }
        if (user.PasswordHash != request.Password.ToSha256())
        {
            return Results.BadRequest(new ApiResponse
            {
                Message = localization["PasswordNotMatch"]
            });
        }
        
        var userClaims = new IdentityUserClaims
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Policies = user.Policies
        };
        var accessToken = userClaims.GenerateAccessToken(identityOptions);
        var refreshToken = StringExtensions.RandomString(36);
        return Results.Ok(new ApiResponse<SignInResponse>
        {
            Data = new SignInResponse
            {
                AccessToken = accessToken,
                AccessTokenExpiration = DateTimeExtensions.Now.AddMinutes(identityOptions.AccessTokenDurationInMinutes),
                RefreshToken = refreshToken,
                RefreshTokenExpiration = DateTimeExtensions.Now.AddDays(identityOptions.RefreshTokenDurationInDays),
            },
            Message = "Success"
        });
    }
}