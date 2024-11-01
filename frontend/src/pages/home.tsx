import axios from "axios";
import { useEffect, useReducer, useState } from "react";
import { BookrecomAPIUrl, OpenLibraryUrl } from "../globals/ulrs";
import BookTableByAuthorKey from "../components/book_table_by_author_key";
import DataFetchReducer, { initialReducerState } from "../functions/data_fetch/data_fetch_reducer";
import Loading from "../components/loading";

interface Author {
    id: number,
    name: string,
    key: string
}

export default function HomePage()
{
    const [availableAuthors, setAvailableAuthors] = useState<Author[]>([]);
    const [filteredAvailableAuthors, setFilteredAvailableAuthors] = useState<Author[]>(availableAuthors);

    const [selectedAuthorKey, setSelectedAuthorKey] = useState<string>();

    const [availableAuthorLoadingState, dispatch] = useReducer(DataFetchReducer<Author[]>, initialReducerState);

    useEffect(() => {
        axios.get(`${BookrecomAPIUrl}/Author/GetAvailableAuthors`)
        .then(res => { dispatch({type: 'FETCH_SUCCESS', payload: res.data}); setAvailableAuthors(res.data) })
        .catch(err =>{ dispatch({type: 'FETCH_FAILURE', error: "Error while loading available authors"}) });
    }, [])

    function FilterAvailableAuthors(authorName: string)
    {
        setFilteredAvailableAuthors(availableAuthors.filter((author) => author.name.toLowerCase().includes(authorName.toLowerCase())));
    }

    function HandleAuthorSelection(authorName: string)
    {
        const selectedAuthor:Author | undefined = availableAuthors.find(author => author.name === authorName);

        selectedAuthor && setSelectedAuthorKey(selectedAuthor.key);
    }
    
    return (
        <>
            {availableAuthorLoadingState.isLoading ? <Loading /> : 
            availableAuthorLoadingState.error ? 
            <div>Failed to load authors by key</div> :
            <>
                <div className="text-bold text-xl">
                    Select an author from the list:
                </div>
                <input type="text" list="availableAuthorList" placeholder="Select an author" 
                        onChange={(e) => FilterAvailableAuthors(e.target.value)} 
                        onInput={(e) => HandleAuthorSelection((e.target as HTMLInputElement).value)} 
                    />
                    <datalist id="availableAuthorList">
                        {filteredAvailableAuthors.map((author, index) => (
                            <option key={index}>
                                {author.name}
                            </option>
                        ))}
                    </datalist>

                <BookTableByAuthorKey authorKey={selectedAuthorKey} />
            </>
            }
        </>
    );
}