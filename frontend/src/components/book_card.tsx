import { Book_card_props } from "../interfaces/bookcardprops";

export default function BookCard(props: Book_card_props)
{
    return (<>
        <div className="border bg-lime-50 text-center shadown-lg mx-2 
                        mb-4 rounded-lg hover:bg-lime-100
                        hover:scale-105 transition duration-200 w-32 h-64
                        overflow-y-auto">
            <div className="m-4 w-auto h-auto">
                <img src={props.imageURL} ></img>
            </div>
            <div className="font-bold">
                <h5>{props.title}</h5>
            </div>
        </div>
    </>);
}