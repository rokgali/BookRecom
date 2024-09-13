import { Book } from "./book"

export interface OpenLibrarySearchResult {
    numFound: number,
    start: number,
    numFoundExact: boolean
    docs: Book[]
}