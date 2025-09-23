import { rqClient } from "@/shared/api/instance";
import {queryClient} from "@/shared/api/query-client.ts";

export function useAddToCart() {

    const addToCartMutation = rqClient.useMutation("post", "/Cart/add/sodas/{sodaId}", {
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
        isPending: addToCartMutation.isPending,
        addToCart: (sodaId: string) =>
            addToCartMutation.mutate({
                params: {
                    path: { sodaId }
                }
            }),
    };
}