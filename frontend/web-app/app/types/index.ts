export type Auction = {
    id: string;
    createdAt: string;
    updatedAt: string;
    auctionEnd: string;
    seller: string;
    winner: string | null;
    make: string;
    model: string;
    year: number;
    color: string;
    mileage: number;
    imageUrl: string;
    status: "Live" | "ReserveNotMet" | "Finished";
    reservePrice: number;
    soldAmount: number;
    currentHighBid: number;
  };
  
  export type PagedResult<T> = {
    results: T[];
    pageCount: number;
    totalCount: number;
  };