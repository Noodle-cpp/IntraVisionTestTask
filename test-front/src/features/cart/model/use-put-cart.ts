import { rqClient } from "@/shared/api/instance";
import {queryClient} from "@/shared/api/query-client.ts";
import type {ApiSchemas} from "@/shared/api/schema";

export function useUpdateCart() {

    const updateCartMutation = rqClient.useMutation("put", "/Cart/{cartId}", {
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["get", "/Cart"]
            });
        },
        onError: (error) => {
            console.error("Ошибка при добавлении в корзину:", error);
        },
    });

    return {
        isPending: updateCartMutation.isPending,
        updateCart: (cartId: string, updateData: ApiSchemas["UpdateCartRequest"]) =>
            updateCartMutation.mutate({
                params: {
                    path: { cartId },
                },
                body: updateData
            }),
    };
}