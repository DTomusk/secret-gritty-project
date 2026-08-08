import { useNavigate } from "react-router-dom";
import RegistrationForm from "../features/auth/components/RegistrationForm";
import { useAuth } from "../features/auth/hooks/useAuth";
import { useRegister } from "../features/auth/hooks/useRegister";
import type { RegisterSchema } from "../features/auth/schemas/registerSchema";
import { useEffect } from "react";

export default function RegistrationPage() {
    const navigate = useNavigate();
    const mutation = useRegister();
    const { logIn, isAuthenticated } = useAuth();

    useEffect(() => {
        if (isAuthenticated) {
            navigate("/auth/intro", { replace: true });
        }
    }, [isAuthenticated, navigate]);

    const onSubmit = async (formData: RegisterSchema) => {
        const response = await mutation.mutateAsync(formData);
        await logIn(response.token);
        navigate("/auth/intro", { replace: true });
    }

    return (
        <RegistrationForm onSubmit={onSubmit} />
    )
}