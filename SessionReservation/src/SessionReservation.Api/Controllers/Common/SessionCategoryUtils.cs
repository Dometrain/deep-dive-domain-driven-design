using SessionReservation.Domain.SessionAggregate;
using ErrorOr;

namespace SessionReservation.Api.Controllers.Common;

public static class SessionCategoryUtils
{
    public static ErrorOr<List<SessionCategory>> ToDomain(List<string>? categories)
    {
        if (categories is null)
        {
            return new List<SessionCategory>();
        }

        List<SessionCategory> parsedCategories = categories
            .Select(category => SessionCategory.TryFromName(category, out var parsedCategory) ? parsedCategory : null)
            .Where(category => category is not null)
            .ToList()!;

        if (parsedCategories.Count != categories.Count)
        {
            return categories.Except(parsedCategories.ConvertAll(category => category.Name))
                .Select(invalidCategory => Error.Validation("Categories.InvalidCategory", $"Invalid category '{invalidCategory}'"))
                .ToList();
        }

        return parsedCategories;
    }
}