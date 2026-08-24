import { Alert, Button, Stack, Typography } from "@mui/material";
import { POLL_TYPE_NAMES } from "../types";
import FormWrapper from "../../../components/FormWrapper";
import { Controller, useForm } from "react-hook-form";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import type { Dayjs } from "dayjs";
import { useState } from "react";
import { useCreateEventPoll } from "../hooks";

type PollCreationFormProps = {
    eventId: string;
    pollType: number;
};

type CreatePollFormValues = {
    closesAt: Dayjs | null;
}

export default function PollCreationForm({ eventId, pollType }: PollCreationFormProps) {
    const [submitError, setSubmitError] = useState<string | null>(null);
    const { mutate: createPoll } = useCreateEventPoll(eventId);

    const {
        control,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<CreatePollFormValues>({
        defaultValues: {
            closesAt: null,
        },
    });

    async function onFormSubmit(values: CreatePollFormValues) {
        setSubmitError(null);

        const apiPayload = {
            pollType: pollType,
            closesAt: values.closesAt ? values.closesAt.format() : "",
        }

        try {
            await createPoll(apiPayload, {
            onError: (error) => {
                const message = error instanceof Error ? error.message : "Failed to create poll";
                setSubmitError(message);
            }});
        } catch (error) {
            const message = error instanceof Error ? error.message : "Failed to schedule event";
            setSubmitError(message);
        }
    }

    return (
        <Stack spacing={1} sx={{ textAlign: "left", justifyContent: 'center', alignItems: 'start' }}>
            <Typography variant="body1">
                Create a new {POLL_TYPE_NAMES[pollType]} poll for event {eventId}.
            </Typography>
            {submitError && <Alert severity="error">{submitError}</Alert>}
            <FormWrapper component="form" spacing={2} onSubmit={handleSubmit(onFormSubmit)} noValidate>
                <Controller
                    name="closesAt"
                    control={control}
                    rules={{ required: "Closing date is required" }}
                    render={({ field: { onChange, value } }) => (
                        <DateTimePicker
                            onChange={(newValue) => onChange(newValue)}
                            value={value}
                            label="Closing Date"
                            slotProps={{ 
                                textField: { 
                                    error: !!errors.closesAt, 
                                    helperText: errors.closesAt?.message || "Choose when the poll should close", 
                                    fullWidth: true 
                                } 
                            }}
                        />
                    )}
                />

                <Button type="submit" 
                    variant="contained" 
                    disabled={isSubmitting}
                    sx={{ alignSelf: 'flex-start' }}
                >
                    Create poll
                </Button>
            </FormWrapper>
        </Stack>
    );
}