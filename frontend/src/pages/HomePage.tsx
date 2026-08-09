import { Stack, Typography } from "@mui/material";
import { getUsername } from "../lib/auth/token";

export default function HomePage() {
    const username = getUsername();
    return (
        <Stack>
            <Typography variant="h1">
                {username ? `Hi, ${username}!` : "Welcome, guest"}
            </Typography>
        </Stack>
    );
}