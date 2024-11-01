"use client";

import Link from "next/link";
import { useParamsStore } from "@/hooks/useParamsStore";
import { AiOutlineCar } from "react-icons/ai";

export default function Logo() {
  const reset = useParamsStore((state) => state.reset);

  return (
    <Link href="/" passHref>
      <div
        onClick={() => reset()}
        className="flex cursor-pointer items-center gap-2 text-3xl font-semibold text-cyan-400"
      >
        <AiOutlineCar size={34} />
        <div>Carsties Auction</div>
      </div>
    </Link>
  );
}
