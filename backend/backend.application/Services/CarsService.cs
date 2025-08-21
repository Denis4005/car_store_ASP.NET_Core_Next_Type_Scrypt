using Backend.Core.Abstraction;
using Backend.Core.Models;

namespace Backend.Application.Services;

public class CarsService : ICarsService
{
    private readonly ICarsRepository _carsRepository;
    public CarsService(ICarsRepository carsRepository)
    {
        _carsRepository = carsRepository;
    }

    public async Task<List<Car>> GetAllCars()
    {
        return await _carsRepository.Get();
    }
    public async Task<Guid> CreateCar(Car car)
    {
        return await _carsRepository.Create(car);
    }
    public async Task<Guid> UpdateCar(Guid id, string brand, string model, int horsepower, string color, int price)
    {
        return await _carsRepository.Update(id, brand, model, horsepower, color, price);
    }
    public async Task<Guid> DeleteCar(Guid id)
    {
        return await _carsRepository.Delete(id);
    }
}

