import { Card, CardHeader, Stack, Typography } from "@mui/material";
import { POLL_TYPE_NAMES, type EventPollResponse } from "../types";
import PollCreationForm from "./PollCreationForm";

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
                <Stack spacing={1} sx={{ marginTop: 1 }}>
                    <Typography>Poll ID: {poll.pollId}</Typography>
                    <Typography>Poll Type: {POLL_TYPE_NAMES[pollType]}</Typography>
                    {poll.options.map((option) => (
                        <Stack key={option.optionId}>
                            <Typography>Option ID: {option.optionId}</Typography>
                            <Typography>Option Text: {option.optionText}</Typography>
                        </Stack>
                    ))}
                </Stack>
            ) : (
                <PollCreationForm eventId={eventId} pollType={pollType} />
            )}
        </Card>
    );
}