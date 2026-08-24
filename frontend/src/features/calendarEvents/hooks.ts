import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type { ChooseNextHostRequest, CreateEventPollRequest, EventDetailResponse, ScheduleEventRequest, UpcomingEventResponse } from "./types";
import { chooseNextHost, createPollForEvent, getEventById, getPollsByEventId, getUpcomingBookClub, getUpcomingEvent, scheduleEvent } from "./api";

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

export function useChooseNextHost() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: async (input: ChooseNextHostRequest) => {
            const response = await chooseNextHost(input);
            return response;
        },
        onSuccess: () => {
            // Invalidate the upcoming book club query to refetch the updated data
            queryClient.invalidateQueries({
                queryKey: ["events", "upcoming", "bookclub"],
            });
        }
    });
}

export function useEventPolls(eventId: string) {
    return useQuery({
        queryKey: ["events", eventId, "polls"],
        queryFn: async () => {
            const response = await getPollsByEventId(eventId);
            return response;
        }
    })
}

export function useCreateEventPoll(eventId: string) {
    const queryClient = useQueryClient();
    
    return useMutation({
        mutationFn: async (input: CreateEventPollRequest) => {
            const response = await createPollForEvent(eventId, input);
            return response;
        },
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["events", eventId, "polls"],
            });
        }
    });
}