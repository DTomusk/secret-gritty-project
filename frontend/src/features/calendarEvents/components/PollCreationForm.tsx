import { Alert, Button, Stack, TextField, Typography } from "@mui/material";
import { POLL_TYPE_NAMES, POLL_TYPES } from "../types";
import FormWrapper from "../../../components/FormWrapper";
import { Controller, useFieldArray, useForm } from "react-hook-form";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import type { Dayjs } from "dayjs";
import { useState } from "react";
import { useCreateEventPoll } from "../hooks";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";

type PollCreationFormProps = {
    eventId: string;
    pollType: number;
};

type CreatePollFormValues = {
    closesAt: Dayjs | null;
    options: DateOption[] | LocationOption[] | BookOption[];
};

type DateOption = { date: Dayjs | null };
type LocationOption = { location: string | null };
type BookOption = { title: string | null; author: string | null };

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
            options: [getEmptyOption(pollType)],
        },
    });

    const { fields, append, remove } = useFieldArray({
        control,
        name: "options",
    });

    function getEmptyOption(type: number) {
        if (type === POLL_TYPES.DATE) return { date: null };
        if (type === POLL_TYPES.BOOK) return { title: "", author: "" };
        return { location: "" };
    }

    async function onFormSubmit(values: CreatePollFormValues) {
        setSubmitError(null);

        const apiPayload = {
            pollType: pollType,
            closesAt: values.closesAt ? values.closesAt.format() : "",
            options: values.options.map((opt: any) => ({
                date: opt.date ? opt.date.format() : undefined,
                location: opt.location,
                title: opt.title,
                author: opt.author,
            })),
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

                <Typography variant="h6">Options</Typography>
                {fields.map((field, index) => (
                    <Stack key={field.id} spacing={1}>
                        {pollType === POLL_TYPES.DATE && (
                            <Controller
                                name={`options.${index}.date`}
                                control={control}
                                render={({ field }) => (
                                    <DatePicker {...field} label="Option date" />
                                )}
                            />
                        )}
                        {pollType === POLL_TYPES.LOCATION && (
                            <Controller
                                name={`options.${index}.location`}
                                control={control}
                                render={({ field }) => (
                                    <TextField {...field} label="Location" fullWidth />
                                )}
                            />
                        )}
                        {pollType === POLL_TYPES.BOOK && (
                            <>
                                <Controller
                                    name={`options.${index}.title`}
                                    control={control}
                                    render={({ field }) => (
                                        <TextField {...field} label="Book title" fullWidth />
                                    )}
                                />
                                <Controller
                                    name={`options.${index}.author`}
                                    control={control}
                                    render={({ field }) => (
                                        <TextField {...field} label="Author" fullWidth />
                                    )}
                                />
                            </>
                        )}
                        {fields.length > 1 && (
                            <Button onClick={() => remove(index)} color="error">
                                Remove option
                            </Button>
                        )}
                    </Stack>
                ))}
            
            <Button onClick={() => append(getEmptyOption(pollType))}>
                Add another option
            </Button>

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