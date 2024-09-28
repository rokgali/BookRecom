import { useEffect, useState } from "react"
import { OpenLibrarySearchResult } from "../interfaces/openlibrarySearchResult";
import axios from 'axios'
import BookCardWithImageLoading from "../components/book_card_with_image_loading";
import { Book } from "../interfaces/book";
import { useNavigate } from "react-router-dom";
import { BookPageProps } from "../interfaces/bookpageprops";

interface HomePageProps {
}

interface Author {
    id: number,
    name: string
}

export default function HomePage(props: HomePageProps)
{
    const [searchBooks, setSearchBooks] = useState<OpenLibrarySearchResult>();
    const [availableAuthors, setAvailableAuthors] = useState<string[]>([]);
    const [filteredAvailableAuthors, setFilteredAvailableAuthors] = useState<string[]>(availableAuthors);

    const [authorName, setAuthorName] = useState<string>(() => {
        return localStorage.getItem('authorName') || 'Fyodor Dostoyevsky';
    });

    const [bookSearchLoading, setBookSearchLoading] = useState<boolean>(true);
    const [bookSearchErrorMessage, setBookSearchErrorMessage] = useState<string>();

    const [authorInputError, setAuthorInputError] = useState<string>();

    const navigate = useNavigate();

    useEffect(() => {
        axios.get(`https://openlibrary.org/search.json?author=%22${authorName}%22`)
        .then(res => {
            const bookSearchResult: OpenLibrarySearchResult = res.data
            
            setSearchBooks(bookSearchResult);
            setBookSearchLoading(false);
        })
        .catch(err => {
            setBookSearchErrorMessage('Books failed to load, check input parameters');
            setBookSearchLoading(false);
        })
    }, [bookSearchLoading])

    useEffect(() => {
        console.log(availableAuthors);

        if(availableAuthors.length == 0)
            return; 
        
        const filtered = availableAuthors.filter(name => 
                            name.trim().toLowerCase().includes(authorName.trim().toLowerCase()));

        setFilteredAvailableAuthors(filtered);
        
    }, [authorName]);

    useEffect(() => {
        axios.get(`http://localhost:5103/api/Author/GetAvailableAuthors`)
        .then(res => {
            setAvailableAuthors(res.data.map((author: Author) => author.name))
        })
        .catch(err => {
        })
    }, [])

    function goToBookPage(props: BookPageProps)
    {
        navigate('/book', {state: props});
    }

    function reloadBookResults()
    {
        if(authorName === localStorage.getItem('authorName'))
            return;

        if(!availableAuthors.includes(authorName))
        {
            setAuthorInputError('Please select an author from the recommended list');
            setTimeout(() => {
                setAuthorInputError('');
            }, 3000);
            return;
        }

        localStorage.setItem('authorName', authorName);
        setBookSearchLoading(true);
    }

    return (<>
        <div>
            <h1>Select an author</h1>
            <div>
                <input list="availableAuthors" type="text" value={authorName} onChange={(e) => setAuthorName(e.target.value)} />
                <datalist id="availableAuthors">
                    {filteredAvailableAuthors.map((name, index) => (
                        <option key={index} value={name} ></option>
                    ))}
                </datalist>
                <button onClick={() => reloadBookResults()}>Search</button>
            </div>
            {authorInputError && <div className="text-red-500 font-bold text-lg mx-auto w-auto">{authorInputError}</div>}
        </div>

        {bookSearchErrorMessage && <div className="text-red-500 font-bold text-lg mx-auto w-auto">{bookSearchErrorMessage}</div>}

        {bookSearchLoading ? <div className="text-center text-green-400 text-lg mx-auto w-auto font-bold">Loading...</div> :
            <div className="flex flex-wrap max-w-full justify-center space-x-3 md:justify-start mt-10">
                {searchBooks && searchBooks?.docs.map((book, id) => (
                    <div key={id} onClick={() => goToBookPage({title: book.title, workId: book.key, coverId: book.cover_i, authorName: book.author_name[0],
                    authorKey: book.author_key[0]})} >
                        {book.cover_i &&
                            <BookCardWithImageLoading title={book.title} 
                            imageURL={`https://covers.openlibrary.org/b/id/${book.cover_i}-L.jpg?default=false`} />
                        }
                    </div>
                ))}
            </div>
        }
        
    </>)
}