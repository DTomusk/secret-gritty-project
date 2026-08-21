import { api } from "../../lib/api/api"
import type { EventDetailResponse, ScheduleEventRequest, UpcomingEventResponse, ScheduleEventResponse, ChooseNextHostRequest } from "./types"

export const getUpcomingEvent = async () => {
    return api.get<UpcomingEventResponse>("/Events/Next")
}

export const getUpcomingBookClub = async () => {
    return api.get<UpcomingEventResponse>("/Events/Next/BookClub")
}

export const getEventById = async (eventId: string) => {
    return api.get<EventDetailResponse>(`/Events/${eventId}`)
}

export const scheduleEvent = async (input: ScheduleEventRequest) => {
    return api.post<ScheduleEventResponse>("/Events", JSON.stringify(input));
}

export const chooseNextHost = async (input: ChooseNextHostRequest) => {
    return api.post<void>(`/Events/Next/BookClub/Host`, JSON.stringify(input));
}
