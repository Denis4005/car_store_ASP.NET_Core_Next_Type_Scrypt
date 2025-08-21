"use client";

import { Skeleton } from "antd";
import { useEffect, useState } from "react";
import {
  CarRequest,
  createCar,
  deleteCar,
  getAllCars,
  updateCar,
} from "../services/cars";
import { AuthModal, Cars, CreateUpdateCar, Mode } from "../components";
import { useMenu } from "../context/MenuContext";
import { decodeJwt } from "jose";
export default function CarsPage() {
  const { toggleMenu } = useMenu();
  const [inputHorsepower, setInputHorsepower] = useState<string>("");
  const [inputPrice, setInputPrice] = useState<string>("");
  const [horsepower, setHorsepower] = useState<number>(0);
  const [price, setPrice] = useState<number>(0);

  const defaultValues = {
    brand: "",
    model: "",
    horsepower: Number(inputHorsepower),
    color: "",
    price: Number(inputPrice),
  } as Car;
  const [values, setValues] = useState<Car>(defaultValues);

  const [cars, setCars] = useState<Car[]>([]);
  const [loading, setLoading] = useState(true);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [mode, setMode] = useState(Mode.Create);

  useEffect(() => {
    const getCars = async () => {
      const cars = await getAllCars();
      setLoading(false);
      setCars(cars);
    };
    getCars();
  }, []);

  const handleCreateCar = async (request: CarRequest) => {
    await createCar(request);
    closeModal();
    const cars = await getAllCars();
    setCars(cars);
  };
  const handleUpdateCar = async (id: string, request: CarRequest) => {
    await updateCar(id, request);
    closeModal();
    const cars = await getAllCars();
    setCars(cars);
  };

  const handleDeleteCar = async (id: string) => {
    await deleteCar(id);
    closeModal();
    const cars = await getAllCars();
    setCars(cars);
  };

  const openModal = () => {
    setMode(Mode.Create);
    setIsModalOpen(true);
  };
  const closeModal = () => {
    setInputHorsepower("");
    setInputPrice("");
    setValues(defaultValues);
    setIsModalOpen(false);
  };
  const openEditModal = (car: Car) => {
    setMode(Mode.Edit);
    setInputHorsepower(String(car.horsepower));
    setInputPrice(String(car.price));
    setValues(car);
    setIsModalOpen(true);
  };
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
    <div>
      {role ? (
        <button
          className="my-2 ml-7 px-6 py-2 bg-red-600 hover:bg-red-700 rounded-full text-white transition duration-300 transform hover:scale-105"
          onClick={() => openModal()}
        >
          Добавить Автомобиль
        </button>
      ) : (
        ""
      )}
      <CreateUpdateCar
        mode={mode}
        values={values}
        isModalIs={isModalOpen}
        handlCreate={handleCreateCar}
        handleUpdate={handleUpdateCar}
        handleCancel={closeModal}
        inputPrice={inputPrice}
        setInputPrice={setInputPrice}
        inputHorsepower={inputHorsepower}
        setInputHorsepower={setInputHorsepower}
        setPrice={setPrice}
        setHorsepower={setHorsepower}
      />
      {loading ? (
        <Skeleton>Loading ...</Skeleton>
      ) : (
        <Cars
          cars={cars}
          handleDelete={handleDeleteCar}
          handleOpen={openEditModal}
        />
      )}
      {isOpen ? <AuthModal onClose={toggleMenu} /> : ""}
    </div>
  );
}
