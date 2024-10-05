import axios from "axios";
import { useEffect, useState } from "react";
import { BookrecomAPIUrl } from "../components/globals/ulrs";


export default function CachedBooks()
{
    const[offset, setOffset] = useState<number>(1);
    const pageSize = 20;

    useEffect(() => {
        axios.get(`${BookrecomAPIUrl}/Book/GetCachedBooks?offset=${offset}&pageSize=${pageSize}`)
        .then(res => console.log(res.data))
        .catch(err => console.error(err));
    }, [])

    return (<div>Cached books </div>);
}