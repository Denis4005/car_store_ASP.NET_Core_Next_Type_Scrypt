"use client";
import { AuthModal } from "./components";
import { useMenu } from "./context/MenuContext";

export default function Home() {
  const { isOpen, toggleMenu } = useMenu();
  return (
    <>
      <div className="min-h-screen">
        <div className="relative min-h-screen bg-gray-900 overflow-hidden">
          <div className="absolute inset-0">
            <img
              src="https://images.unsplash.com/photo-1555215695-3004980ad54e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2070&q=80"
              alt="BMW 5 Series F10"
              className="w-full h-full object-cover object-center opacity-70"
            />
            <div className="absolute inset-0 bg-gradient-to-t from-gray-900 via-gray-900/70 to-transparent"></div>
          </div>
          <div className="relative z-10 container mx-auto px-6 flex flex-col items-center justify-center min-h-[70vh] text-center py-20"></div>
        </div>
      </div>
      {isOpen ? <AuthModal onClose={toggleMenu} /> : ""}
    </>
  );
}
