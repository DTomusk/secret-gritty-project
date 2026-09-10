import { Outlet } from "react-router-dom";
import NavBar from "../components/NavBar";
import Box from "@mui/material/Box";
import Stack from "@mui/material/Stack";

export default function AppLayout() {
    return (
        <Stack sx={{ 
            height: "100dvh", 
            overflow: "hidden", 
            justifyContent: "center", 
            alignItems: "center"
            }}
            color="background.default"
        >
            <NavBar />
            <Box
                sx={{
                    flex: 1,
                    minHeight: 0,
                    overflowY: "auto",
                    display: 'flex',
                    alignItems: 'start',
                    justifyContent: 'center',
                    // width: {xs: "100%", sm: "80%", md: "60%", lg: "50%"},
                    width: "100%",
                    px: 2,
                    py: 2,
                    background: (theme) =>
                        `linear-gradient(135deg, ${theme.palette.primary.light}1f 0%, ${theme.palette.background.default} 45%, ${theme.palette.secondary.light}1a 100%)`,
                }}
            >
                <Outlet />
            </Box>
        </Stack>
    )
}