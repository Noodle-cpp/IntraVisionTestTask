import {keepPreviousData, useQuery} from "@tanstack/react-query";
import {rqClient} from "@/shared/api/instance.ts";
import {type RefCallback, useCallback} from "react";

type UseSodaListParams = {
    brandId?: string,
    minPriceValue: number,
    maxPriceValue: number,
    perPage?: number
}

export function useSodaList({
    brandId,
    minPriceValue,
    maxPriceValue,
    perPage = 10}: UseSodaListParams) {

    const { fetchNextPage, data, isFetchingNextPage, isPending, hasNextPage } =
        rqClient.useInfiniteQuery(
            "get",
            "/Sodas",
            {
                params: {
                    query: {
                        page: 1,
                        perPage,
                        minPrice: minPriceValue,
                        maxPrice: maxPriceValue,
                        brandId: brandId ?? null    // используем переданное значение
                    },
                },
            },
            {
                initialPageParam: 1,
                pageParamName: "page",
                getNextPageParam: (lastPage: { totalPages: number },
                                   _: unknown[],
                                   lastPageParams: number) =>
                    Number(lastPageParams) < lastPage.totalPages
                        ? Number(lastPageParams) + 1
                        : null,
                placeholderData: keepPreviousData
            }
        );

    const cursorRef: RefCallback<HTMLDivElement> = useCallback(
        (el) => {
            const observer = new IntersectionObserver(
                (entries) => {
                    if (entries[0].isIntersecting) {
                        fetchNextPage();
                    }
                },
                { threshold: 0.5 },
            );

            if (el) {
                observer.observe(el);

                return () => {
                    observer.disconnect();
                };
            }
        },
        [fetchNextPage],
    );
    const sodas = data?.pages.flatMap((page) => page.list) ?? [];

    const priceQuery = useQuery(rqClient.queryOptions("get", "/Sodas/price/range", {
    }, {refetchOnWindowFocus: false}));

    const minPrice = priceQuery.data?.minPrice??0
    const maxPrice = priceQuery.data?.maxPrice??0

    return{
        sodas,
        minPrice,
        maxPrice,
        isFetchingNextPage,
        fetchNextPage,
        isPending: isPending || priceQuery.isPending,
        hasNextPage,
        cursorRef,
    }
}