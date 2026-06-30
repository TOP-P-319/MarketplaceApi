using System.Collections.Frozen;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using Shared.Products;
using Shared.Purchases;
using Shared.Requests;
using Shared.Reviews;
using Shared.Users;

namespace Migrations;

/// <summary>
/// Idempotent demo seeding so the storefront has data to display.
/// Every demo account uses the password "password".
/// </summary>
public static class Seeder
{
    public static async Task SeedAsync(AppDbContext ctx)
    {
        if (await ctx.Users.AnyAsync()) return;

        var now = DateTime.UtcNow;
        var passwordHash = new PasswordHasher<UserModel>().HashPassword(new UserModel(), "password");

        UserEntity User(string name, string phone, UserRoles role, long balance) => new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            PhoneNumber = phone,
            PasswordHash = passwordHash,
            Balance = balance,
            Role = role,
            Status = UserStatuses.Active,
            CreatedAt = now,
            UpdatedAt = now,
        };

        var anna = User("Анна Соколова", "+79000000001", UserRoles.Buyer, 100_000);
        var ivan = User("Иван Кузнецов", "+79000000002", UserRoles.Seller, 50_000);
        var dmitry = User("Дмитрий Волков", "+79000000003", UserRoles.Buyer, 80_000);
        var eco = User("ЭкоТовары", "+79000000004", UserRoles.Seller, 30_000);
        var oleg = User("Олег Спамов", "+79000000005", UserRoles.Buyer, 20_000);
        await ctx.Users.AddRangeAsync(anna, ivan, dmitry, eco, oleg);

        ProductEntity Product(UserEntity seller, string name, string description, long price, int amount,
            Dictionary<string, string> features) => new()
        {
            Id = Guid.NewGuid(),
            SellerId = seller.Id,
            Name = name,
            Description = description,
            ImageUrls = [],
            Features = features,
            Price = price,
            Amount = amount,
            Status = ProductStatuses.Published,
            CreatedAt = now,
            UpdatedAt = now,
        };

        var headphones = Product(ivan, "Беспроводные наушники AuroBeats",
            "Полноразмерные беспроводные наушники с активным шумоподавлением и временем работы до 40 часов.",
            5990, 12, new() { ["Тип"] = "Накладные, закрытые", ["Подключение"] = "Bluetooth 5.3", ["Время работы"] = "до 40 часов", ["Шумоподавление"] = "Активное" });
        var keyboard = Product(ivan, "Механическая клавиатура K2",
            "Компактная механическая клавиатура на 84 клавиши с горячей заменой переключателей и RGB-подсветкой.",
            6990, 7, new() { ["Формат"] = "75% (84 клавиши)", ["Переключатели"] = "тихие", ["Подсветка"] = "RGB", ["Подключение"] = "USB-C / Bluetooth", ["Материал"] = "Алюминий" });
        var lamp = Product(ivan, "Умная лампа GlowMini",
            "Настольная умная лампа с регулировкой яркости и цветовой температуры через приложение.",
            2490, 20, new() { ["Мощность"] = "9 Вт", ["Цвет. температура"] = "2700–6500K", ["Управление"] = "приложение", ["Срок службы"] = "25000 ч" });
        var band = Product(ivan, "Фитнес-браслет PulseBand",
            "Фитнес-браслет с пульсометром, мониторингом сна и уведомлениями со смартфона.",
            3490, 15, new() { ["Экран"] = "AMOLED", ["Защита"] = "5 ATM", ["Автономность"] = "14 дней", ["Датчики"] = "Пульс, SpO2, шагомер" });
        var speaker = Product(eco, "Портативная колонка BoomGo",
            "Влагозащищённая портативная колонка с насыщенным басом и ремешком для переноски.",
            4290, 10, new() { ["Мощность"] = "20 Вт", ["Защита"] = "IPX7", ["Автономность"] = "18 часов", ["Связь"] = "Bluetooth 5.2" });
        var chair = Product(eco, "Эргономичное кресло ErgoFlex",
            "Офисное кресло с сетчатой спинкой, поясничной поддержкой и регулируемыми подлокотниками.",
            14990, 5, new() { ["Спинка"] = "Сетка, дышащая", ["Подлокотники"] = "регулируемые", ["Макс. нагрузка"] = "120 кг", ["Механизм"] = "Синхромеханизм" });
        var coffee = Product(eco, "Кофеварка BrewPro",
            "Капельная кофеварка с термокувшином и таймером отложенного старта на 1.2 литра.",
            5490, 8, new() { ["Объём"] = "1.2 л", ["Мощность"] = "900 Вт", ["Кувшин"] = "Термос, сталь", ["Таймер"] = "Есть" });
        var backpack = Product(eco, "Городской рюкзак UrbanPack",
            "Влагостойкий рюкзак с отделением для ноутбука и USB-портом для зарядки.",
            3990, 18, new() { ["Объём"] = "22 л", ["Отделение"] = "Ноутбук до 15.6\"", ["Материал"] = "Полиэстер 900D", ["Особенности"] = "USB-порт, потайной карман" });
        await ctx.Products.AddRangeAsync(headphones, keyboard, lamp, band, speaker, chair, coffee, backpack);

