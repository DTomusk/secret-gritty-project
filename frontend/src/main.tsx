import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import { initApiClient } from './lib/api/client.ts';
import { CssBaseline, ThemeProvider } from '@mui/material';
import { appTheme } from './theme.ts';
import './i18n';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';

initApiClient(import.meta.env.VITE_API_BASE_URL);

const queryClient = new QueryClient();

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ThemeProvider theme={appTheme}>
      <CssBaseline />
      <QueryClientProvider client={queryClient}>
        <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="en-gb">
          <App />
        </LocalizationProvider> 
      </QueryClientProvider>
    </ThemeProvider>
  </StrictMode>,
)
