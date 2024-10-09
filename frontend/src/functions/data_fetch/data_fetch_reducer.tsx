interface State<T> {
    isLoading: boolean,
    data: T | null,
    error: string | null
}

type Action<T> =
  | { type: 'FETCH_INIT' }
  | { type: 'FETCH_SUCCESS'; payload: T }
  | { type: 'FETCH_FAILURE'; error: string };

export const initialReducerState = {
    isLoading: true,
    data: null,
    error: null
}

export default function DataFetchReducer<T> (state: State<T>, action: Action<T>): State<T>
{
    switch(action.type) {
        case 'FETCH_INIT': return {
            ...state,
            isLoading: true,
            error: null
        };
        case 'FETCH_SUCCESS': return {
            isLoading: false,
            data: action.payload,
            error: null
        };
        case 'FETCH_FAILURE': return {
            ...state,
            isLoading: false,
            error: action.error
        };
        default:
            throw new Error("Data fetch reducer failed");
    }
}