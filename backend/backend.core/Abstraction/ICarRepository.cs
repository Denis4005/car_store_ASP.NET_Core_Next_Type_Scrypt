using Backend.Core.Models;

namespace Backend.Core.Abstraction;

public interface ICarsRepository
{
    Task<Guid> Create(Car car);
    Task<Guid> Delete(Guid id);
    Task<List<Car>> Get();
    Task<Guid> Update(Guid id, string brand, string model, int horsepower, string color, int price);

}
