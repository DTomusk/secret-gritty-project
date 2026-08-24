import { Stack, Typography } from "@mui/material";
import type { UpcomingEventResponse } from "../types";
import { useEventPolls } from "../hooks";

type MyUnscheduledBookClubSectionProps = {
    nextBookClub: UpcomingEventResponse
};

export default function MyUnscheduledBookClubSection({ nextBookClub }: MyUnscheduledBookClubSectionProps) {
    const { data: eventPolls } = useEventPolls(nextBookClub.id);
    
    return (
        <Stack spacing={2} sx={{ textAlign: "left", justifyContent: 'center', alignItems: 'start' }}>
            <Typography variant="h5">Next Book Club</Typography>
            <Typography>Hey bozo, you haven't scheduled the book club yet!</Typography>
            <Typography>Book Club Name: {nextBookClub.name}</Typography>
            <Typography>Host: {nextBookClub.hostUserName}</Typography>
            {eventPolls && eventPolls.length > 0 ? (
                <Stack spacing={1}>
                    <Typography variant="h6">Polls for this event:</Typography>
                    {eventPolls.map((poll) => (
                        <Typography key={poll.pollId}>{poll.pollId} - {poll.closesAt} (Type: {poll.type})</Typography>
                    ))}
                </Stack>
            ) : (
                <Typography>There are no polls for this event yet.</Typography>
            )}
        </Stack>
    );
}