import { Button, Stack, Typography } from "@mui/material";
import type { UpcomingEventResponse } from "../types";
import { useNavigate } from "react-router-dom";

type UnscheduledBookClubSectionProps = {
    nextBookClub: UpcomingEventResponse
};

export default function UnscheduledBookClubSection({ nextBookClub }: UnscheduledBookClubSectionProps) {
    // We need to check if there are polls for this event or not
    // Have polls been created?
    // If no polls, show other users message, show host message with button
    const navigate = useNavigate();
    return (
        <Stack spacing={2} sx={{ mt: 2, justifyContent: 'center', alignItems: 'center' }}>
            {nextBookClub.currentUserIsHost ? (
                <Typography variant="h5">
                    You're hosting the next book club.
                </Typography>
            ) : (
                <Typography variant="h5">
                    {nextBookClub.hostUserName} is hosting the next book club.
                </Typography>
            )}
            <Button
                variant="contained"
                onClick={() => navigate(`/events/${nextBookClub.id}`)}
            >
                View Details
            </Button>
        </Stack>
    );
}