import { Alert, Box, Button, Link, Stack, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { Link as RouterLink } from "react-router-dom";
import FormWrapper from "../../../components/FormWrapper";
import type { LoginSchema } from "../schemas/loginSchema";

type LoginFormValues = {
  username: string;
  password: string;
};

type LoginFormProps = {
  onSubmit?: (values: LoginSchema) => Promise<void> | void;
};

export default function LoginForm({ onSubmit }: LoginFormProps) {
  const [submitError, setSubmitError] = useState<string | null>(null);
  const { t } = useTranslation(["auth", "common"]);

  const {
    control,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<LoginFormValues>({
    defaultValues: {
      username: "",
      password: "",
    },
  });

  async function onFormSubmit(values: LoginFormValues) {
    setSubmitError(null);

    try {
      if (onSubmit) {
        await onSubmit({ username: values.username, password: values.password });
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
          <Typography variant="h5">{t("auth:title.login")}</Typography>
          <Typography variant="body2" color="text.secondary">
            {t("auth:subtitle.login")}
          </Typography>
        </Stack>

        {submitError ? <Alert severity="error">{submitError}</Alert> : null}

        <FormWrapper component="form" spacing={2} onSubmit={handleSubmit(onFormSubmit)} noValidate>
          <Controller
            name="username"
            control={control}
            rules={{
              required: t("auth:validation.usernameRequired"),
            }}
            render={({ field }) => (
              <TextField
                {...field}
                label={t("auth:fields.username")}
                type="text"
                autoComplete="username"
                error={!!errors.username}
                helperText={errors.username?.message}
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
                autoComplete="current-password"
                error={!!errors.password}
                helperText={errors.password?.message}
                fullWidth
              />
            )}
          />

          <Button type="submit" variant="contained" size="large" disabled={isSubmitting}>
            {isSubmitting ? t("common:actions.submitting") : t("auth:actions.submitLogin")}
          </Button>
        </FormWrapper>

        <Typography variant="body2" color="text.secondary">
          {t("auth:prompts.newHere")}
          <Link
            component={RouterLink}
            to="/auth/register"
            underline="hover"
            sx={{ fontWeight: 600 }}
          >
            {t("auth:actions.switchToRegister")}
          </Link>
        </Typography>
      </Stack>
    </Box>
  );
}
