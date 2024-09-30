import { Book } from "./book"

export interface OpenLibraryBookSearchResult {
    numFound: number,
    start: number,
    numFoundExact: boolean
    docs: Book[]
}