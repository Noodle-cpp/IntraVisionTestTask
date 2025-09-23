import {rqClient} from "@/shared/api/instance.ts";
import {useQuery} from "@tanstack/react-query";

export function useGetCarts() {
    const cartsQuery = useQuery(rqClient.queryOptions("get", "/Cart", {
    }, {refetchOnWindowFocus: false}));

    const carts = cartsQuery.data?.carts ?? [];
    const purchaseCount: number = cartsQuery.data?.count ?? 0;

    const isProductInCart = (sodaId: string): boolean => {
        return carts.some((cart) => cart.sodaId === sodaId);
    };

    return {
        carts,
        isProductInCart,
        purchaseCount,
    }
}