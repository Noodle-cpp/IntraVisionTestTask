import { HttpResponse } from "msw";
import { http } from "../http";
import type {ApiSchemas} from "../../schema";

const brands: ApiSchemas["Brand"][] = [
    {
        id: "1",
        name: "Cola",
    },
    {
        id: "2",
        name: "Sprite",
    },
    {
        id: "3",
        name: "Fanta",
    },
    {
        id: "4",
        name: "Dr.Pepper",
    },
];

export const brandHandlers = [
    http.get("/brands", () => {
        return HttpResponse.json(brands);
    }),
];