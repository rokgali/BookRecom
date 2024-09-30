import { useEffect, useState } from "react";
import OpenLibraryAuthorSearchResult from "../interfaces/openlibrary_author_search_results";
import axios from "axios";
import Loading from "./loading";

interface AuthorToSend {
    name: string,
    key: string
}

export default function AuthorSearchResultTable()
{
    const [searchAuthors, setSearchAuthors] = useState<OpenLibraryAuthorSearchResult>();
    const [authorName, setAuthorName] = useState<string>('');
    const [selectedAuthorKeys, setSelectedAuthorKeys] = useState<string[]>([]);
    const [selectedAuthorKeysLoading, setSelectedAuthorKeysLoading] = useState<boolean>(true);

    useEffect(() => {
        axios.get(`https://openlibrary.org/search/authors.json?q=${authorName}`)
        .then(res => {
            setSearchAuthors(res.data);
        })
        .catch(err => {
            console.error(err);
        })
    }, [authorName]);

    function SaveAuthorToDb(authorToSend: AuthorToSend) {
        console.log(authorToSend);
    }

    return (
    <>
        <div>
            {selectedAuthorKeysLoading ? <Loading /> : 
            <div>
                <input type="text" value={authorName} onChange={(e) => setAuthorName(e.target.value)} />
                <ul className="space-y-4">
                {searchAuthors?.docs.map((author, index) => (
                <li key={index}>
                    <div>{author.name}</div>
                    <div>{author.top_work}</div>
                    <div>{author.key}</div>
                    <div className="text-bold text-lg">Author subjects:</div>
                    {author.top_subjects && author.top_subjects.map((subject, key) => (
                        <div key={key}>{subject}</div>
                    ))}
                    <button className="bg-green-400 p-2 rounded-lg mt-2" onClick={() => 
                        SaveAuthorToDb({name: author.name, key: author.key})}>Add as searchable</button>
                </li>
                ))}
            </ul>
            </div>}
        </div>
    </>);
}