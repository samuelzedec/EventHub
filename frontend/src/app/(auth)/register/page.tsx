"use client";

import { useForm } from "react-hook-form";
import * as z from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Card, CardHeader, CardTitle, CardContent, CardDescription, CardFooter } from "@/components/ui/card";
import { Form, FormControl, FormField, FormItem, FormMessage } from "@/components/ui/form";
import axios from "@/services/Axios";
import useAlertStore from "@/stores/alertStore";

export default function RegisterForm() {
	const setStoreAlert = useAlertStore((state) => state.showAlert);
	const formSchema = z.object({
		username: z
			.string()
			.min(5, "Nome deve ter no minímo 5 caracteres")
			.max(255, "Nome não pode passar de 255 caracteres"),
		email: z
			.string()
			.email("E-mail no formato inválido")
			.max(255, "E-mail não pode passar de 255 caracteres"),
		slug: z.string()
	});

	const form = useForm<z.infer<typeof formSchema>>({
		resolver: zodResolver(formSchema),
		defaultValues: {
			username: "",
			email: "",
			slug: ""
		}
	});

	const createUser = async (values: z.infer<typeof formSchema>) => {
		try {
			const response = await axios.post("account/register", values);
			if (response.status == 201) {
				alertSuccess();
				form.reset();
			} 
		} catch (error : any) {
			console.log(error?.response.data.errors);
			alertError(error?.response.data.errors);
		}		
	}

	const alertSuccess = () => {
		setStoreAlert({
			variant: "default",
			title: "Sucesso",
			description: "Conta cadastrada com sucesso"
		});
	}

	const alertError = (messages: string[]) => {
		messages.forEach((message: string) => {
			setStoreAlert({
				variant: "destructive",
				title: "Erro",
				description: message
			});
		});
	}

	return (
		<>
			<div className="min-h-screen w-full flex items-center justify-center px-2">
				<Card className="w-full max-w-lg min-w-sm">
					<CardHeader className="text-center">
						<CardTitle className="text-xl">Cadastrar-se</CardTitle>
						<CardDescription>Informe os seguintes dados:</CardDescription>
					</CardHeader>
			
					<CardContent>
						<Form {...form}>
							<form
								onSubmit={form.handleSubmit(createUser)}
								className="space-y-4">
								<FormField
									control={form.control}
									name="username"
									render={({ field }) => (
										<FormItem>
											<FormControl>
												<Input
													placeholder="Nome de usuário"
													{...field}
												/>
											</FormControl>
											<FormMessage />
										</FormItem>
									)}
								/>
								<FormField
									control={form.control}
									name="email"
									render={({ field }) => (
										<FormItem>
											<FormControl>
												<Input
													placeholder="E-mail"
													{...field}
												/>
											</FormControl>
											<FormMessage />
										</FormItem>
									)}
								/>
								<FormField
									control={form.control}
									name="slug"
									render={({ field }) => (
										<FormItem>
											<FormControl>
												<Input
													placeholder="Slug"
													{...field}
												/>
											</FormControl>
										</FormItem>
									)}
								/>
								<div className="flex justify-center">
									<Button
										type="submit"
										className="max-w-40 w-full animate-pulse"
									>
										Enviar
									</Button>
								</div>
							</form>
						</Form>
					</CardContent>
					<CardFooter  className="flex justify-center">
						<CardDescription className="border-t-2 py-2">
							&copy; 2025 EventHub. Todos os direitos reservados
						</CardDescription>
					</CardFooter>
				</Card>
			</div>
		</>
	);
}