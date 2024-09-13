import { Book } from "../interfaces/book";

interface SelectedBookProps {
    selectedBooks: Book[]
}

export default function SelectedBooksWidget(props: SelectedBookProps)
{
    return (
        <div className="border w-24 h-14 rounded-lg bg-red-200 
                    hover:bg-red-300 transition duration-200 p-2
                    hover:h-auto hover:w-auto max-h-40 overflow-y-auto
                    group">
                {props.selectedBooks.map((book, id) => (
                        <div key={id} className="mt-2 hover:bg-red-400 bg-red-200 border rounded-lg opacity-0 group-hover:opacity-100">
                            {book.title}
                        </div>
                    ))}
        </div>
    );
}