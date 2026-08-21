import { useMutation, useQuery } from "@tanstack/react-query";
import type { EventDetailResponse, ScheduleEventRequest, UpcomingEventResponse } from "./types";
import { getEventById, getUpcomingBookClub, getUpcomingEvent, scheduleEvent } from "./api";

export function useUpcomingEvent() {
    return useQuery<UpcomingEventResponse>({
        queryKey: ["events", "upcoming"],
        queryFn: async () => {
            const response = await getUpcomingEvent();
            return response;
        }
    })
}

export function useUpcomingBookClub() {
    return useQuery<UpcomingEventResponse>({
        queryKey: ["events", "upcoming", "bookclub"],
        queryFn: async () => {
            const response = await getUpcomingBookClub();
            return response;
        }
    })
}

export function useEventById(eventId: string) {
    return useQuery<EventDetailResponse>({
        queryKey: ["events", eventId],
        queryFn: async () => {
            const response = await getEventById(eventId);
            return response;
        }
    })
}

export function useScheduleEvent() {
    return useMutation({
        mutationFn: async (input: ScheduleEventRequest) => {
            const response = await scheduleEvent(input);
            return response;
        }
    });
}