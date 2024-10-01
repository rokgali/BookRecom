import { useEffect, useState } from "react";
import OpenLibraryAuthorSearchResult from "../interfaces/openlibrary_author_search_results";
import axios from "axios";
import Loading from "./loading";
import SavedSuccesfully from "./notifications/saved_succesfully";

interface Author {
    name: string,
    key: string
}

export default function AuthorSearchResultTable()
{
    const [searchAuthors, setSearchAuthors] = useState<OpenLibraryAuthorSearchResult>();
    const [authorName, setAuthorName] = useState<string>('');
    const [selectedAuthors, setSelectedAuthors] = useState<Author[]>([]);
    const [selectedAuthorsLoading, setSelectedAuthorsLoading] = useState<boolean>(true);

    const [showSavedAuthorSuccessfully, setShowSavedAuthorSuccesfully] = useState<boolean>(false);
    const [refreshGetAuthors, setRefreshGetAuthors] = useState<boolean>(false);

    useEffect(() => {
        GetAuthors();
        setRefreshGetAuthors(false);
    }, [refreshGetAuthors])

    useEffect(() => {
        axios.get(`https://openlibrary.org/search/authors.json?q=${authorName}`)
        .then(res => {
            setSearchAuthors(res.data);
        })
        .catch(err => {
            console.error(err);
        })
    }, [authorName]);

    function GetAuthors() {
        axios.get(`http://localhost:5103/api/Author/GetAvailableAuthors`)
        .then(res => { setSelectedAuthors(res.data); setSelectedAuthorsLoading(false) })
        .catch(err => { setSelectedAuthorsLoading(false) })
    }

    function SaveAuthorToDb(authorToSend: Author) {
        axios.post(`http://localhost:5103/api/Author/CreateAuthor`, authorToSend)
        .then(res => {setShowSavedAuthorSuccesfully(true); setRefreshGetAuthors(true)})
        .catch(err => {console.error(err)})

    }

    function RemoveAuthorFromDb(authorKey: string) {

    }

    function ShowSavedAuthorSuccesfullyComplete()
    {
        setShowSavedAuthorSuccesfully(false);
    }

    return (
    <>
        {showSavedAuthorSuccessfully &&         
            <div>
                <SavedSuccesfully message="Saved author succesfully" duration={5000} onComplete={() => ShowSavedAuthorSuccesfullyComplete()} />
            </div>
        }
        <div className="flex">
            {selectedAuthorsLoading ? <Loading /> :
            <>
                <div className="flex-1 mr-4">
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
                            {selectedAuthors.map(sa => sa.key).includes(author.key) ? 
                                <div className="bg-red-400">Author already added</div> 
                            :
                                <button className="bg-green-400 p-2 rounded-lg mt-2" onClick={() => 
                                SaveAuthorToDb({name: author.name, key: author.key})}>Add as searchable</button>
                            } 
                        </li>
                        ))}
                    </ul>
                </div>
                <div className="flex-1">
                    {selectedAuthors.map((author, index) => (
                        <div key={index}>{author.name} {author.key}</div>
                    ))}
                </div>
            </>
            }
        </div>
    </>);
}