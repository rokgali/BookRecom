import { useEffect, useState } from "react";
import BookCard from "./book_card";
import { Book_card_props } from "../interfaces/bookcardprops";

export default function BookCardWithImageLoading(props: Book_card_props)
{
    const [imageLoaded, setImageLoaded] = useState<boolean>(false);
    const [failedToLoadImageError, setFailedToLoadImageError] = useState<string>();
    const imageURL = props.imageURL

    useEffect(() => {
        const img = new Image();
        img.src = imageURL;
        img.onload = () => setImageLoaded(true);
        img.onerror = () => setFailedToLoadImageError("Failed to load book image");
    }, [imageURL])

    return (<>
        {failedToLoadImageError && 
            <div className="text-red-500 font-bold text-lg mx-auto w-auto">{failedToLoadImageError}</div>
        }
        {imageLoaded && (
                <BookCard title={props.title} imageURL={props.imageURL} />
        )}
    </>
    );
}