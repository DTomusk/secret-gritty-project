import { POLL_TYPE_NAMES } from "../types";
import PollCreationForm from "./PollCreationForm";

type PollTypeSectionProps = {
    eventId: string;
    pollId?: string;
    pollType: number;
};

export default function PollTypeSection({ eventId, pollId, pollType }: PollTypeSectionProps) {
    return (
        <div>
            <h3>{POLL_TYPE_NAMES[pollType]} poll</h3>
            {pollId ? (
                <div>
                    <p>Poll ID: {pollId}</p>
                    <p>Poll Type: </p>
                </div>
            ) : (
                <PollCreationForm eventId={eventId} pollType={pollType} />
            )}
        </div>
    );
}