"use client";

import { Card } from "antd";
import { CardTitle } from "./Cardtitle";
import { decodeJwt } from "jose";
import { useEffect, useState } from "react";
import { useMenu } from "../context/MenuContext";

interface Props {
  cars: Car[];
  handleDelete: (id: string) => void;
  handleOpen: (car: Car) => void;
}
export const Cars = ({ cars, handleDelete, handleOpen }: Props) => {
  const { isReg, isOpen } = useMenu();
  const [role, setRole] = useState<string>("");
  useEffect(() => {
    if (typeof window !== "undefined") {
      try {
        const token = localStorage.getItem("token");

        if (token) {
          const decoded = decodeJwt(token);

          if (decoded && typeof decoded === "object" && "role" in decoded) {
            const roleDecode = (decoded as { role: string }).role;
            setRole(roleDecode);
          } else {
            setRole("");
          }
        } else {
          setRole("");
        }
      } catch (error) {
        console.error("Error decoding token:", error);
        setRole("");
      }
    }
  }, [isReg, isOpen]);
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6 p-6">
      {cars.map((car: Car) => (
        <Card
          key={car.id}
          className="hover:shadow-lg transition-shadow duration-300 border border-gray-100 rounded-xl overflow-hidden"
          title={<CardTitle brand={car.brand} model={car.model} />}
        >
          <div className="p-4 space-y-3">
            <p className="text-gray-600">
              <span className="font-medium text-gray-800">Мощность:</span>{" "}
              {car.horsepower} л.с.
            </p>
            <p className="text-gray-600">
              <span className="font-medium text-gray-800">Цвет:</span>
              <span
                className="inline-block w-4 h-4 rounded-full ml-2"
                style={{ backgroundColor: car.color.toLowerCase() }}
              />
              {car.color}
            </p>
            <p className="text-gray-600">
              <span className="font-medium text-gray-800">Цена:</span>
              <span className="text-green-600 font-medium ml-1">
                {new Intl.NumberFormat("ru-RU").format(car.price)} ₽
              </span>
            </p>

            {role === "Admin" ? (
              <div className="flex space-x-3 mt-4">
                <button
                  onClick={() => handleOpen(car)}
                  className="flex-1 py-2 px-4 bg-blue-50 text-blue-600 hover:bg-blue-100 border border-blue-200 rounded-lg transition-all duration-200 hover:shadow-md focus:outline-none focus:ring-2 focus:ring-blue-300 focus:ring-opacity-50 font-medium"
                >
                  Редактировать
                </button>
                <button
                  onClick={() => handleDelete(car.id)}
                  className="flex-1 py-2 px-4 bg-red-50 text-red-600 hover:bg-red-100 border border-red-200 rounded-lg transition-all duration-200 hover:shadow-md focus:outline-none focus:ring-2 focus:ring-red-300 focus:ring-opacity-50 font-medium"
                >
                  Удалить
                </button>
              </div>
            ) : (
              ""
            )}
          </div>
        </Card>
      ))}
    </div>
  );
};
