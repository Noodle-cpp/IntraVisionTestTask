import {useState} from "react";

export function useCartEdit() {
    const [inputCounts, setInputCounts] = useState<Record<string, string>>({});

    const setInputCount = (cartId: string, value: string) => {
        setInputCounts(prev => ({
            ...prev,
            [cartId]: value
        }));
    };

    const getInputCount = (cartId: string) => {
        return inputCounts[cartId] || "";
    };

    return {
        inputCounts,
        setInputCount,
        getInputCount,
    }
}