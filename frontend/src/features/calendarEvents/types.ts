export type ScheduleEventRequest = {
    name: string;
    // ISO date string, e.g. "2023-06-15"
    date: string;
    eventType: number;
}

export type ScheduleEventResponse = {
    eventId: string;
}

export type UpcomingEventResponse = {
    id: string;
    name: string;
    date: string; 
    eventType: number;
    hostUserName: string;
}

export type EventDetailResponse = {
    name: string;
    date: string; 
    eventType: number;
    hostUserName: string;
}

export type ChooseNextHostRequest = {
    hostId: string;
}