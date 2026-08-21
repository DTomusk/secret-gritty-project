import { useMembers } from "../../../members/hooks";
import MemberSelectForm from "../../../members/components/MemberSelectForm";

export function NoBookClubScheduledSection() {
    const { data: members } = useMembers();
    
    return (
        <MemberSelectForm
            title="No book club scheduled"
            subtitle="Choose the next host"
            members={members?.members || []}
        />
    );
}