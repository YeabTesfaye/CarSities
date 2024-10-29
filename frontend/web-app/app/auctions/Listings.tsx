"use client";
import  { useEffect, useState } from "react";
import AuctionCard from "./AuctionCard";
import AppPagination from "@/components/AppPagination";
import { getData } from "../actions/auctionActions";
import { Auction, PagedResult } from "../types";
import Filter from "./Filters";
import { useParamsStore } from "@/hooks/useParamsStore";
import { useShallow } from "zustand/react/shallow";
import qs from "query-string";
import EmptyFilter from "@/components/EmptyFilter";

export default function Listings() {
  const [data, setData] = useState<PagedResult<Auction>>();
  const params = useParamsStore(
    useShallow((state) => ({
      pageNumber: state.pageNumber,
      pageSize: state.pageSize,
      searchTerm: state.searchTerm,
      orderBy: state.orderBy,
      filterBy: state.filterBy,
    }))
  );
  const setParams = useParamsStore((state) => state.setParams);

  const url = qs.stringifyUrl({ url: "", query: params });

  function setPageNumber(pageNumber: number) {
    setParams({ pageNumber });
  }
  useEffect(() => {
    getData(url).then((data) => {
      setData(data);
    });
  }, [url]);
  return (
    <>
      <Filter />
      {data?.totalCount === 0 ? (
        <EmptyFilter />
      ) : (
        <>
          <div className="grid grid-cols-4 gap-6">
            {data?.results.map((auction) => (
              <AuctionCard auction={auction} key={auction.id} />
            ))}
          </div>
          <div className="flex justify-center mt-4">
            <AppPagination
              currentPage={params.pageNumber}
              pageCount={data?.pageCount || 0}
              onPageChange={setPageNumber}
            />
          </div>
        </>
      )}
    </>
  );
}
