import { useParamsStore } from "@/hooks/useParamsStore";
import { Button, ButtonGroup } from "flowbite-react";

const pageSizeButtons = [4, 8, 12];

function Filters() {
  const pageSize = useParamsStore((state) => state.pageSize);
  const setParams = useParamsStore((state) => state.setParams);
  return (
    <div className=" flex justify-between items-center mb-4">
      <div>
        <span className=" uppercase text-sm items-start text-gray-500 mr-2">
          <ButtonGroup>
            {pageSizeButtons.map((value, index) => (
              <Button
                key={index}
                onClick={() => setParams({pageSize: value})}
                color={`${pageSize === value ? "red" : "gray"}`}
                className=" focus:ring-1"
              >
                {value}
              </Button>
            ))}
          </ButtonGroup>
        </span>
      </div>
    </div>
  );
}

export default Filters;
