import { Book } from "../interfaces/book";
import Modal from 'react-modal'


interface BookModalProps {
    selectedBook: Book
    isOpen: boolean
    toggleSelectedBookModal(book: Book): void 
}

export default function BookModal(props: BookModalProps)
{
    return (
        <Modal isOpen={props.isOpen}>
            <div>
                <div>
                    {props.selectedBook.title}
                </div>
                <div>
                    {props.selectedBook.key}
                </div>
                <div>
                    {props.selectedBook.cover_i}
                </div>
                <div>
                    Author name:
                    {props.selectedBook.author_name && props.selectedBook.author_name.at(0)}
                </div>
                <div>
                    Author key:
                    {props.selectedBook.author_key && props.selectedBook.author_key.at(0)}
                </div>
                <div>
                    {props.selectedBook.description && props.selectedBook.description.value}
                </div>
            </div>
            <div>
                <button onClick={() => 
                    props.toggleSelectedBookModal(props.selectedBook)}>Close</button>
            </div>
        </Modal>
        );
}