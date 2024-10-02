import { useEffect, useState } from "react"
import { OperationProps } from "../../interfaces/operation_props";

export default function OperationSuccess(props: OperationProps)
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