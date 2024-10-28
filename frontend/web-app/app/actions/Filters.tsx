import { Button, ButtonGroup } from "flowbite-react";

type Props = {
  pageSize: number;
  setPageSize: (size: number) => void;
};

const pageSizeButtons = [4, 8, 12];

function Filters({ pageSize, setPageSize }: Props) {
  return (
    <div className=" flex justify-between items-center mb-4">
      <div>
        <span className=" uppercase text-sm items-start text-gray-500 mr-2">
          <ButtonGroup>
            {pageSizeButtons.map((value, index) => (
              <Button
                key={index}
                onClick={() => setPageSize(value)}
                color={`${pageSize === value ? "red" : "gray"}`}
                className=" focus:ring-1"
              >{value}</Button>
            ))}
          </ButtonGroup>
        </span>
      </div>
    </div>
  );
}

export default Filters;
