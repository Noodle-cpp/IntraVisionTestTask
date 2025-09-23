import { useReducer, useCallback } from 'react';
type FilterState = {
    minPrice: number;
    maxPrice: number;
    minPriceValue: number;
    maxPriceValue: number;
    brandId?: string;
    open: boolean;
};

type FilterAction =
    | { type: 'SET_PRICE_RANGE'; payload: { min: number; max: number } }
    | { type: 'SET_PRICE_VALUES'; payload: { min: number; max: number } }
    | { type: 'SET_BRAND_ID'; payload: string }
    | { type: 'SET_OPEN'; payload: boolean }
    | { type: 'UPDATE_FROM_API'; payload: { minPrice: number; maxPrice: number } };

function filterReducer(state: FilterState, action: FilterAction): FilterState {
    switch (action.type) {
        case 'SET_PRICE_RANGE':
            return {
                ...state,
                minPrice: action.payload.min,
                maxPrice: action.payload.max,
            };

        case 'SET_PRICE_VALUES':
            return {
                ...state,
                minPriceValue: action.payload.min,
                maxPriceValue: action.payload.max,
            };

        case 'SET_BRAND_ID':
            return {
                ...state,
                brandId: action.payload,
            };

        case 'SET_OPEN':
            return {
                ...state,
                open: action.payload,
            };

        case 'UPDATE_FROM_API':
            return {
                ...state,
                minPrice: action.payload.minPrice,
                maxPrice: action.payload.maxPrice,
                minPriceValue: action.payload.minPrice,
                maxPriceValue: action.payload.maxPrice,
            };

        default:
            return state;
    }
}

const initialState: FilterState = {
    minPrice: 0,
    maxPrice: 0,
    minPriceValue: 0,
    maxPriceValue: 0,
    brandId: undefined,
    open: false
};

export function useFilter() {
    const [state, dispatch] = useReducer(filterReducer, initialState);

    const setPriceRange = useCallback((min: number, max: number) => {
        dispatch({ type: 'SET_PRICE_RANGE', payload: { min, max } });
    }, []);

    const setPriceValues = useCallback((min: number, max: number) => {
        dispatch({ type: 'SET_PRICE_VALUES', payload: { min, max } });
    }, []);

    const setBrandId = useCallback((brandId: string) => {
        dispatch({ type: 'SET_BRAND_ID', payload: brandId });
    }, []);

    const setOpen = useCallback((open: boolean) => {
        dispatch({ type: 'SET_OPEN', payload: open });
    }, []);

    const updateFromApi = useCallback((minPrice: number, maxPrice: number) => {
        dispatch({ type: 'UPDATE_FROM_API', payload: { minPrice, maxPrice } });
    }, []);

    return {
        ...state,
        setPriceRange,
        setPriceValues,
        setBrandId,
        updateFromApi,
        setOpen
    };
}