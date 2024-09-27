import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { BookPageProps } from "../interfaces/bookpageprops";
import TakeawaysResponse, { Takeaway } from "../interfaces/takeawaysResponse";
import axios from 'axios';

export default function BookPage()
{
    const location = useLocation();
    const pageProps: BookPageProps = location.state || {};

    const [bookDescriptionAndTakeawaysLoading, setBookDescriptionAndTakeawaysLoading] = useState<boolean>(true);
    const [bookDescription, setBookDescription] = useState<string>('');
    const [bookTakeaways, setBookTakeaways] = useState<TakeawaysResponse>();

    const [bookDescriptionError, setBookDescriptionError] = useState<string>(); 
    const [bookTakeawaysError, setBookTakeawaysError] = useState<string>();

    useEffect(() => {
        const fetchBookDescriptionAndTakeaways = async () => {
                const getBookDescriptionRequest = axios.get(`http://localhost:5103/api/Book/GetBookDescription?workId=${pageProps.workId}&title=${pageProps.title}&authorName=${pageProps.authorName}`)
                                                       .then(res => setBookDescription(res.data))
                                                       .catch(err => setBookDescriptionError("Book description failed to load"));

                const getBookTakeawaysRequest = axios.get(`http://localhost:5103/api/Book/GetBookTakeaways?workId=${pageProps.workId}&title=${pageProps.title}&authorName=${pageProps.authorName}`)
                                                     .then(res => { setBookTakeaways(res.data); console.log(res.data) })
                                                     .catch(err => setBookTakeawaysError("Book takeaways failed to load"));

            try {
                await Promise.all([getBookDescriptionRequest, getBookTakeawaysRequest]);

                setBookDescriptionAndTakeawaysLoading(false);
            } catch (err) {

            }
        }

        fetchBookDescriptionAndTakeaways();

    }, [])

    return (
        <>
            {bookDescriptionError && <div>Something went wrong while fetching book description</div>}
            {bookTakeawaysError && <div>Something went wrong while fetching book takeaways</div>}
            {!bookDescriptionAndTakeawaysLoading && 
                <div>
                    <div>
                        {pageProps.title} {pageProps.authorName}
                    </div>
                    <div className="font-bold">
                        {bookDescription}
                    </div>
                    <ul>
                        {bookTakeaways && bookTakeaways?.takeaways.map((takeaway, index) => (
                            <li key={index}>
                                <div>takeaway number: {index}</div>

                                <div>Name: {takeaway.name}</div>
                                <div>Lesson: {takeaway.lesson}</div>
                                <div>Episode: {takeaway.episode}</div>
                            </li>
                        ))}
                    </ul>
                </div>
            }
        </>
    );
}