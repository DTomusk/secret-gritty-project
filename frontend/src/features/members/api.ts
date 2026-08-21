import { api } from "../../lib/api/api"
import type { MemberResponse } from "./types"

export const getAllMembers = async () => {
    return api.get<MemberResponse>("/Members")
}