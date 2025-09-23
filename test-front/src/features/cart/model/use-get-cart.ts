import {rqClient} from "@/shared/api/instance.ts";
import {useQuery} from "@tanstack/react-query";

export function useGetCarts() {
    const cartsQuery = useQuery(rqClient.queryOptions("get", "/Cart", {
    }, {refetchOnWindowFocus: false}));

    const carts = cartsQuery.data?.carts ?? [];

    return {
        carts,
    }
}