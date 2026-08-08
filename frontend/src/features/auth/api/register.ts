import { api } from "../../../lib/api/api"

import type { LoginResponse, RegistrationRequest } from "../types/types";

export const register = async (input: RegistrationRequest) => {
    return api.post<LoginResponse>("/auth/register", JSON.stringify(input));
}