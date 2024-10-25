import Image from "next/image";
import Link from "next/link";
import CountdownTimer from "./CountdownTimer";

type Props = {
  auction: any;
};
export default function AuctionCard({ auction }: Props) {
  return (
    <Link href={"#"}>
      <div className=" relative w-full bg-gray-200  aspect-video rounded-lg overflow-hidden">
        <Image
          src={auction.imageUrl}
          alt={`Image of ${auction.model}`}
          fill
          priority 
          sizes='(max-width: 768px) 100vw, (max-width: 1200px) 25vw'
          className=" object-cover"
        />
      </div>
      <div className=" flex justify-between items-center mt-4">
        <h3 className=" text-gray-700">{auction.make} {auction.model}</h3>
        <p className="font-semibold text-sm"> {auction.year}</p>
      </div>
      <CountdownTimer auctionEnd={auction.auctionEnd} />
    </Link>
  );
}
