"use client";
import Image from "next/image";
import { useEffect, useState } from "react";

type Props = {
  imageUrl: string;
  model: string;
};
export default function CardImage({ imageUrl, model }: Props) {
  useEffect(() => {
    const timeout = setTimeout(() => setIsLoading(false), 3000);
    return () => clearTimeout(timeout);
  }, [imageUrl]);

  const [isloading, setIsLoading] = useState(false);
  return (
    <Image
      src={imageUrl}
      alt={`Image of ${model}`}
      loading="lazy"
      fill
      sizes="(max-width: 768px) 100vw, (max-width: 1200px) 25vw"
      className={`object-cover group-hover:opacity-75 duration-700 ease-out
         ${
           isloading
             ? " grayscale blur-2xl scale-110"
             : " grayscale-0 blur-0 scale-100"
         }   `}
      onLoad={() => setIsLoading(false)}
      key={imageUrl}
    />
  );
}
