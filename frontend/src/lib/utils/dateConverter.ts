export function daysUntil(isoDateString: string): number {
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    const targetDate = new Date(isoDateString);
    targetDate.setHours(0, 0, 0, 0);

    const timeDifference = targetDate.getTime() - today.getTime();
    const daysDifference = Math.ceil(timeDifference / (1000 * 3600 * 24));
    return daysDifference;
}