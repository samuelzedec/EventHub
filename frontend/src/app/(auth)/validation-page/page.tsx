"use client";
import axios from "@/services/Axios";
import { useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";
import Loading from "../../../components/loading";
import SeeResult from "./components/seeResult";

interface Props {
	email?: string;
	status: number;
	title: string;
	message: string;
};

export default function ValidationPage() {
	const searchParams = useSearchParams();
	const [isLoading, setIsLoading] = useState<boolean>(true);
	const [apiData, setApiData] = useState<Props>(Object.assign({}));
	const email = searchParams.get("email");
	const code = searchParams.get("code");

	const fetchData = async () => {
		try {
			const response = await axios.get("/account/validation-email", {
				params: { email, code },
			});

			setApiData({
				status: response.status,
				title: "Sucesso",
				message: response.data?.data
			});

		} catch (error: any) {
			const errorMessage = error.response?.data?.errors[0];
      setApiData({
        status: error.response?.status,
        title: "Erro",
        message: errorMessage
      });
		} finally {
			setIsLoading(!isLoading);
		}
	}
	
	useEffect(() => {
		fetchData();
	},[email, code]);

	if (isLoading) return <Loading />;

	return <SeeResult
		status={apiData.status}
		message={apiData.message}
		title={apiData.status == 200 ? "Sucesso" : "Error"}
		email={email}
	/>
}