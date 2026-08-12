import AuthLayout from '../layout/AuthLayout'
import NotFoundPage from '../pages/NotFoundPage'
import LoginPage from '../pages/LoginPage'
import RegistrationPage from '../pages/RegistrationPage'
import HomePage from '../pages/HomePage'
import AppLayout from '../layout/AppLayout'
import { createBrowserRouter } from 'react-router-dom'
import ProtectedLayout from './ProtectedLayout'
import EventsPage from '../pages/EventsPage'
import EventDetailPage from '../pages/EventDetailPage'
import EventCreatePage from '../pages/EventCreatePage'

export const router = createBrowserRouter([
    {
        path: '/',
        element: <AppLayout />,
        children: [
            {
                element: <ProtectedLayout />,
                children: [
                    {
                        index: true,
                        element: <HomePage />
                    },
                    {
                        path: 'events',
                        children: [
                            {
                                index: true,
                                element: <EventsPage />
                            },
                            {
                                path: 'create',
                                element: <EventCreatePage />
                            },
                            {
                                path: ':id',
                                element: <EventDetailPage />
                            }
                        ]
                    },
                    {
                        path: '*',
                        element: <NotFoundPage />
                    }
                ]
            }
        ]
    },
    {
        path: '/auth',
        element: <AuthLayout />,
        children: [
            {
                path: 'login',
                element: <LoginPage />
            },
            {
                path: 'register',
                element: <RegistrationPage />
            },
        ]
    }
])