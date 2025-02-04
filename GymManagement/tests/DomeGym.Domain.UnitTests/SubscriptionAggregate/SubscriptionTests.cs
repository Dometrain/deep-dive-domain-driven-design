using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.SubscriptionAggregate;
using GymManagement.Domain.UnitTests.TestConstants;
using GymManagement.Domain.UnitTests.TestUtils.Gyms;
using GymManagement.Domain.UnitTests.TestUtils.Subscriptions;

using FluentAssertions;

namespace GymManagement.Domain.UnitTests.SubscriptionAggregate;

public class SubscriptionTests
{
    [Fact]
    public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        var subscription = SubscriptionFactory.CreateSubscription(subscriptionType: SubscriptionType.Pro);

        var gyms = Enumerable.Range(0, subscription.GetMaxGyms() + 1)
            .Select(_ => GymFactory.CreateGym(id: Guid.NewGuid()))
            .ToList();

        // Act
        var addGymResults = gyms.ConvertAll(subscription.AddGym);

        // Assert
        var allButLastAddGymResults = addGymResults.Take(..^1);
        allButLastAddGymResults.Should().AllSatisfy(result => result.IsError.Should().BeFalse());

        var lastAddGymResult = addGymResults.Last();
        lastAddGymResult.IsError.Should().BeTrue();
        lastAddGymResult.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows);
    }
}