import { Metadata } from "next";
import "./globals.css";
import NavBar from "@/components/NavBar";

export const metadata: Metadata = {
  title: "Carstie",
};
export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <NavBar />
        {/* // mx-auto */}
        <main className=" container mx-auto  px-5 pt-10 ">{children}</main>
      </body>
    </html>
  );
}
