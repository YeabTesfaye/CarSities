"use client"
import React, { useEffect, useState } from "react";
import AuctionCard from "./AuctionCard";
import AppPagination from "@/components/AppPagination";
import { getData } from "../actions/auctionActions";
import { Auction } from "../types";
import Filter from "../actions/Filters";

export default  function Listings() {
  const [auction, setAuctions] = useState<Auction[]>([]);
  const [pageCount, setPageCount] = useState<number>(0);
  const [pageNumber, setPageNumber] = useState<number>(1);
  const [pageSize,setPageSize] = useState<number>(4);

  useEffect(() => {
    getData(pageNumber,pageSize).then((data) => {
      setAuctions(data.results);
      setPageCount(data.pageCount);
    });
  }, [pageNumber,pageSize]);
   
  return (
    <React.Fragment>
      <Filter pageSize={pageSize} setPageSize={setPageSize} />
      <div className="grid grid-cols-4 gap-6">
        {auction.map((auction) => (
          <AuctionCard auction={auction} key={auction.id} />
        ))}
      </div>
      <div className="flex justify-center mt-4">
        <AppPagination currentPage={pageNumber} pageCount={pageCount} onPageChange={setPageNumber} />
      </div>
    </React.Fragment>
  );
}
