import Author from "./author"

export default interface OpenLibraryAuthorSearchResult {
    numFound: number,
    start: number,
    numFoundExact: boolean
    docs: Author[]
}