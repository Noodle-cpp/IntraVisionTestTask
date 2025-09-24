import {rqClient} from "@/shared/api/instance.ts";
import {useQuery} from "@tanstack/react-query";

export function useGetCoins() {
    const coinsQuery = useQuery(rqClient.queryOptions("get", "/Coins", {
    }, {refetchOnWindowFocus: false}));

    const coins = coinsQuery.data ?? [];

    return {
        coins,
    }
}