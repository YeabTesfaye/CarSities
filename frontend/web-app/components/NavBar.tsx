import { AiFillCar } from "react-icons/ai";

export default function NavBar() {
  return (
    <header
      className="sticky top-0 z-50 flex justify-between
     bg-white p-5 items-center text-green-800 shadow-md"
    >
      <div>
        <div className="flex items-center gap-2 text-3xl font-semibold text-cyan-4 00">
          <AiFillCar />
          Carsties Auction
        </div>
      </div>
      <div>Search</div>
      <div>Login</div> 
    </header>
  );
}
