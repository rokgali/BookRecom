import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { BookPageProps } from "../interfaces/bookpageprops";
import TakeawaysResponse, { Takeaway } from "../interfaces/takeawaysResponse";
import axios from 'axios';
import Loading from "../components/loading";
import { BookrecomAPIUrl } from "../globals/ulrs";
import { PostBook } from "../interfaces/book";

export default function BookPage()
{
    const location = useLocation();
    const pageProps: BookPageProps = location.state || {};

    const [bookDescriptionAndTakeawaysLoading, setBookDescriptionAndTakeawaysLoading] = useState<boolean>(true);
    const [bookDescription, setBookDescription] = useState<string>('');
    const [bookTakeaways, setBookTakeaways] = useState<Takeaway[]>([]);
    const [saveBookToDbFlag, setSaveToDbFlag] = useState<boolean>(false);

    const [bookDescriptionError, setBookDescriptionError] = useState<string>(); 
    const [bookTakeawaysError, setBookTakeawaysError] = useState<string>();
    const [failedToLoad, setFailedToLoad] = useState<boolean>(false);

    useEffect(() => {
        const FetchBookDescriptionAndTakeaways = async () => {
                const getBookDescriptionRequest = axios.get(`${BookrecomAPIUrl}/Book/GetBookDescription?workId=${pageProps.workId}&title=${pageProps.title}&authorName=${pageProps.authorName}`)
                                                       .then(res => setBookDescription(res.data))
                                                       .catch(err => {setBookDescriptionError("Book description failed to load"); setFailedToLoad(true);});

                const getBookTakeawaysRequest = axios.get(`${BookrecomAPIUrl}/Book/GetBookTakeaways?numberOfTakeaways=5&workId=${pageProps.workId}&title=${pageProps.title}&authorName=${pageProps.authorName}`)
                                                     .then(res => { setBookTakeaways(res.data);})
                                                     .catch(err => {setBookTakeawaysError("Book takeaways failed to load"); setFailedToLoad(true);});

            try {
                await Promise.all([getBookDescriptionRequest, getBookTakeawaysRequest]);

                setBookDescriptionAndTakeawaysLoading(false);
                setSaveToDbFlag(true);
            } catch (err) {

            }
        }

        FetchBookDescriptionAndTakeaways();
    }, [])

    useEffect(() => {
        if(!saveBookToDbFlag)
            return; 

        const bookToSave:PostBook = {title:pageProps.title, workId:pageProps.workId, coverId:pageProps.coverId,
                                     authorKey: pageProps.authorKey, takeawaysHeading:'',
                                     description:bookDescription, takeaways:bookTakeaways};
        SaveBookToDb(bookToSave);

    }, [saveBookToDbFlag])

    function SaveBookToDb(bookToSave: PostBook)
    {
        axios.post(`${BookrecomAPIUrl}/Book/CreateBook`, bookToSave)
        .then(res => {console.log(res)})
        .catch(err => {console.log(err)}); 
    }

    return (
        <>
            {failedToLoad ? <div>Failed to load</div> 
            
            :

            !bookDescriptionAndTakeawaysLoading ? 
                <div>
                    <h1 className="text-bold text-center text-lg mx-auto w-auto my-6">
                        <div>
                            {pageProps.title}
                        </div>
                        <div>
                            {pageProps.authorName}
                        </div>
                    </h1>
                    <div className="p-4 mx-auto w-3/4 h-auto rounded-lg bg-blue-200">
                        <div className="">
                            {bookDescription}
                        </div>
                        <div className="mt-4">
                            <ul className="space-y-4">
                                {bookTakeaways && bookTakeaways.map((takeaway, index) => (
                                    <li key={index}>
                                        <div>takeaway number: {index + 1}</div>

                                        <div>Name: {takeaway.name}</div>
                                        <div>Lesson: {takeaway.lesson}</div>
                                        <div>Episode: {takeaway.episode}</div>
                                    </li>
                                ))}
                            </ul>
                        </div>
                    </div>
                </div>
                : <Loading />
            }
        </>
    );
}