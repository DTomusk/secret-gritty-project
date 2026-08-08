export type RegistrationRequest = {
    registrationCode: string;
    password: string;
}

export type RegistrationResponse = {
    token: string;
}

export type LoginRequest = {
    username: string;
    password: string;
}

export type LoginResponse = {
    token: string;
}