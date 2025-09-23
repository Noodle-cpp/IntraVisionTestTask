import { HttpResponse } from "msw";
import { http } from "../http";
import type {ApiSchemas} from "../../schema";

const carts: ApiSchemas["Cart"][] = [
    {
        id: "1",
        sodaId: "1",
        sodaName: "Cola",
        brandId: "1",
        brandName: "Cola",
        count: 1,
        price: 68,
        createdAt: "2025-12-25T10:30:00Z",
    },
    {
        id: "1",
        sodaId: "2",
        sodaName: "Sprite",
        brandId: "2",
        brandName: "Sprite",
        count: 3,
        price: 48,
        createdAt: "2025-12-25T10:30:00Z",
    },
];

export const cartHandlers = [
    http.get("/carts", () => {
        return HttpResponse.json(carts);
    }),
];