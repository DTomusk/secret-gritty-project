import { Alert, Button, Stack, Typography } from "@mui/material";
import Spinner from "../components/Spinner";
import { NoBookClubScheduledSection } from "../features/auth/components/sections/NoBookClubScheduledSection";
import { getUsername } from "../lib/auth/token";
import { useUpcomingBookClub } from "../features/calendarEvents/hooks";
import { useNavigate } from "react-router-dom";
import { UpcomingBookClubSection } from "../features/calendarEvents/components/UpcomingBookClubSection";
import UnscheduledBookClubSection from "../features/calendarEvents/components/UnscheduledBookClubSection";
import MyUnscheduledBookClubSection from "../features/calendarEvents/components/MyUnscheduledBookClubSection";

export default function HomePage() {
    const username = getUsername();
    const { data: nextBookClub, isLoading, error } = useUpcomingBookClub();
    const navigate = useNavigate();

    return (
        <Stack spacing={2} sx={{ 
            mt: 2, 
            justifyContent: 'center', 
            alignItems: 'center' ,
            width: {xs: "100%", sm: "80%", md: "60%", lg: "50%"},
        }}>
            <Typography variant="h1">
                {username ? `Hi, ${username}!` : "Welcome, guest"}
            </Typography>
            {isLoading && <Spinner />}
            {error && <Alert severity="error">Failed to load upcoming event</Alert>}
            {nextBookClub && nextBookClub.date && !error && (
                <UpcomingBookClubSection bookClub={nextBookClub} />
            )}
            {nextBookClub && !nextBookClub.date && nextBookClub.currentUserIsHost && !error && (
                <MyUnscheduledBookClubSection nextBookClub={nextBookClub} />
            )}
            {nextBookClub && !nextBookClub.date && !nextBookClub.currentUserIsHost && !error && (
                <UnscheduledBookClubSection nextBookClub={nextBookClub} />
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