"use client";

import { useMenu } from "../context/MenuContext";

export const ButtonLogin = () => {
  const { toggleFunc, isReg } = useMenu();
  return (
    <button
      className="ml-4 px-6 py-2 bg-red-600 hover:bg-red-700 rounded-full text-white transition duration-300 transform hover:scale-105"
      onClick={toggleFunc}
    >
      {isReg ? "Выйти" : "Войти"}
    </button>
  );
};
