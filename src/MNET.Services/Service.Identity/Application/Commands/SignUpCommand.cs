using BuildingBlock.DataExtensions;
using BuildingBlock.MongoDB;
using BuildingBlock.Shared.CQRS.Command;
using BuildingBlock.Shared.Localization.Abstractions;
using BuildingBlock.Shared.ValueModels;
using MongoDB.Bson;
using MongoDB.Driver;
using Service.Identity.Domain.Aggregates.UserAggregate;
using Service.Identity.Resources;

namespace Service.Identity.Application.Commands;

public record SignUpCommand : ICommand<IResult>
{
    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;
}

public class SignUpCommandHandler(
    IMongoService mongoService,
    ILocalizationService<IdentityLocalizationResource> localization)
    : ICommandHandler<SignUpCommand, IResult>
{
    public async Task<IResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var userFilterBuilder = Builders<User>.Filter.Or(
            Builders<User>.Filter.Eq(x => x.Email, request.Email),
            Builders<User>.Filter.Eq(x => x.PhoneNumber, request.PhoneNumber));
        var userCursor = await mongoService.Collection<User>()
            .FindAsync(userFilterBuilder, cancellationToken: cancellationToken);
        var userExists = await userCursor.AnyAsync(cancellationToken: cancellationToken);
        if (userExists)
        {
            return Results.Conflict(new ApiResponse
            {
                Message = localization["PhoneOrEmailAlreadyExists"]
            });
        }

        var user = new User
        {
            Id = ObjectId.GenerateNewId().ToString(),
            SubId = await mongoService.NextSequenceAsync<User>(cancellationToken),
            Email = request.Email,
            IsEmailVerified = false,
            PhoneNumber = request.PhoneNumber,
            IsPhoneVerified = false,
            PasswordHash = request.Password.ToSha256(),
            CreatedAt = DateTimeExtensions.Now,
            ModifiedAt = DateTimeExtensions.Now,
        };
        await mongoService.Collection<User>().InsertOneAsync(user, cancellationToken: cancellationToken);
        return Results.Ok(new ApiResponse<object>
        {
            Data = user
        });
    }
}