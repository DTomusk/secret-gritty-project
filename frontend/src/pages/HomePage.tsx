import { Alert, Button, Stack, Typography } from "@mui/material";
import Spinner from "../components/Spinner";
import { getUsername } from "../lib/auth/token";
import { useUpcomingEvent } from "../features/calendarEvents/hooks";
import { daysUntil } from "../lib/utils/dateConverter";
import { useNavigate } from "react-router-dom";

export default function HomePage() {
    const username = getUsername();
    const { data, isLoading, error } = useUpcomingEvent();
    const navigate = useNavigate();

    return (
        <Stack spacing={2} sx={{ mt: 2, justifyContent: 'center', alignItems: 'center' }}>
            <Typography variant="h1">
                {username ? `Hi, ${username}!` : "Welcome, guest"}
            </Typography>
            {isLoading && <Spinner />}
            {error && <Alert severity="error">Failed to load upcoming event</Alert>}
            {data && (
                <Stack spacing={3}>
                    <Typography variant="h3">It's {daysUntil(data.date)} days until {data.name}</Typography>
                    <Button variant="contained" 
                            onClick={() => navigate(`/events/${data.id}`)}
                            sx={{ mt: 2, alignSelf: 'center' }}
                    >
                        View event details
                    </Button>
                </Stack>
            )}
            {!data && !isLoading && !error && (
                <Typography>No upcoming events</Typography>
            )}
            <Button variant="outlined" onClick={() => navigate("/events/create")}>
                Schedule an event
            </Button>
        </Stack>
    );
}