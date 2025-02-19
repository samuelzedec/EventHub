"use client";
import { Alert, AlertTitle, AlertDescription } from "./ui/alert";
import { IoMdAlert } from "react-icons/io";
import type { AlertProps } from "@/interfaces/alertShow";
import useAlertStore from "@/stores/alertStore";

export default function AlertShow() {
	const store = useAlertStore((state): AlertProps | null => state.alert);
	if (!store) return null;
	
	return (
		<Alert
			variant={store.variant}
			className={`${store.variant === "destructive" ? "text-red-500" : "text-green-500"} max-w-96 w-full h-16 m-2 z-50 fixed animate-pulse`}
		>
			<span className="flex gap-2 content-center">
				<IoMdAlert className="h-4 w-4" color={store.variant === "destructive" ? "#ef4444" : "#22c55e"} />
				<AlertTitle className="font-bold">{store.title}</AlertTitle>
			</span>
			<AlertDescription>{store.description}</AlertDescription>
		</Alert>
	);
}