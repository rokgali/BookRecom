import axios from "axios";
import { OpenLibraryCoversUlr, OpenLibraryUrl } from "../globals/ulrs";
import { useEffect, useReducer, useState } from "react";
import { OpenLibraryBookSearchResult } from "../interfaces/openlibrary_book_search_results";
import DataFetchReducer, { initialReducerState } from "../functions/data_fetch/data_fetch_reducer";
import Loading from "./loading";
import { BookPageProps } from "../interfaces/bookpageprops";
import { useNavigate } from "react-router-dom";
import BookCardWithImageLoading from "./book_card_with_image_loading";

interface BookTableByAuthorKeyProps {
    authorKey: string | undefined
}

export default function BookTableByAuthorKey(props: BookTableByAuthorKeyProps)
{
    const [bookSearchResult, setBookSearchResult] = useState<OpenLibraryBookSearchResult>();
    const [booksLoadingState, dispatch] = useReducer(DataFetchReducer<OpenLibraryBookSearchResult>, initialReducerState);
    
    const navigate = useNavigate();

    useEffect(() => {
        if(!props.authorKey)
            return;

        axios.get(`${OpenLibraryUrl}/search.json?author_key=${props.authorKey}`)
        .then(res => {
            const response: OpenLibraryBookSearchResult = res.data;
            setBookSearchResult(response);
            dispatch({type: 'FETCH_SUCCESS', payload: res.data})
        })
        .catch(err => {
            dispatch({type: 'FETCH_FAILURE', error: "Error while loading available authors"})
        })
    }, [props.authorKey])

    function GoToBookPage(props: BookPageProps)
    {
        navigate('/book', {state: props});
    }

    return (
        <div>
            {props.authorKey && bookSearchResult && 
            booksLoadingState.isLoading ? <Loading /> : booksLoadingState.error ? <div>Error occured while loading book table</div> :
                <div className="flex flex-wrap max-w-full justify-center space-x-3 md:justify-start mt-10">
                {bookSearchResult?.docs.map((book, id) => (
                    <div key={id} onClick={() => GoToBookPage({title: book.title, workId: book.key, coverId: book.cover_i, authorName: book.author_name[0],
                    authorKey: book.author_key[0]})} >
                        {book.cover_i &&
                            <BookCardWithImageLoading title={book.title} 
                            imageURL={`${OpenLibraryCoversUlr}/b/id/${book.cover_i}-L.jpg?default=false`} />
                        }
                    </div>
                ))}
                </div>
            }
        </div>
    );
}