import { Alert, Stack, Typography } from "@mui/material";
import { getUsername } from "../lib/auth/token";
import { useUpcomingEvent } from "../features/calendarEvents/hooks";
import { daysUntil } from "../lib/utils/dateConverter";

export default function HomePage() {
    const username = getUsername();
    const { data, isLoading, error } = useUpcomingEvent();
    return (
        <Stack>
            <Typography variant="h1">
                {username ? `Hi, ${username}!` : "Welcome, guest"}
            </Typography>
            {isLoading && <Typography>Loading upcoming event...</Typography>}
            {error && <Alert severity="error">Failed to load upcoming event</Alert>}
            {data && (
                <Stack>
                    <Typography variant="h3">It's {daysUntil(data.date)} days until {data.name}</Typography>
                </Stack>
            )}
            {!data && !isLoading && !error && (
                <Typography>No upcoming events</Typography>
            )}
        </Stack>
    );
}