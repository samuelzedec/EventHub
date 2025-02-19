export interface AlertProps {
	variant: "default" | "destructive";
	title: string;
	description: string;
}

export interface AlertStore {
	alert: AlertProps | null;
	showAlert: (props: AlertProps) => void;
} 