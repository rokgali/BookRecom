import { useEffect, useState } from "react"
import { OpenLibrarySearchResult } from "../interfaces/openlibrarySearchResult";
import axios from 'axios'
import BookCardWithImageLoading from "../components/book_card_with_image_loading";
import { Book } from "../interfaces/book";
import BookModal from "../components/book_modal";

interface HomePageProps {
}

export default function HomePage(props: HomePageProps)
{
    const [searchBooks, setSearchBooks] = useState<OpenLibrarySearchResult>();

    const [selectedBookModalIsOpen, setSelectedBookModalIsOpen] = useState<boolean>(false);
    const [selectedBook, setSelectedBook] = useState<Book>();

    const [bookSearchLoading, setBookSearchLoading] = useState<boolean>(true);
    const [bookSearchErrorMessage, setBookSearchErrorMessage] = useState<string>();

    useEffect(() => {
        axios.get("https://openlibrary.org/search.json?q=language:eng&limit=20")
        .then(res => {
            const bookSearchResult: OpenLibrarySearchResult = res.data
            
            setSearchBooks(bookSearchResult);
            setBookSearchLoading(false);

            console.log(res);
        })
        .catch(err => {
            setBookSearchErrorMessage('Books failed to load, check input parameters');
            setBookSearchLoading(false);
        })
    }, [])

    // When books search is performed, the data is cached into db
    useEffect(() => {
        if(searchBooks === undefined || searchBooks.numFound === 0)
            return;

        
    }, [searchBooks])

    const toggleSelectedBookModal = (book: Book) => {
        if(!selectedBookModalIsOpen)
        {
            setSelectedBookModalIsOpen(true);
            setSelectedBook(book);

            return;
        }

        setSelectedBookModalIsOpen(false);
        setSelectedBook(undefined);
    }

    return (<>
        <div className="flex flex-wrap max-w-full justify-center space-x-3 md:justify-start">
            {bookSearchErrorMessage && <div className="text-red-500 font-bold text-lg mx-auto w-auto">{bookSearchErrorMessage}</div>}

            {bookSearchLoading && <div className="text-center text-green-400 text-lg mx-auto w-auto font-bold">Loading...</div>}
            {searchBooks?.docs.map((book, id) => (
                <div key={id} onClick={() => toggleSelectedBookModal(book)} >
                    {book.cover_i &&
                        <BookCardWithImageLoading title={book.title} 
                        imageURL={`https://covers.openlibrary.org/b/id/${book.cover_i}-L.jpg?default=false`} />
                    }
                </div>
            ))}
        </div>

        {selectedBook && <BookModal selectedBook={selectedBook} isOpen={selectedBookModalIsOpen} toggleSelectedBookModal={() => toggleSelectedBookModal(selectedBook)} />}
    </>)
}