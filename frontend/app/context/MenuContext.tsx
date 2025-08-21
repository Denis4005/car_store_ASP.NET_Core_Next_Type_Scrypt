"use client";

import {
  createContext,
  useContext,
  useState,
  ReactNode,
  useEffect,
} from "react";

type MenuContextType = {
  isOpen: boolean;
  toggleMenu: () => void;
  closeMenu: () => void;
  openMenu: () => void;
  isReg: boolean;
  toggleReg: () => void;
  regNo: () => void;
  regYes: () => void;
  toggleFunc: () => void;
};

const MenuContext = createContext<MenuContextType | undefined>(undefined);

export const MenuProvider = ({ children }: { children: ReactNode }) => {
  const [isOpen, setIsOpen] = useState(false);
  const [isReg, setIsReg] = useState(false);

  const toggleMenu = () => setIsOpen((prev) => !prev);
  const closeMenu = () => setIsOpen(false);
  const openMenu = () => setIsOpen(true);

  const toggleReg = () => setIsReg((prev) => !prev);
  const regNo = () => setIsReg(false);
  const regYes = () => setIsReg(true);

  const toggleFunc = () => {
    const token = localStorage.getItem("token");

    if (token) {
      localStorage.removeItem("token");
      setIsReg(false);
    } else {
      setIsReg(true);
    }
    toggleMenu();
  };

  useEffect(() => {
    const checkToken = async () => {
      try {
        const token = localStorage.getItem("token");
        if (!token) {
          regNo();
          return false;
        }
        const response = await fetch(`/api/health-token`, {
          method: "GET",
          headers: {
            Authorization: `${token}`,
            "Content-Type": "application/json",
          },
        });
        if (response.ok) {
          regYes();
        } else {
          regNo();
          localStorage.removeItem("token");
        }
        return response.ok;
      } catch (error) {
        console.error("Token validation failed:", error);
        regNo();
        localStorage.removeItem("token");
        return false;
      }
    };
    checkToken();
  }, []);

  return (
    <MenuContext.Provider
      value={{
        isOpen,
        toggleMenu,
        closeMenu,
        openMenu,
        toggleReg,
        regNo,
        regYes,
        isReg,
        toggleFunc,
      }}
    >
      {children}
    </MenuContext.Provider>
  );
};

export const useMenu = () => {
  const context = useContext(MenuContext);
  if (context === undefined) {
    throw new Error("useMenu должно использоваться в MenuProvider");
  }
  return context;
};
