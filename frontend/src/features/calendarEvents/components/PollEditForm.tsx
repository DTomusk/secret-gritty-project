import { useState } from "react";
import dayjs from "dayjs";
import PollForm, { type CreatePollFormValues } from "./PollForm";
import { POLL_TYPES, type EventPollResponse } from "../types";

type PollEditFormProps = {
    poll: EventPollResponse;
};

// TODO: get event poll response to return the correct data structures rather than parsing on the frontend
function convertPollToFormValues(poll: EventPollResponse): CreatePollFormValues {
    const closesAt = dayjs(poll.closesAt);

    const options = poll.options.map((option) => {
        if (poll.type === POLL_TYPES.DATE) {
            return {
                date: dayjs(option.optionText),
            };
        }
        if (poll.type === POLL_TYPES.BOOK) {
            // For now, optionText would need to be parsed or we need additional API data
            // This is a placeholder - you may need to adjust based on actual API structure
            return {
                title: option.optionText,
                author: "",
            };
        }
        return {
            location: option.optionText,
        };
    });

    return {
        closesAt,
        options: options as CreatePollFormValues["options"],
    };
}

export default function PollEditForm({ poll }: PollEditFormProps) {
    const [submitError, setSubmitError] = useState<string | null>(null);
    const [isSubmitting, setIsSubmitting] = useState(false);

    async function onFormSubmit(values: CreatePollFormValues) {
        setSubmitError(null);
        setIsSubmitting(true);

        // TODO: Implement edit poll mutation and submit logic
        setTimeout(() => {
            setIsSubmitting(false);
        }, 0);
    }

    const initialValues = convertPollToFormValues(poll);

    return (
        <PollForm
            pollType={poll.type}
            initialValues={initialValues}
            onSubmit={onFormSubmit}
            isSubmitting={isSubmitting}
            submitError={submitError}
            submitButtonLabel="Save changes"
        />
    );
}