"use server";
import { Auction, PagedResult } from "../types";

export async function getData(
  pageNumer: number,
  pageSize : number
): Promise<PagedResult<Auction>> {
  const res = await fetch(
    `http://localhost:6001/search?pageNumber=${pageNumer}&pageSize=${pageSize}`
  );

  if (!res.ok) throw new Error("Failed to fetch data");
  return res.json();
}
