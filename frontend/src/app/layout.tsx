import "@/globals.css";
import AlertShow from "@/components/alertShow";

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="pt-br" className="dark">
      <body>
        <AlertShow/>
        {children}
      </body>
    </html>
  );
}