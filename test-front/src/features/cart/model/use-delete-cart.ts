import {rqClient} from "@/shared/api/instance.ts";
import {queryClient} from "@/shared/api/query-client.ts";

export function useDeleteCart() {
    const deleteCartMutation = rqClient.useMutation("delete", "/Cart/{cartId}", {
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["get", "/Cart"]
            });
        },
        onError: (error) => {
            console.error("Ошибка при удалении товара из корзины:", error);
        },
    });

    return {
        isPending: deleteCartMutation.isPending,
        deleteCart: (cartId: string) =>
            deleteCartMutation.mutate({
                params: {
                    path: { cartId },
                },
            }),
    };
}