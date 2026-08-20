import { Alert, Box, Button, Stack, TextField, Typography } from "@mui/material";
import BackLink from "../components/BackLink";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Controller, useForm } from "react-hook-form";
import type { Dayjs } from "dayjs";
import { useScheduleEvent } from "../features/calendarEvents/hooks";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";

type CreateEventFormValues = {
    name: string;
    date: Dayjs | null;
    eventType: number;
};

export default function EventCreatePage() {
    const [submitError, setSubmitError] = useState<string | null>(null);
    const { mutate: scheduleEvent } = useScheduleEvent();
    const navigate = useNavigate();
    
    const {
        control,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<CreateEventFormValues>({
        defaultValues: {
            name: "",
            date: null,
            eventType: 1,
        },
    }); 

    async function onFormSubmit(values: CreateEventFormValues) {
        setSubmitError(null);

        const apiPayload = {
            name: values.name.trim(),
            date: values.date ? values.date.format('YYYY-MM-DD') : "",
            eventType: values.eventType,
        }

        try {
            await scheduleEvent(apiPayload, {
                onSuccess: (data) => {
                    navigate(`/events/${data.eventId}`);
                },
                onError: (error) => {
                    const message = error instanceof Error ? error.message : "Failed to schedule event";
                    setSubmitError(message);
                }
            });
        } catch (error) {
            const message = error instanceof Error ? error.message : "Failed to schedule event";
            setSubmitError(message);
        }
    }

    return (
        <Stack spacing={3} sx={{ justifyContent: 'center', alignItems: 'center' }}>
            <BackLink />
            <Box component="section">
                <Stack spacing={2}>
                    <Typography variant="h4">Create Event</Typography>
                    <Typography variant="body2" color="text.secondary">
                        Fill out the form below to create a new event.
                    </Typography>

                    {submitError ? <Alert severity="error">{submitError}</Alert> : null}

                    <Stack component="form" spacing={2} onSubmit={handleSubmit(onFormSubmit)} noValidate>
                        <Controller
                            name="name"
                            control={control}
                            rules={{ 
                                required: "Name is required",
                                validate: (value) => value.trim() !== "" || "Name is required"
                            }}
                            render={({ field }) => (
                                <TextField
                                    {...field}
                                    label="Event Name"
                                    error={!!errors.name}
                                    helperText={errors.name?.message}
                                    fullWidth
                                />
                            )}
                        />

                        <Controller
                            name="date"
                            control={control}
                            rules={{ required: "Date is required" }}
                            render={({ field: { onChange, value } }) => (
                                <DatePicker
                                    onChange={(newValue) => onChange(newValue)}
                                    value={value}
                                    label="Event Date"
                                    slotProps={{ 
                                        textField: { 
                                            error: !!errors.date, 
                                            helperText: errors.date?.message, 
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
                            Schedule event
                        </Button>
                    </Stack>
                </Stack>
            </Box>
        </Stack>
    );
}