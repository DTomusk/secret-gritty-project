import { Box, Stack, Typography } from "@mui/material";
import BackLink from "../components/BackLink";
export default function EventCreatePage() {

    return (
        <Stack spacing={3} sx={{ justifyContent: 'center', alignItems: 'center' }}>
            <BackLink />
            <Box component="section">
                <Stack spacing={3}>
                    <Stack spacing={1}>
                        <Typography variant="h4">Create Event</Typography>
                        <Typography variant="body2" color="text.secondary">
                            Fill out the form below to create a new event.
                        </Typography>
                    </Stack>
                </Stack>
            </Box>
        </Stack>
    );
}