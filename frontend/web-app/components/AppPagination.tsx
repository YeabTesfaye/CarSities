"use client";
import { Pagination } from "flowbite-react";

type Props = {
  currentPage: number;
  pageCount: number;
  onPageChange: (page: number) => void;
};
export default function AppPagination({
  currentPage,
  pageCount,
  onPageChange,
}: Props) {
  return (
    <Pagination
      currentPage={currentPage}
      totalPages={pageCount}
      onPageChange={(page: number) => onPageChange(page)}
      showIcons={true}
      layout="pagination"
      className=" text-blue-500 mb-5"
    >
      AppPagination
    </Pagination>
  );
}
