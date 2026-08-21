import { Stack, Typography, FormControl, InputLabel, Select, MenuItem, Alert, Button } from "@mui/material";
import { useState } from "react";
import { Controller, useForm } from "react-hook-form";
import FormWrapper from "../../../components/FormWrapper";

type MemberSelectFormValues = {
    memberId: string;
}

type MemberSelectFormProps = {
    title?: string;
    subtitle?: string;
    members: { memberId: string; memberName: string }[];
    onSubmit?: (values: MemberSelectFormValues) => Promise<void> | void;
};

export default function MemberSelectForm({ title, subtitle, members, onSubmit }: MemberSelectFormProps) {
    const [submitError, setSubmitError] = useState<string | null>(null);
    const {
        control, 
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<MemberSelectFormValues>({
        defaultValues: {
            memberId: "",
        },
    });

    async function onFormSubmit(values: MemberSelectFormValues) {
        try {
            if (onSubmit) {
                await onSubmit(values);
            }
        } catch (error) {
            console.error(error);
            setSubmitError(error instanceof Error ? error.message : "Failed to submit form");
        }
    }

    return (
        <Stack spacing={2} sx={{ mt: 2, justifyContent: 'center', alignItems: 'center' }}>
            {title && <Typography variant="h5">{title}</Typography>}
            {subtitle && <Typography variant="body2">{subtitle}</Typography>}
            {submitError ? <Alert severity="error">{submitError}</Alert> : null}
            <FormWrapper component="form" spacing={2} onSubmit={handleSubmit(onFormSubmit)} noValidate>
                <Controller
                    name="memberId"
                    control={control}
                    rules={{
                        required: "Please select a member",
                    }}
                    render={({ field }) => (
                        <FormControl fullWidth error={!!errors.memberId}>
                            <InputLabel id="member-select-label">Select Member</InputLabel>
                            <Select
                                {...field}
                                labelId="member-select-label"
                                id="member-select"
                                label="Select Member"
                            >
                                {members.map((member) => (
                                    <MenuItem key={member.memberId} value={member.memberId}>
                                        {member.memberName}
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    )}
                />
                <Button type="submit" variant="contained" size="large" disabled={isSubmitting}>
                    {isSubmitting ? "Submitting..." : "Submit"}
                </Button>
            </FormWrapper>
        </Stack>
    );
}