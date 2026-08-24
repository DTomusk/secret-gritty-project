import { Stack, Typography } from "@mui/material";
import { BOOK_CLUB_POLL_TYPES, type EventPollResponse, type UpcomingEventResponse } from "../types";
import { useEventPolls } from "../hooks";
import PollTypeSection from "./PollTypeSection";

type MyUnscheduledBookClubSectionProps = {
    nextBookClub: UpcomingEventResponse
};

export default function MyUnscheduledBookClubSection({ nextBookClub }: MyUnscheduledBookClubSectionProps) {
    const { data: eventPolls } = useEventPolls(nextBookClub.id);

    // A book club has a mandatory set of poll types
    // For each poll type, need to check if a poll of that type exists for this event
    // These can be sections in this component

    const pollsByType = eventPolls?.reduce((acc, poll) => {
        acc[poll.type] = poll;
        return acc;
    }, {} as Record<number, EventPollResponse>);
    
    return (
        <Stack spacing={2} sx={{ textAlign: "left", justifyContent: 'center', alignItems: 'start' }}>
            <Typography variant="h5">Next Book Club</Typography>
            <Typography>Hey bozo, you haven't scheduled the book club yet!</Typography>
            <Typography>Book Club Name: {nextBookClub.name}</Typography>
            <Typography>Host: {nextBookClub.hostUserName}</Typography>
            {pollsByType && BOOK_CLUB_POLL_TYPES.map((pollType) => (
                <PollTypeSection
                    key={pollType}
                    eventId={nextBookClub.id}
                    pollId={pollsByType[pollType]?.pollId}
                    pollType={pollType}
                />
            ))}
        </Stack>
    );
}