"use client";

import { useParamsStore } from "@/hooks/useParamsStore";
import { AiOutlineCar } from "react-icons/ai";

export default function Logo() {
    const reset = useParamsStore(state => state.reset);
  return (
    <div onClick={reset} className="flex cursor-pointer items-center gap-2 text-3xl font-semibold text-cyan-4 00">
      <AiOutlineCar size={34} />
      <div>Carsties Auction</div>
    </div>
  );
}
