import { api } from "../../lib/api/api"
import type { EventDetailResponse, ScheduleEventRequest, UpcomingEventResponse } from "./types"

export const getUpcomingEvent = async () => {
    return api.get<UpcomingEventResponse>("/Events/Next")
}

export const getEventById = async (eventId: string) => {
    return api.get<EventDetailResponse>(`/Events/${eventId}`)
}

export const scheduleEvent = async (input: ScheduleEventRequest) => {
    return api.post("/Events", JSON.stringify(input));
}

