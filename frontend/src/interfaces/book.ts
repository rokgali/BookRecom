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

export interface Book {
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