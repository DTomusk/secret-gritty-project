import { Stack, type StackProps } from "@mui/material";
import { type ReactNode, type FormHTMLAttributes } from "react";

type FormWrapperProps = StackProps & FormHTMLAttributes<HTMLFormElement> & {
    children: ReactNode;
};

export default function FormWrapper({ children, ...props }: FormWrapperProps) {
    return (
        <Stack
            {...props}
            sx={{
                width: "100%",
                minWidth: "300px",
                maxWidth: "500px",
                ...props.sx,
            }}
        >
            {children}
        </Stack>
    );
}
