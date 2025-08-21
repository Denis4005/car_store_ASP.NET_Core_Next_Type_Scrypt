namespace backend.api.Contracts;

public record CarsRequest(
    string Brand,
    string Model,
    int Horsepower,
    string Color,
    int Price,
    Guid UserId);