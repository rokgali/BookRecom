import { Book_card_props } from "../interfaces/bookcardprops";

export default function BookCard(props: Book_card_props)
{
    return (<>
        <div className="border bg-lime-50 text-center shadown-lg mx-2 
                        mb-4 rounded-lg hover:bg-lime-100
                        hover:scale-105 transition duration-200 w-40 h-72 overflow-y-hidden">
            <div className="m-4 w-26 h-48 bg-white flex items-bottom justify-center overflow-hidden">
                <img src={props.imageURL} ></img>
            </div>
            <div className="font-bold break-words w-full whitespace-break-spaces">
                <p>{props.title}</p>
            </div>
        </div>
    </>);
}