namespace Backend.Api.Contracts;

public record CarsResponse(
    Guid Id,
    string Brand,
    string Model,
    int Horsepower,
    string Color,
    int Price);

