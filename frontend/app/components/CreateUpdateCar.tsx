"use client";

import { Modal } from "antd";
import { CarRequest } from "../services/cars";
import { useEffect, useState } from "react";

interface Props {
  mode: Mode;
  values: Car;
  isModalIs: boolean;
  handleCancel: () => void;
  handlCreate: (request: CarRequest) => void;
  handleUpdate: (id: string, request: CarRequest) => void;
  setHorsepower: (value: number) => void;
  setPrice: (value: number) => void;
  inputHorsepower: string;
  setInputHorsepower: (value: string) => void;
  inputPrice: string;
  setInputPrice: (value: string) => void;
}
export enum Mode {
  Create,
  Edit,
}
export const CreateUpdateCar = ({
  mode,
  values,
  isModalIs,
  handleCancel,
  handlCreate,
  handleUpdate,
  setHorsepower,
  setPrice,
  inputHorsepower,
  setInputHorsepower,
  inputPrice,
  setInputPrice,
}: Props) => {
  const [brand, setBrand] = useState<string>("");
  const [model, setModel] = useState<string>("");
  const [color, setColor] = useState<string>("");

  const handleChangeHorsepower = (e: React.ChangeEvent<HTMLInputElement>) => {
    setInputHorsepower(e.target.value);
    if (!isNaN(e.target.valueAsNumber)) {
      setHorsepower(e.target.valueAsNumber);
    }
  };

  const handleChangePrice = (e: React.ChangeEvent<HTMLInputElement>) => {
    setInputPrice(e.target.value);
    if (!isNaN(e.target.valueAsNumber)) {
      setPrice(e.target.valueAsNumber);
    }
  };

  useEffect(() => {
    setBrand(values.brand);
    setModel(values.model);
    setHorsepower(values.horsepower);
    setColor(values.color);
    setPrice(values.price);
  }, [values]);
  const handleOk = async () => {
    const carRequest = {
      brand,
      model,
      horsepower: Number(inputHorsepower),
      color,
      price: Number(inputPrice),
    };
    mode == Mode.Create
      ? handlCreate(carRequest)
      : handleUpdate(values.id, carRequest);
  };
  return (
    <Modal
      title={mode === Mode.Create ? "Добавить авто" : "Редактировать авто"}
      open={isModalIs}
      onCancel={handleCancel}
      footer={[
        <button
          key="cancel"
          onClick={handleCancel}
          className="px-6 py-2 border border-gray-300 rounded-lg text-gray-700 hover:bg-gray-50 transition-colors duration-200 focus:outline-none focus:ring-2 focus:ring-gray-400"
        >
          Отмена
        </button>,
        <button
          key="submit"
          onClick={handleOk}
          className="ml-3 px-6 py-2 bg-red-600 rounded-lg text-white hover:bg-red-700 transition-colors duration-200 focus:outline-none focus:ring-2 focus:ring-red-500 shadow-md hover:shadow-lg"
        >
          {mode === Mode.Create ? "Добавить" : "Сохранить"}
        </button>,
      ]}
    >
      <div className="space-y-4 mt-3">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Марка
          </label>
          <input
            type="text"
            value={brand}
            onChange={(e) => setBrand(e.target.value)}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Введите марку"
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Модель
          </label>
          <input
            type="text"
            value={model}
            onChange={(e) => setModel(e.target.value)}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Введите модель"
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Мощность (л.с.)
          </label>
          <input
            type="number"
            value={inputHorsepower}
            onChange={handleChangeHorsepower}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Введите мощность"
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Цвет
          </label>
          <input
            type="text"
            value={color}
            onChange={(e) => setColor(e.target.value)}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Введите цвет"
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Цена
          </label>
          <input
            type="number"
            value={inputPrice}
            onChange={handleChangePrice}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Введите цену"
          />
        </div>
      </div>
    </Modal>
  );
};
