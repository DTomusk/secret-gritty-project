import { Alert, Stack, Typography } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import { useEventById } from "../features/calendarEvents/hooks";
import Spinner from "../components/Spinner";

export default function EventDetailPage() {
    let { id } = useParams<{ id: string }>();
    const navigate = useNavigate();

    if (!id) {
        navigate("/events");
        return null;
    }

    const { data, isLoading, error } = useEventById(id);

    return (
        <Stack>
            <Typography variant="h1">Event Details</Typography>
            {isLoading && <Spinner/>}
            {error && <Alert severity="error">Failed to load event details</Alert>}
            {data && (
                <Stack>
                    <Typography variant="h3">{data.name}</Typography>
                    <Typography variant="body2">Date: {data.date}</Typography>
                </Stack>
            )}
        </Stack>
    );
}