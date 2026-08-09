import { useQuery } from "@tanstack/react-query";
import type { UpcomingEventResponse } from "./types";
import { getUpcomingEvent } from "./api";

export function useUpcomingEvent() {
    return useQuery<UpcomingEventResponse>({
        queryKey: ["events", "upcoming"],
        queryFn: async () => {
            const response = await getUpcomingEvent();
            return response;
        }
    })
}