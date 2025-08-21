namespace Backend.Core.Models;

public class Car
{
    public const int MAX_TITLE_LENGTH = 250;
    private Car(Guid id, string brand, string model, int horsepower, string color, int price, Guid userid)
    {
        Id = id;
        Brand = brand;
        Model = model;
        HorsePower = horsepower;
        Color = color;
        Price = price;
        UserId = userid;
    }
    public Guid Id { get; }
    public string Brand { get; } = string.Empty;
    public string Model { get; } = string.Empty;
    public int HorsePower { get; }
    public string Color { get; } = string.Empty;
    public int Price { get; }
    public Guid UserId { get; }

    
    private static string ValidateCar(string brand, string model, int horsepower, string color, int price)
    {
        if (string.IsNullOrEmpty(brand) || brand.Length > MAX_TITLE_LENGTH)
        {
            return "Брэнд не может быть пустым";
        }
        if (string.IsNullOrEmpty(model) || model.Length > MAX_TITLE_LENGTH)
        {
            return "Модель не может быть пустой";
        }
        if (string.IsNullOrEmpty(color) || color.Length > MAX_TITLE_LENGTH)
        {
            return "Цвет не может быть пустым";
        }
        if (horsepower <= 0)
        {
            return "Не правильно указаны лошадиные силы";
        }
        if (price < 0)
        {
            return "Не правильно указана цена";
        }

        return string.Empty;
    }

    public static (Car ?Car, string Error) Create(Guid id, string brand, string model, int horsepower, string color, int price, Guid userid)
    {
        var error = ValidateCar(brand, model, horsepower, color, price);
        if (!string.IsNullOrEmpty(error))
        {
            return (null, error);
        }
        var car = new Car(id, brand, model, horsepower, color, price, userid);

        return (car, error);
    }

    public static (bool Success, string Error) Update(Guid id, string brand, string model, int horsepower, string color, int price)
    {
        if (id == Guid.Empty)
        {
            return (false, "Не существующий ID");
        }
        var error = ValidateCar(brand, model, horsepower, color, price);
        
        if (!string.IsNullOrEmpty(error))
        {
            return (false, error);
        }
        return (true, error);
    }

    public static (bool Success, string Error) Delete(Guid id)
    {
        if (id == Guid.Empty)
            return (false, "Не существующий ID");

        return (true, string.Empty);
    }
}
