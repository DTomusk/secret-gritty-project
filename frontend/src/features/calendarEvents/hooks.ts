import { useQuery } from "@tanstack/react-query";
import type { EventDetailResponse, UpcomingEventResponse } from "./types";
import { getEventById, getUpcomingEvent } from "./api";

export function useUpcomingEvent() {
    return useQuery<UpcomingEventResponse>({
        queryKey: ["events", "upcoming"],
        queryFn: async () => {
            const response = await getUpcomingEvent();
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