import { auth } from "@/auth";
import Heading from "@/components/Heading";
import React from "react";

export default async function Session() {
  const sessoin = await auth();
  console.log(sessoin);
  return (
    <div>
      <Heading title="Session dashboard" subtitle="" />
      <div className=" bg-blue-200 border-2 border-blue-500">
        <h3 className=" text-lg">Session data</h3>
        <pre>{JSON.stringify(sessoin, null, 2)}</pre>
      </div>
    </div>
  );
}
