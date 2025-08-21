import Link from "next/link";
import { ButtonLogin } from "../blocks/ButtonLogin";

export const Header = () => {
  const items = [
    {
      key: "home",
      label: (
        <Link
          className="block py-2 px-3 rounded hover:bg-gray-700 transition-colors"
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
          className="block py-2 px-3 rounded hover:bg-gray-700 transition-colors"
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
          className="block py-2 px-3 rounded hover:bg-gray-700 transition-colors"
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
          className="block py-2 px-3 rounded hover:bg-gray-700 transition-colors"
          href="#footer"
          scroll={true}
        >
          Контакты
        </Link>
      ),
    },
  ];
  return (
    <header className="bg-gray-900 text-white">
      <nav className="flex items-center justify-between p-4">
        <ul className="flex space-x-6 w-full">
          <div className="text-3xl font-bold text-white m-auto">
            <Link href={"/"}>
              <span className="text-red-500">Best</span>CarShop
            </Link>
          </div>
          <div className="hidden md:flex space-x-8 items-center m-auto">
            {items.map((item) => (
              <li
                key={item.key}
                className="hover:text-red-300 transition-colors"
              >
                {item.label}
              </li>
            ))}
            <ButtonLogin />
          </div>
          <button className="md:hidden text-white">
            <svg
              className="w-8 h-8"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M4 6h16M4 12h16M4 18h16"
              ></path>
            </svg>
          </button>
        </ul>
      </nav>
    </header>
  );
};
