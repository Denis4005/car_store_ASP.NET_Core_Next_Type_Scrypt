using System.Security.Claims;
using backend.api.Contracts;
using backend.core.Enums;
using backend.infrastructure;
using Backend.Api.Contracts;
using Backend.Core.Abstraction;
using Backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class CarsController : ControllerBase
{
    private readonly ICarsService _carsService;
    public CarsController(ICarsService carsService)
    {
        _carsService = carsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CarsResponse>>> GetCars()
    {
        var cars = await _carsService.GetAllCars();

        var response = cars.Select(b => new CarsResponse(b.Id, b.Brand, b.Model, b.HorsePower, b.Color, b.Price));

        return Ok(response);
    }

    [HttpPost]
    [RequirePermissions(Permission.Read)]
    public async Task<ActionResult<Guid>> CreateCar(CarsRequest request)
    {
        var (car, error) = Car.Create(
            Guid.NewGuid(),
            request.Brand,
            request.Model,
            request.Horsepower,
            request.Color,
            request.Price,
            request.UserId
        );
        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        if (car is null)
        {
            return BadRequest("Неизвестная ошибка создания автомобиля");
        }

        var carId = await _carsService.CreateCar(car);
        return Ok(carId);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<Guid>> UpdatesCar(Guid id, CarsRequest request)
    {
        var (isValid, error) = Car.Update(id, request.Brand, request.Model, request.Horsepower, request.Color, request.Price);
        if (!isValid)
        {
            return BadRequest(error);
        }
        var carId = await _carsService.UpdateCar(id, request.Brand, request.Model, request.Horsepower, request.Color, request.Price);
        return Ok(carId);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<Guid>> DeleteCar(Guid id)
    {
        var (isValid, error) = Car.Delete(id);
        if (!isValid)
        {
            return BadRequest(error);
        }
        var carId = await _carsService.DeleteCar(id);
        return Ok(carId);
    }
}
