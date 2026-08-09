export type ScheduleEventRequest = {
    name: string;
    // ISO date string, e.g. "2023-06-15"
    date: string;
    eventType: number;
}

export type UpcomingEventResponse = {
    name: string;
    date: string; 
    eventType: number;
    scheduledByUserName: string;
}