        ReviewEntity Review(UserEntity author, ProductEntity product, int rating, string text) => new()
        {
            Id = Guid.NewGuid(),
            AuthorId = author.Id,
            ProductId = product.Id,
            Rating = rating,
            Text = text,
            CreatedAt = now,
            UpdatedAt = now,
        };
        await ctx.Reviews.AddRangeAsync(
            Review(anna, lamp, 5, "Удобно менять свет под настроение, приложение работает стабильно."),
            Review(dmitry, headphones, 5, "Звук отличный, шумодав реально работает в метро."),
            Review(oleg, headphones, 4, "Хорошие, но чехол могли бы сделать пожёстче."),
            Review(anna, keyboard, 4, "Печатать приятно, но тяжеловата для поездок."),
            Review(dmitry, coffee, 4, "Кофе варит хорошо, но работает шумновато."),
            Review(oleg, chair, 5, "Спина перестала уставать к вечеру."),
            Review(anna, backpack, 4, "Вместительный, материал приятный. USB-порт пригодился."));

        PurchaseEntity Purchase(UserEntity buyer, ProductEntity product) => new()
        {
            Id = Guid.NewGuid(),
            BuyerId = buyer.Id,
            SellerId = product.SellerId,
            ProductId = product.Id,
            ProductName = product.Name,
            PricePaid = product.Price,
            CreatedAt = now,
            UpdatedAt = now,
        };
        await ctx.Purchases.AddRangeAsync(
            Purchase(anna, lamp),
            Purchase(anna, keyboard),
            Purchase(anna, backpack),
            Purchase(dmitry, headphones),
            Purchase(dmitry, coffee),
            Purchase(oleg, headphones),
            Purchase(oleg, chair));

        // Pending "new product" request — shows up as «В обработке» in the seller's "My products".
        var pendingCreate = new ProductCreateRequestModel
        {
            SellerId = ivan.Id,
            Name = "Настольная лампа LumiDesk",
            Description = "Настольная лампа с тёплым светодиодным светом, гибкой стойкой и сенсорным управлением яркостью.",
            ImageUrls = [],
            Price = 2490,
            Amount = 9,
            Features = new Dictionary<string, string>
            {
                ["Мощность"] = "7 Вт",
                ["Цвет. температура"] = "3000K",
                ["Управление"] = "Сенсорное",
                ["Питание"] = "USB-C",
            }.ToFrozenDictionary(),
            Status = RequestStatuses.Pending,
            CreatedAt = now,
            UpdatedAt = now,
        };
        await ctx.Requests.AddAsync(new ProductCreateRequestMapper().MapToEntity(pendingCreate));

        await ctx.SaveChangesAsync();
    }
}
