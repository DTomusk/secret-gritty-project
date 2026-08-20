import { jwtDecode } from "jwt-decode";

const JWT_TOKEN_KEY = "jwt";

interface DecodedToken {
    sub: string;
    unique_name: string;
}

function getToken(): string | null {
    return localStorage.getItem(JWT_TOKEN_KEY);
}

function setToken(token: string): void {
    localStorage.setItem(JWT_TOKEN_KEY, token);
}

function clearToken(): void {
    localStorage.removeItem(JWT_TOKEN_KEY);
}

function getUsername(): string | null {
    const token = getToken();
    if (!token) return null;

    try {
        const decoded = jwtDecode<DecodedToken>(token);
        return decoded.unique_name;
    } catch  {
        return null;
    }
}

export { getToken, setToken, clearToken, getUsername };