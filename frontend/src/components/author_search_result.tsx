import { useEffect, useState } from "react";
import OpenLibraryAuthorSearchResult from "../interfaces/openlibrary_author_search_results";
import axios from "axios";
import Loading from "./loading";
import { BookrecomAPIUrl, OpenLibraryUrl } from "./globals/ulrs";
import OperationSuccess from "./notifications/operation_success";
import OperationFailure from "./notifications/operation_failure";

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

    const [showOperationSuccess, setShowOperationSuccess] = useState<boolean>(false);
    const [operationSuccessMessage, setOperationSuccessMessage] = useState<string>('');

    const [showOperationFailure, setShowOperationFailute] = useState<boolean>(false);
    const [operationFailureMessage, setOperationFailureMessage] = useState<string>('');

    const [refreshGetAuthors, setRefreshGetAuthors] = useState<boolean>(false);

    useEffect(() => {
        GetAuthors();
        setRefreshGetAuthors(false);
    }, [refreshGetAuthors])

    useEffect(() => {
        axios.get(`${OpenLibraryUrl}/search/authors.json?q=${authorName}`)
        .then(res => {
            setSearchAuthors(res.data);
        })
        .catch(err => {
            console.error(err);
        })
    }, [authorName]);

    function GetAuthors() {
        axios.get(`${BookrecomAPIUrl}/Author/GetAvailableAuthors`)
        .then(res => { setSelectedAuthors(res.data); setSelectedAuthorsLoading(false) })
        .catch(err => { setSelectedAuthorsLoading(false) })
    }

    function SaveAuthorToDb(authorToSend: Author) {
        axios.post(`${BookrecomAPIUrl}/Author/CreateAuthor`, authorToSend)
        .then(res => {setShowOperationSuccess(true); setOperationSuccessMessage('Author created succesfully'); setRefreshGetAuthors(true)})
        .catch(err => {setShowOperationFailute(true); setOperationFailureMessage('Failed to create author')});
    }

    function RemoveAuthorFromDb(authorKey: string) {
        axios.delete(`${BookrecomAPIUrl}/Author/RemoveAuthor?authorKey=${authorKey}`)
        .then(res => {setShowOperationSuccess(true); setOperationSuccessMessage('Author removed succesfully'); setRefreshGetAuthors(true)})
        .catch(err => {setShowOperationFailute(true); setOperationFailureMessage('Failed to remove author')});
    }

    function ShowOperationSuccessComplete()
    {
        setShowOperationSuccess(false);
        setOperationSuccessMessage('');
    }

    function ShowOperationFailureComplete()
    {
        setShowOperationFailute(false);
        setOperationFailureMessage('');
    }

    return (
    <>
        {showOperationSuccess &&         
            <div>
                <OperationSuccess message={operationSuccessMessage} duration={5000} onComplete={() => ShowOperationSuccessComplete()} />
            </div>
        }

        {showOperationFailure &&
            <div>
                <OperationFailure message={operationFailureMessage} duration={5000} onComplete={() => ShowOperationFailureComplete()} />
            </div>
        }

        <div className="flex">
            {selectedAuthorsLoading ? <Loading /> :
            <>
                <div className="flex-1 mr-4 p-2">
                    <div>
                        <input className="border rounded-lg px-2" placeholder="Search for author" 
                        type="text" value={authorName} onChange={(e) => setAuthorName(e.target.value)} />
                    </div>
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
                        <div key={index}>
                            <div className="mr-4">{author.name} {author.key}</div>
                            <button className="rounded-lg shadow-lg py-4 px-2 bg-red-400" onClick={() => RemoveAuthorFromDb(author.key)}>Remove author</button>
                        </div>
                    ))}
                </div>
            </>
            }
        </div>
    </>);
}