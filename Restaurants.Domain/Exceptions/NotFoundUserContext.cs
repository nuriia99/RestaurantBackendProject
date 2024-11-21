namespace Restaurants.Domain.Exceptions;

public class NotFoundUserContext(string resourceType)
    : Exception($"{resourceType} doesn't exist")
{

}