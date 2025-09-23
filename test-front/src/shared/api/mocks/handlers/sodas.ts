import { HttpResponse } from "msw";
import { http } from "../http";
import type {ApiSchemas} from "../../schema";

const sodas: ApiSchemas["Soda"][] = [
    {
        id: "1",
        name: "Напиток газированный Cola",
        price: 105,
        count: 100,
        imgPath: "https://png.klev.club/uploads/posts/2024-05/png-klev-club-nkua-p-zhestyanaya-banka-png-25.png",
        brandId: "1"
    },
    {
        id: "2",
        name: "Напиток газированный Sprite",
        price: 83,
        count: 200,
        imgPath: "https://png.klev.club/uploads/posts/2024-04/png-klev-club-r90m-p-banka-zheleznaya-png-12.png",
        brandId: "2"
    },
    {
        id: "3",
        name: "Напиток газированный Fanta",
        price: 98,
        count: 100,
        imgPath: "https://png.klev.club/uploads/posts/2024-04/png-klev-club-r90m-p-banka-zheleznaya-png-12.png",
        brandId: "3"
    },
    {
        id: "4",
        name: "Напиток газированный Dr.Pepper",
        price: 110,
        count: 0,
        imgPath: "https://png.klev.club/uploads/posts/2024-04/png-klev-club-r90m-p-banka-zheleznaya-png-12.png",
        brandId: "4"
    },
    {
        id: "5",
        name: "Напиток газированный Dr.Pepper",
        price: 110,
        count: 23,
        imgPath: "https://png.klev.club/uploads/posts/2024-04/png-klev-club-r90m-p-banka-zheleznaya-png-12.png",
        brandId: "1"
    },
    {
        id: "6",
        name: "Напиток газированный Dr.Pepper",
        price: 110,
        count: 517,
        imgPath: "https://png.klev.club/uploads/posts/2024-04/png-klev-club-r90m-p-banka-zheleznaya-png-12.png",
        brandId: "2"
    },
    {
        id: "7",
        name: "Напиток газированный Dr.Pepper",
        price: 110,
        count: 203,
        imgPath: "https://png.klev.club/uploads/posts/2024-04/png-klev-club-r90m-p-banka-zheleznaya-png-12.png",
        brandId: "1"
    },
    {
        id: "8",
        name: "Напиток газированный Dr.Pepper",
        price: 110,
        count: 3,
        imgPath: "https://png.klev.club/uploads/posts/2024-04/png-klev-club-r90m-p-banka-zheleznaya-png-12.png",
        brandId: "2"
    },
    {
        id: "9",
        name: "Напиток газированный Dr.Pepper",
        price: 110,
        count: 7,
        imgPath: "https://png.klev.club/uploads/posts/2024-04/png-klev-club-r90m-p-banka-zheleznaya-png-12.png",
        brandId: "3"
    },
    {
        id: "10",
        name: "Напиток газированный Dr.Pepper",
        price: 110,
        count: 0,
        imgPath: "https://png.klev.club/uploads/posts/2024-04/png-klev-club-r90m-p-banka-zheleznaya-png-12.png",
        brandId: "3"
    },
];

export const sodaHandlers = [
    http.get("/sodas", () => {
        return HttpResponse.json(sodas);
    }),
];