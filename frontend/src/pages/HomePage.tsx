import { Alert, Button, Stack, Typography } from "@mui/material";
import Spinner from "../components/Spinner";
import { NoBookClubScheduledSection } from "../features/auth/components/sections/NoBookClubScheduledSection";
import { getUsername } from "../lib/auth/token";
import { useUpcomingBookClub } from "../features/calendarEvents/hooks";
import { useNavigate } from "react-router-dom";
import { UpcomingBookClubSection } from "../features/calendarEvents/components/UpcomingBookClubSection";

export default function HomePage() {
    const username = getUsername();
    const { data: nextBookClub, isLoading, error } = useUpcomingBookClub();
    const navigate = useNavigate();

    return (
        <Stack spacing={2} sx={{ mt: 2, justifyContent: 'center', alignItems: 'center' }}>
            <Typography variant="h1">
                {username ? `Hi, ${username}!` : "Welcome, guest"}
            </Typography>
            {isLoading && <Spinner />}
            {error && <Alert severity="error">Failed to load upcoming event</Alert>}
            {nextBookClub && nextBookClub.date && !error && (
                <UpcomingBookClubSection bookClub={nextBookClub} />
            )}
            {nextBookClub && !nextBookClub.date && !error && (
                <Stack spacing={2} sx={{ mt: 2, justifyContent: 'center', alignItems: 'center' }}>
                <Typography variant="h5">
                    {nextBookClub.hostUserName} is hosting the next book club.
                </Typography>
                <Typography variant="body1">
                    {nextBookClub.hostUserName} hasn't chosen a date yet, stay tuned!
                </Typography>
                </Stack>
            )}
            {!nextBookClub && !isLoading && !error && (
                <NoBookClubScheduledSection />
            )}
            {/* <Button variant="outlined" onClick={() => navigate("/events/create")}>
                Schedule an event
            </Button> */}
        </Stack>
    );
}