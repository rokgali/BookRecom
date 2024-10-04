import TakeawaysResponse from "./takeawaysResponse"

interface description {
    type: string,
    value: string,
    title: string
}

interface author {
    key: string   
}

interface authors {
    author: author
}

interface description {
    type: string,
    value: string
}

interface created {
    type: string,
    value: string
}

interface last_modified {
    type: string,
    value: string
}

export interface GetBook {
    title: string,
    key: string,
    author_key: string[]
    authors: authors[],
    author_name: string[],
    covers: number[],
    cover_i: number
    description: description,
    subjects: string[],
    created: created,
    last_modified: last_modified
}

interface Author {
    name: string,
    key: string
}

export interface Takeaway {
    name: string,
    lesson: string,
    episode: string
}

export interface PostBook {
    title: string,
    workId: string,
    coverId: number,
    author: Author,
    takeawaysHeading: string,
    description: string,
    takeaways: Takeaway[]
}