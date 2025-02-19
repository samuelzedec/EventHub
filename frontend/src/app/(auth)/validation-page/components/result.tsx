import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardTitle } from "@/components/ui/card";
import axios from "@/services/Axios";

interface Props {
	status: number;
	title: string;
	message: string;
	email?: string;
};

export default function Result({ status, title, message}: Props) {
	const sendCode = () => {
		axios
	}
	return (
		<>
			<div className="min-h-screen w-full flex items-center justify-center px-2">
				<Card className="text-center max-w-xs w-full p-3">
					<CardContent>
						<CardTitle className={`text-xl ${status != 200 ? "text-red-600" : "text-green-400"}`}>
							{ title }
						</CardTitle>
						<CardDescription className="border-t-2 p-3">
							{ message }
						</CardDescription>
						<CardDescription>
							<Button>{ status != 410 ? "Fazer Login" : "Reenviar Código" }</Button>
						</CardDescription>
					</CardContent>
				</Card>
			</div>
		</>
	)
}