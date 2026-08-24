import { Alert, Button, Stack, TextField, Typography } from "@mui/material";
import { POLL_TYPE_NAMES, POLL_TYPES } from "../types";
import FormWrapper from "../../../components/FormWrapper";
import { Controller, useFieldArray, useForm } from "react-hook-form";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import type { Dayjs } from "dayjs";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";

type PollFormProps = {
    pollType: number;
    onSubmit: (values: CreatePollFormValues) => void | Promise<void>;
    isSubmitting?: boolean;
    submitError?: string | null;
    initialValues?: CreatePollFormValues;
    submitButtonLabel?: string;
};

type CreatePollFormValues = {
    closesAt: Dayjs | null;
    options: DateOption[] | LocationOption[] | BookOption[];
};

type DateOption = { date: Dayjs | null };
type LocationOption = { location: string | null };
type BookOption = { title: string | null; author: string | null };

function getEmptyOption(type: number) {
    if (type === POLL_TYPES.DATE) return { date: null };
    if (type === POLL_TYPES.BOOK) return { title: "", author: "" };
    return { location: "" };
}

export default function PollForm({
    pollType,
    onSubmit,
    isSubmitting = false,
    submitError,
    initialValues,
    submitButtonLabel = "Create poll",
}: PollFormProps) {
    const {
        control,
        handleSubmit,
        formState: { errors },
    } = useForm<CreatePollFormValues>({
        defaultValues: initialValues || {
            closesAt: null,
            options: [getEmptyOption(pollType)],
        },
    });

    const { fields, append, remove } = useFieldArray({
        control,
        name: "options",
    });

    return (
        <Stack spacing={1} sx={{ textAlign: "left", justifyContent: "center", alignItems: "start" }}>
            <Typography variant="body1">
                {POLL_TYPE_NAMES[pollType]} poll
            </Typography>
            {submitError && <Alert severity="error">{submitError}</Alert>}
            <FormWrapper component="form" spacing={2} onSubmit={handleSubmit(onSubmit)} noValidate>
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
                                    fullWidth: true,
                                },
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
                                rules={{ required: "Date is required" }}
                                render={({ field }) => (
                                    <DatePicker {...field} label="Option date" />
                                )}
                            />
                        )}
                        {pollType === POLL_TYPES.LOCATION && (
                            <Controller
                                name={`options.${index}.location`}
                                control={control}
                                rules={{ required: "Location is required" }}
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
                                    rules={{ required: "Book title is required" }}
                                    render={({ field }) => (
                                        <TextField {...field} label="Book title" fullWidth />
                                    )}
                                />
                                <Controller
                                    name={`options.${index}.author`}
                                    control={control}
                                    rules={{ required: "Author is required" }}
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

                <Button
                    type="submit"
                    variant="contained"
                    disabled={isSubmitting}
                    sx={{ alignSelf: "flex-start" }}
                >
                    {submitButtonLabel}
                </Button>
            </FormWrapper>
        </Stack>
    );
}

export type { CreatePollFormValues };
