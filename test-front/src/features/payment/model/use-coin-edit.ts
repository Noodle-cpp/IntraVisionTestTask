import {useState} from "react";
import type {ApiSchemas} from "@/shared/api/schema";

export function useCoinEdit() {
    const [inputCounts, setInputCounts] = useState<Record<string, string>>({});

    const setInputCount = (coinId: string, value: string) => {
        setInputCounts(prev => ({
            ...prev,
            [coinId]: value
        }));
    };

    const getInputCount = (coinId: string) => {
        return inputCounts[coinId] || "0";
    };

    const calculateTotalAmount = (coins: Array<ApiSchemas["CoinResponse"]>) => {
        return coins.reduce((total, coin) => {
            const count = parseInt(getInputCount(coin.id));
            return total + (coin.banknote * count);
        }, 0);
    };

    const getCoinAmount = (coinId: string, banknote: number) => {
        const count = parseInt(getInputCount(coinId));
        return banknote * count;
    };

    return {
        inputCounts,
        setInputCount,
        getInputCount,
        calculateTotalAmount,
        getCoinAmount,
    }
}