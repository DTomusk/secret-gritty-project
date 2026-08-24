import { useState } from "react";
import { useCreateEventPoll } from "../hooks";
import PollForm, { type CreatePollFormValues } from "./PollForm";

type PollCreationFormProps = {
    eventId: string;
    pollType: number;
};

export default function PollCreationForm({ eventId, pollType }: PollCreationFormProps) {
    const [submitError, setSubmitError] = useState<string | null>(null);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const { mutate: createPoll } = useCreateEventPoll(eventId);

    async function onFormSubmit(values: CreatePollFormValues) {
        setSubmitError(null);
        setIsSubmitting(true);

        const apiPayload = {
            pollType: pollType,
            closesAt: values.closesAt ? values.closesAt.format() : "",
            options: values.options.map((opt: any) => ({
                date: opt.date ? opt.date.format() : undefined,
                location: opt.location,
                title: opt.title,
                author: opt.author,
            })),
        };

        try {
            await createPoll(apiPayload, {
                onError: (error) => {
                    const message = error instanceof Error ? error.message : "Failed to create poll";
                    setSubmitError(message);
                    setIsSubmitting(false);
                },
                onSuccess: () => {
                    setIsSubmitting(false);
                },
            });
        } catch (error) {
            const message = error instanceof Error ? error.message : "Failed to create poll";
            setSubmitError(message);
            setIsSubmitting(false);
        }
    }

    return (
        <PollForm
            pollType={pollType}
            onSubmit={onFormSubmit}
            isSubmitting={isSubmitting}
            submitError={submitError}
            submitButtonLabel="Create poll"
        />
    );
}