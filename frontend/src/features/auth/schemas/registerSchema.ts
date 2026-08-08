import { z } from "zod";

export const registerSchema = z.object({
    registrationCode: z.string().length(12, "Registration code must be exactly 12 characters"),
    password: z.string().min(8, "Password must be at least 8 characters"),
});

export type RegisterSchema = z.infer<typeof registerSchema>;