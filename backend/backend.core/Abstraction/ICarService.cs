using Backend.Core.Models;

namespace Backend.Core.Abstraction;

public interface ICarsService
{
    Task<Guid> CreateCar(Car car);
    Task<Guid> DeleteCar(Guid id);
    Task<List<Car>> GetAllCars();
    Task<Guid> UpdateCar(Guid id, string brand, string model, int horsepower, string color, int price);

}
