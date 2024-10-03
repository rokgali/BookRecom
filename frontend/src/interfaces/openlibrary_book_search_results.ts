import { GetBook } from "./book"

export interface OpenLibraryBookSearchResult {
    numFound: number,
    start: number,
    numFoundExact: boolean
    docs: GetBook[]
}