import {rqClient} from "@/shared/api/instance.ts";
import type {ApiSchemas} from "@/shared/api/schema";

export function useBuy() {

    const buyMutation = rqClient.useMutation("put", "/Cart/buy", {
    });

    return {
        isPending: buyMutation.isPending,
        buy: async (coins: ApiSchemas["PaymentRequest"]) =>
            buyMutation.mutate({
                body: coins
            }),
        changeCoins: buyMutation.data || [],
    }
}