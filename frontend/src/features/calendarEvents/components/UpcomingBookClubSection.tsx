import { useNavigate } from "react-router-dom";
import { Stack, Typography, Button } from "@mui/material";
import type { UpcomingEventResponse } from "../types";
import { daysUntil } from "../../../lib/utils/dateConverter";

export function UpcomingBookClubSection({ bookClub }: { bookClub: UpcomingEventResponse }) {
    const navigate = useNavigate();
    
    return (
        <Stack spacing={3}>
            <Typography variant="h3">
                It's {daysUntil(bookClub.date)} days until {bookClub.name}
            </Typography>
            <Button 
                variant="contained" 
                onClick={() => navigate(`/events/${bookClub.id}`)}
                sx={{ mt: 2, alignSelf: 'center' }}
            >
                View event details
            </Button>
        </Stack>
    );
}