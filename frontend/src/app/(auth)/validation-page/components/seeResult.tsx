"use client";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardTitle } from "@/components/ui/card";
import axios from "@/services/Axios";
import { useRouter } from "next/navigation";

interface Props {
	status: number;
	title: string;
	message: string;
	email: string | null;
};

export default function SeeResult({ status, title, message, email }: Props) {
	const router = useRouter(); 

	const directionStatus = () => {
		switch (status) {
			case 200:
				statusOk();
				break;
			case 410:
				statusGone();
				break;
			default:
				otherStatuses();
				break;
		}
	}

	const statusOk = () => {
		router.replace("/login");
	}

	const statusGone = () => {
		axios.get("/account/resend-code", {
			params: {
				email: email
			}
		});
	}

	const otherStatuses = () => {
		router.replace("/");
	};

	return (
		<>
			<div className="min-h-screen w-full flex items-center justify-center px-2">
				<Card className="text-center max-w-xs w-full p-3">
					<CardContent>
						<CardTitle className={`text-xl ${status != 200 ? "text-red-600" : "text-green-400"}`}>
							{title}
						</CardTitle>
						<CardDescription className="border-t-2 p-3">
							{message}
						</CardDescription>
						<CardDescription>
							<Button onClick={directionStatus}>
								{
									status == 200 ? "Fazer Login"
										: status == 410 ? "Reenviar Código"
											: "Voltar ao início"
								}
							</Button>
						</CardDescription>
					</CardContent>
				</Card>
			</div>
		</>
	)
}