import { Button } from "@mui/material";
import { ChevronLeft } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";

export default function BackLink() {
    const navigate = useNavigate();

    return (
        <Button sx={{ alignSelf: 'flex-start' }} variant="text" startIcon={<ChevronLeft />} onClick={() => navigate(-1)}>
            Back
        </Button>
    )
}