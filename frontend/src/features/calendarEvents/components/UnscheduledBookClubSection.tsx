import { Stack, Typography } from "@mui/material";
import type { UpcomingEventResponse } from "../types";

type UnscheduledBookClubSectionProps = {
    nextBookClub: UpcomingEventResponse
};

export default function UnscheduledBookClubSection({ nextBookClub }: UnscheduledBookClubSectionProps) {
    return (
        <Stack spacing={2} sx={{ mt: 2, justifyContent: 'center', alignItems: 'center' }}>
            {nextBookClub.currentUserIsHost ? (
                <Typography variant="h5">
                    You are hosting the next book club.
                </Typography>
            ) : (
                <Typography variant="h5">
                    {nextBookClub.hostUserName} is hosting the next book club.
                </Typography>
            )}
        </Stack>
    );
}