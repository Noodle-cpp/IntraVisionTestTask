import {rqClient} from "@/shared/api/instance.ts";
import type {ApiSchemas} from "@/shared/api/schema";
import {useState} from "react";

export function useBuy() {
    const [responseData, setResponseData] = useState<ApiSchemas["CoinResponse"][]>([]);
    const [isError, setIsError] = useState<boolean>(false);

    const buyMutation = rqClient.useMutation("put", "/Cart/buy", {
        onSuccess: (data) => {
            setResponseData(data);
            setIsError(false);
        },
        onError: () => {
            setIsError(true);
            setResponseData([]);
        }
    });

    const calculateTotalAmount = (coins: Array<ApiSchemas["CoinResponse"]>) => {
        return coins.reduce((total, coin) => {
            return total + (coin.banknote * coin.count);
        }, 0);
    };

    return {
        isPending: buyMutation.isPending,
        buy: (coins: ApiSchemas["PaymentRequest"]) =>
            buyMutation.mutate({
                body: coins
            }),
        responseData,
        isError,
        calculateTotalAmount
    }
}