import { useEffect, useState } from "react"
import { Book } from "../interfaces/book";
import { OpenLibrarySearchResult } from "../interfaces/openlibrarySearchResult";
import axios from 'axios'
import BookCardWithImageLoading from "../components/book_card_with_image_loading";

interface HomePageProps {
    selectedBooks: Book[],
    addSelectedBookToWidget: (book: Book) => void,
    removeSelectedBookFromWidget: (bookId: string) => void
}

export default function HomePage(props: HomePageProps)
{
    const [searchBooks, setSearchBooks] = useState<OpenLibrarySearchResult>();

    const [bookSearchLoading, setBookSearchLoading] = useState<boolean>(true);
    const [bookSearchErrorMessage, setBookSearchErrorMessage] = useState<string>();

    const editSelectedBooks = (book: Book) => {
        const selectedBookIndex = props.selectedBooks.findIndex(b => b === book);

        selectedBookIndex === -1 ? props.addSelectedBookToWidget(book) : props.removeSelectedBookFromWidget(book.key);
    }

    useEffect(() => {
        axios.get("https://openlibrary.org/search.json?q=language:eng&limit=20")
        .then(res => {
            const bookSearchResult: OpenLibrarySearchResult = res.data
            
            setSearchBooks(bookSearchResult);
            setBookSearchLoading(false);
        })
        .catch(err => {
            setBookSearchErrorMessage('Books failed to load, check input parameters');
            setBookSearchLoading(false);
        })
    }, [])

    return (<>
        <div className="flex flex-wrap max-w-full justify-center space-x-3 md:justify-start">
            {bookSearchErrorMessage && <div className="text-red-500 font-bold text-lg mx-auto w-auto">{bookSearchErrorMessage}</div>}

            {bookSearchLoading && <div className="text-center text-green-400 text-lg mx-auto w-auto font-bold">Loading...</div>}
            {searchBooks?.docs.map((book, id) => (
                <div key={id} onClick={() => editSelectedBooks(book)}>
                    {book.cover_i &&
                        <BookCardWithImageLoading title={book.title} 
                        imageURL={`https://covers.openlibrary.org/b/id/${book.cover_i}-L.jpg?default=false`} />
                    }
                </div>
            ))}
        </div>
    </>)
}