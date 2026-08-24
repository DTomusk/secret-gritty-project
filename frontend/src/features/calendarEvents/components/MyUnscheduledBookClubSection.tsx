import { Card, CardHeader, Stack, Typography } from "@mui/material";
import { BOOK_CLUB_POLL_TYPES, POLL_TYPE_NAMES, type EventPollResponse, type UpcomingEventResponse } from "../types";
import { useEventPolls } from "../hooks";
import PollEditForm from "./PollEditForm";
import PollCreationForm from "./PollCreationForm";

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
        <Stack spacing={2} sx={{ textAlign: "left", width: "100%", padding: 1 }}>
            <Typography variant="h5">You're hosting the next book club!</Typography>
            <Typography>Hey bozo, you haven't scheduled your book club yet!</Typography>
            {pollsByType && BOOK_CLUB_POLL_TYPES.map((pollType) => (
                <Card key={pollType} sx={{ padding: 2, marginBottom: 2, width: "100%" }}>
                    <CardHeader
                        title={<Typography variant="h5">{POLL_TYPE_NAMES[pollType]} poll</Typography>}
                    />
                    {pollsByType[pollType] ? (
                        <PollEditForm poll={pollsByType[pollType]} />
                    ) : (
                        <PollCreationForm eventId={nextBookClub.id} pollType={pollType} />
                    )}
                </Card>
            ))}
        </Stack>
    );
}