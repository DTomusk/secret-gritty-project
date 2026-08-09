import { api } from "../../lib/api/api"
import type { ScheduleEventRequest, UpcomingEventResponse } from "./types"

export const getUpcomingEvent = async () => {
    return api.get<UpcomingEventResponse>("/Events/Next")
}

export const scheduleEvent = async (input: ScheduleEventRequest) => {
    return api.post("/Events", JSON.stringify(input));
}

