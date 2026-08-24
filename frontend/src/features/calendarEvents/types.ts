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
    currentUserIsHost: boolean;
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

export type EventPollResponse = {
    pollId: string;
    type: number;
    closesAt: string; // ISO date string
    options: EventPollOptionResponse[];
}

export type EventPollOptionResponse = {
    optionId: string;
    optionText: string;
}

export const POLL_TYPES = {
    DATE: 1,
    LOCATION: 2,
    BOOK: 3
} as const;

export const POLL_TYPE_NAMES: Record<number, string> = {
    [POLL_TYPES.DATE]: "Date",
    [POLL_TYPES.LOCATION]: "Location",
    [POLL_TYPES.BOOK]: "Book"
};

export const BOOK_CLUB_POLL_TYPES = [POLL_TYPES.DATE, POLL_TYPES.LOCATION, POLL_TYPES.BOOK];

export type CreateEventPollRequest = {
    pollType: number;
    closesAt: string;
}