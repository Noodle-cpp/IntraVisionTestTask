import {useLocation} from "react-router-dom";
import type {ApiSchemas} from "@/shared/api/schema";

export function useCheck(): {changeCoins: ApiSchemas["CoinResponse"][]} {
    const location = useLocation();
    const { changeCoins } = location.state || {};

    return {
        changeCoins: changeCoins || [],
    }
}