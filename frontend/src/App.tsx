import { useState } from 'react';
import './App.css';
import BookCard from './components/book_card';
import HomePage from './pages/homepage';
import { Book } from './interfaces/book';
import SelectedBooksWidget from './components/selected_books_widget';

function App() {
  const [selectedBooks, setSelectedBooks] = useState<Book[]>([]);

  const addSelectedBookToWidget = (book: Book) => {
    setSelectedBooks(selectedBooks.concat(book));
  }

  const removeSelectedBookFromWidget = (bookId: string) => {
    const bookIndex = selectedBooks.findIndex(b => b.key == bookId);

    if(bookIndex === -1)
      return 

    setSelectedBooks(selectedBooks.filter(b => b.key !== bookId));
  }

  return (
    <>
    <div className="fixed w-full left-0 top-0 h-24 bg-blue-100 shadow-md">
      <div>Header of page</div>
    </div>
    <div className="flex mt-32">
      <div className="md:w-1/6">

      </div>
      <div className="md:w-5/6 w-full">
        <HomePage selectedBooks={selectedBooks}
                  addSelectedBookToWidget={addSelectedBookToWidget} 
                  removeSelectedBookFromWidget={removeSelectedBookFromWidget} />
      </div>
    </div>
    <div className="hidden md:block fixed bottom-0 right-0">
      <SelectedBooksWidget selectedBooks={selectedBooks} />
    </div>
    </>
  );
}

export default App;
