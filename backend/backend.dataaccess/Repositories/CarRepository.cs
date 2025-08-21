using backend.dataaccess;
using Backend.Core.Abstraction;
using Backend.Core.Models;
using Backend.Dataaccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Dataaccess.Repositories;

public class CarsRepository : ICarsRepository
{
    private readonly CarStoreDbContext _context;
    public CarsRepository(CarStoreDbContext context)
    {
        _context = context;
    }
    public async Task<List<Car>> Get()
    {
        var cars = new List<Car>();
        var carEntities = await _context.Car.AsNoTracking().ToListAsync();
        foreach (var b in carEntities)
        {
            var (car, error) = Car.Create(b.Id, b.Brand, b.Model, b.HorsePower, b.Color, b.Price, b.UserId);
            if (car != null)
            {
                cars.Add(car);
            }
        }
        return cars;
    }
    public async Task<Guid> Create(Car car)
    {
        var carEntity = new CarEntity
        {
            Id = car.Id,
            Brand = car.Brand,
            Model = car.Model,
            HorsePower = car.HorsePower,
            Color = car.Color,
            Price = car.Price,
            UserId = car.UserId
        };
        await _context.Car.AddAsync(carEntity);
        await _context.SaveChangesAsync();
        return carEntity.Id;
    }

    public async Task<Guid> Update(Guid id, string brand, string model, int horsepower, string color, int price)
    {
        await _context.Car.Where(b => b.Id == id)
        .ExecuteUpdateAsync(s => s
        .SetProperty(b => b.Brand, b => brand)
        .SetProperty(b => b.Model, b => model)
        .SetProperty(b => b.HorsePower, b => horsepower)
        .SetProperty(b => b.Color, b => color)
        .SetProperty(b => b.Price, b => price));

        return id;
    }
    public async Task<Guid> Delete(Guid id)
    {
        await _context.Car.Where(b => b.Id == id)
        .ExecuteDeleteAsync();

        return id;
    }
}