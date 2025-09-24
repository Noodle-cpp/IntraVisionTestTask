import {rqClient} from "@/shared/api/instance.ts";
import {useQuery} from "@tanstack/react-query";

export function useGetCarts() {
    const cartsQuery = useQuery(rqClient.queryOptions("get", "/Cart", {
    }, {refetchOnWindowFocus: false}));

    const totalPrice = cartsQuery.data?.totalPrice || 0;

    return {
        totalPrice
    }
}