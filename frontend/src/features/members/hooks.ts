import { useQuery } from "@tanstack/react-query";
import type { MemberResponse } from "./types";
import { getAllMembers } from "./api";

export function useMembers() {
    return useQuery<MemberResponse>({
        queryKey: ["members"],
        queryFn: async () => {
            const response = await getAllMembers();
            return response;
        }
    })
}