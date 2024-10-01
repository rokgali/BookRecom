import { useEffect, useState } from "react"

interface SavedSuccesfullyProps {
    message: string,
    duration: number,
    onComplete(): void
}

export default function SavedSuccesfully(props: SavedSuccesfullyProps)
{
    useEffect(() => {
        setTimeout(() => {
            props.onComplete();

        }, props.duration);
    }, [props.onComplete])

    return (<>
            <div className="fixed top-4 right-4 bg-green-500 
            rounded-lg text-white px-4 py-2 shadow-lg">
                {props.message}
            </div>
        </>);
    
}