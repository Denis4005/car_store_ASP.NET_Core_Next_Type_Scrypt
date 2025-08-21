"use client";

import { MenuProvider } from "./context/MenuContext";

export function Providers({ children }: { children: React.ReactNode }) {
  return <MenuProvider>{children}</MenuProvider>;
}
