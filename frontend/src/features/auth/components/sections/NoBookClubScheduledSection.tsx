import { useMembers } from "../../../members/hooks";
import MemberSelectForm from "../../../members/components/MemberSelectForm";
import { useChooseNextHost } from "../../../calendarEvents/hooks";

export function NoBookClubScheduledSection() {
    const { data: members } = useMembers();
    const { mutate: chooseNextHost } = useChooseNextHost();

    async function handleChooseNextHost(memberId: string) {
        await chooseNextHost({ hostId: memberId });

    }
    
    return (
        <MemberSelectForm
            title="The next book club hasn't been scheduled yet"
            subtitle="Choose the next host"
            members={members?.members || []}
            onSubmit={async (values) => {
                handleChooseNextHost(values.memberId);
            }}
        />
    );
}