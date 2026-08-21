import { Alert, Box, Button, Link, Stack, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { Link as RouterLink } from "react-router-dom";
import FormWrapper from "../../../components/FormWrapper";
import type { RegisterSchema } from "../schemas/registerSchema";

type RegistrationFormValues = {
  registrationCode: string;
  password: string;
  confirmPassword: string;
};

type RegistrationFormProps = {
  onSubmit?: (values: RegisterSchema) => Promise<void> | void;
};

export default function RegistrationForm({ onSubmit }: RegistrationFormProps) {
  const [submitError, setSubmitError] = useState<string | null>(null);
  const { t } = useTranslation(["auth", "common"]);

  const {
    control,
    handleSubmit,
    formState: { errors, isSubmitting },
    setError,
  } = useForm<RegistrationFormValues>({
    defaultValues: {
      registrationCode: "",
      password: "",
      confirmPassword: "",
    },
  });

  async function onFormSubmit(values: RegistrationFormValues) {
    setSubmitError(null);

    if (values.password !== values.confirmPassword) {
      setError("confirmPassword", {
        type: "validate",
        message: t("auth:validation.passwordsDoNotMatch"),
      });
      return;
    }

    try {
      if (onSubmit) {
        await onSubmit({ registrationCode: values.registrationCode, password: values.password });
      } else {
        await new Promise((resolve) => setTimeout(resolve, 500));
      }
    } catch (error) {
      const message =
        error instanceof Error ? error.message : t("common:errors.genericSubmit");
      setSubmitError(message);
    }
  }

  return (
    <Box component="section">
      <Stack spacing={3}>
        <Stack spacing={1}>
          <Typography variant="h5">{t("auth:title.register")}</Typography>
          <Typography variant="body2" color="text.secondary">
            {t("auth:subtitle.register")}
          </Typography>
        </Stack>

        {submitError ? <Alert severity="error">{submitError}</Alert> : null}

        <FormWrapper component="form" spacing={2} onSubmit={handleSubmit(onFormSubmit)} noValidate>
          <Controller
            name="registrationCode"
            control={control}
            rules={{
              required: t("auth:validation.registrationCodeRequired"),
            }}
            render={({ field }) => (
              <TextField
                {...field}
                label={t("auth:fields.registrationCode")}
                type="text"
                autoComplete="registration-code"
                error={!!errors.registrationCode}
                helperText={errors.registrationCode?.message}
                fullWidth
              />
            )}
          />

          <Controller
            name="password"
            control={control}
            rules={{
              required: t("auth:validation.passwordRequired"),
              minLength: {
                value: 8,
                message: t("auth:validation.passwordMinLength"),
              },
            }}
            render={({ field }) => (
              <TextField
                {...field}
                label={t("auth:fields.password")}
                type="password"
                autoComplete="new-password"
                error={!!errors.password}
                helperText={errors.password?.message}
                fullWidth
              />
            )}
          />

          <Controller
            name="confirmPassword"
            control={control}
            rules={{
              required: t("auth:validation.confirmPasswordRequired"),
            }}
            render={({ field }) => (
              <TextField
                {...field}
                label={t("auth:fields.confirmPassword")}
                type="password"
                autoComplete="new-password"
                error={!!errors.confirmPassword}
                helperText={errors.confirmPassword?.message}
                fullWidth
              />
            )}
          />

          <Button type="submit" variant="contained" size="large" disabled={isSubmitting}>
            {isSubmitting ? t("common:actions.submitting") : t("auth:actions.submitRegister")}
          </Button>
        </FormWrapper>

        <Typography variant="body2" color="text.secondary">
          {t("auth:prompts.alreadyHaveAccount")}
          <Link
            component={RouterLink}
            to="/auth/login"
            underline="hover"
            sx={{ fontWeight: 600 }}
          >
            {t("auth:actions.switchToLogin")}
          </Link>
        </Typography>
      </Stack>
    </Box>
  );
}
