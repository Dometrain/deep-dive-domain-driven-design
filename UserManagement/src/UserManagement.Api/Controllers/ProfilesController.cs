using MediatR;

using Microsoft.AspNetCore.Mvc;

using UserManagement.Application.Profiles.CreateAdminProfile;
using UserManagement.Application.Profiles.CreateParticipantProfile;
using UserManagement.Application.Profiles.CreateTrainerProfile;
using UserManagement.Application.Profiles.ListProfiles;
using UserManagement.Contracts.Profiles;

namespace UserManagement.Api.Controllers;

[Route("users/{userId:guid}/profiles")]
public class ProfilesController : ApiController
{
    private readonly ISender _sender;

    public ProfilesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("admin")]
    public async Task<IActionResult> CreateAdminProfile(Guid userId)
    {
        var requestUserId = Guid.Parse(HttpContext.User.Claims.First(claim => claim.Type == "id").Value);

        if (requestUserId != userId)
        {
            return Problem(detail: "You are not authorized to create an admin profile for this user", statusCode: StatusCodes.Status403Forbidden);
        }

        var command = new CreateAdminProfileCommand(userId);

        var createProfileResult = await _sender.Send(command);

        return createProfileResult.Match(
            id => Ok(new ProfileResponse(id)),
            Problem);
    }

    [HttpPost("participant")]
    public async Task<IActionResult> CreateParticipantProfile(Guid userId)
    {
        var requestUserId = Guid.Parse(HttpContext.User.Claims.First(claim => claim.Type == "id").Value);

        if (requestUserId != userId)
        {
            return Problem(detail: "You are not authorized to create an Participant profile for this user", statusCode: StatusCodes.Status403Forbidden);
        }

        var command = new CreateParticipantProfileCommand(userId);

        var createProfileResult = await _sender.Send(command);

        return createProfileResult.Match(
            id => Ok(new ProfileResponse(id)),
            Problem);
    }

    [HttpPost("Trainer")]
    public async Task<IActionResult> CreateTrainerProfile(Guid userId)
    {
        var requestUserId = Guid.Parse(HttpContext.User.Claims.First(claim => claim.Type == "id").Value);

        if (requestUserId != userId)
        {
            return Problem(detail: "You are not authorized to create an Trainer profile for this user", statusCode: StatusCodes.Status403Forbidden);
        }

        var command = new CreateTrainerProfileCommand(userId);

        var createProfileResult = await _sender.Send(command);

        return createProfileResult.Match(
            id => Ok(new ProfileResponse(id)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListProfiles(Guid userId)
    {
        var listProfilesQuery = new ListProfilesQuery(userId);

        var listProfilesResult = await _sender.Send(listProfilesQuery);

        return listProfilesResult.Match(
            profiles => Ok(new ListProfilesResponse(
                profiles.AdminId,
                profiles.ParticipantId,
                profiles.TrainerId)),
            Problem);
    }
}