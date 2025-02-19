"use client";
import LoadingBar from "@/components/loadingBar";
import { Card, CardContent, CardDescription, CardTitle } from "@/components/ui/card";

export default function Loading() {
	return (
		<>
			<div className="min-h-screen w-full flex items-center justify-center px-2">	
				<Card className="text-center max-w-xs w-full h-24 p-2">
					<CardContent>
						<CardTitle className="text-xl">
							Carregando...
						</CardTitle>
						<div className="my-3">
							<LoadingBar/>
						</div>
						<CardDescription>
							Aguarde so um momento!
						</CardDescription>
					</CardContent>
				</Card>
			</div>
		</>
	);
}