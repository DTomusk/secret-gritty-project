import { useMutation } from "@tanstack/react-query";
import { register } from "../api/register";
import type { ApiError } from "../../../lib/api/error";
import type { RegistrationRequest, LoginResponse } from "../types/types";

export function useRegister() {
    return useMutation<
        LoginResponse,
        ApiError,
        RegistrationRequest
    >({ 
        mutationFn: register
    })

}