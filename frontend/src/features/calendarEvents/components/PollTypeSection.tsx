import { Card, CardHeader, Typography } from "@mui/material";
import { POLL_TYPE_NAMES, type EventPollResponse } from "../types";
import PollCreationForm from "./PollCreationForm";
import PollEditForm from "./PollEditForm";

type PollTypeSectionProps = {
    eventId: string;
    pollType: number;
    poll?: EventPollResponse;
};

export default function PollTypeSection({ eventId, pollType, poll }: PollTypeSectionProps) {
    return (
        <Card sx={{ padding: 2, marginBottom: 2, width: "100%" }}>
            <CardHeader
                title={<Typography variant="h5">{POLL_TYPE_NAMES[pollType]} poll</Typography>}
            />
            {poll ? (
                <PollEditForm poll={poll} />
            ) : (
                <PollCreationForm eventId={eventId} pollType={pollType} />
            )}
        </Card>
    );
}