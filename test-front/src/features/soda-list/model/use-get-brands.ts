import {useQuery} from "@tanstack/react-query";
import {rqClient} from "@/shared/api/instance.ts";

export function useGetBrands() {
    const brandQuery = useQuery(rqClient.queryOptions("get", "/Brands", {
    }, { refetchOnWindowFocus: false }));

    const brands = brandQuery.data ?? [];

    return {
        brands
    }
}

