"use client";
import { Card, CardHeader, CardTitle, CardContent, CardDescription, CardFooter } from "@/components/ui/card";
import axios from "@/services/Axios";
import { useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";
import Loading from "./components/loading";
import Result from "./components/result";

interface Props {
	status: number;
	title: string;
	message: string;
	email?: string;
};

export default function ValidationPage() {
	const searchParams = useSearchParams();
	const [result, setResult] = useState<boolean | null>(null);
	const [isLoading, setIsLoading] = useState<boolean>(true);
	const [apiData, setApiData] = useState<Props>(Object.assign({}));
	const email = searchParams.get("email");
	const code = searchParams.get("code");
	
	useEffect(() => {

		const fetchData = async () => {
			try {
				const response = await axios.get("/account/validation-email", {
					params: { email, code },
				});
				console.log(response);
				setApiData({
					status: response.status,
					title: "Sucesso",
					message: response.data
				});

			} catch (error: any) {
				setApiData({
					status: error.response.status,
					title: "Sucesso",
					message: error.response.data.errors.join("\n")
				});
				console.log(error.response.data);
			} finally {
				setIsLoading(!isLoading);
			}
		}
		fetchData();
	}, [email, code]);

	// return <Loading />;
	return <Result
		status={apiData.status}
		message={apiData.message}
		title={apiData.status == 200 ? "Sucesso" : "Error" } />
}

/*
Object { data: "Email verificado!", errors: [] }
data: "Email verificado!"
*/ 