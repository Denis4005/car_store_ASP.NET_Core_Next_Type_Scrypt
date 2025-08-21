import Link from "next/link";

export const Footer = () => {
  const items = [
    {
      key: "home",
      label: (
        <Link
          className="text-gray-400 hover:text-red-300 transition"
          href={"/"}
        >
          Главная
        </Link>
      ),
    },
    {
      key: "cars",
      label: (
        <Link
          className="text-gray-400 hover:text-red-300 transition"
          href={"/cars"}
        >
          Каталог
        </Link>
      ),
    },
    {
      key: "about us",
      label: (
        <Link
          className="text-gray-400 hover:text-red-300 transition"
          href="#footer"
          scroll={true}
        >
          О нас
        </Link>
      ),
    },
    {
      key: "contacts",
      label: (
        <Link
          className="text-gray-400 hover:text-red-300 transition"
          href="#footer"
          scroll={true}
        >
          Контакты
        </Link>
      ),
    },
  ];
  return (
    <footer id="footer" className="bg-gray-900 text-white py-12">
      <div className="container mx-auto px-6">
        <div className="grid md:grid-cols-4 gap-8">
          <div>
            <h3 className="text-2xl font-bold mb-4">
              <span className="text-red-500">Best</span>CarShop{" "}
            </h3>
            <p className="text-gray-400">
              Ваш надёжный партнёр в мире автомобилей
            </p>
          </div>
          <div>
            <h4 className="text-lg font-semibold mb-4">Меню</h4>
            <ul className="space-y-2">
              {items.map((item) => (
                <li key={item.key}>{item.label}</li>
              ))}
            </ul>
          </div>
          <div>
            <h4 className="text-lg font-semibold mb-4">Контакты</h4>
            <div className="text-gray-400 not-italic">
              Йошкар-Ола, ул. Строителей, 150
              <br />
              +7 (937) 777-77-77
              <br />
              info@bestcarshop.ru
            </div>
          </div>
        </div>
        <div className="border-t border-gray-800 mt-12 pt-8 text-center text-gray-500">
          <p>© 2025 BestCarShop. Все права защищены.</p>
        </div>
      </div>
    </footer>
  );
};
