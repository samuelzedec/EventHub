import { AlertProps, AlertStore } from "@/interfaces/alertShow";
import { create } from "zustand";

// Create é usado para criar a store do Zustand, que gerencia o estado global
export default create<AlertStore>((set) => ({ // set: é a função que o Zustand usa para atualizar os estados 
	alert: null,
	showAlert: (props: AlertProps) => {
		set({ alert: props })
		setTimeout(() => set({ alert: null }), 3000);
	}
	/* ===========================================================
	 * Todas as função usam o set para poder manipular os estados 
	 * =========================================================== */
}));