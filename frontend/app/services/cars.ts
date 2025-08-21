import { decodeJwt } from "jose";

export interface CarRequest {
  brand: string;
  model: string;
  horsepower: number;
  color: string;
  price: number;
}

export const getAllCars = async () => {
  const token = localStorage.getItem("token");

  const response = await fetch("/api/Cars", {
    headers: {
      Authorization: `${token}`,
      "Content-Type": "application/json",
    },
  });
  return response.json();
};

export const createCar = async (carRequest: CarRequest) => {
  const token = localStorage.getItem("token");

  if (!token) return null;

  const decoded = decodeJwt(token);

  const userId = decoded.userId;
  if (!userId) {
    throw new Error("not found in token");
  }

  const requestWithUserId = {
    ...carRequest,
    userId,
  };

  const response = await fetch("/api/Cars", {
    method: "POST",
    headers: {
      Authorization: `${token}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(requestWithUserId),
  });
  return response.json();
};

export const updateCar = async (id: string, carRequest: CarRequest) => {
  const token = localStorage.getItem("token");
  const response = await fetch(`/api/Cars/${id}`, {
    method: "PUT",
    headers: {
      Authorization: `${token}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(carRequest),
  });
  return response.json();
};

export const deleteCar = async (id: string) => {
  const token = localStorage.getItem("token");
  const response = await fetch(`/api/Cars/${id}`, {
    method: "DELETE",
    headers: {
      Authorization: `${token}`,
      "Content-Type": "application/json",
    },
  });
  return response.json();
};
