export interface Takeaway {
    name: string,
    lesson: string,
    episode: string
}

export default interface TakeawaysResponse {
    heading: string | null,
    takeaways: Takeaway[]
}