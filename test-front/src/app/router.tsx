import { ROUTES } from "../shared/model/routes";
import { createBrowserRouter, redirect } from "react-router-dom";
import { App } from "./app";
import { Providers } from "@/app/providers.tsx";

export const router = createBrowserRouter([
    {
        element: (
            <Providers>
                <App />
            </Providers>
        ),
        children: [
            {
                path: ROUTES.SODAS,
                lazy: () => import("@/features/soda-list/soda-list.page"),
            },
            {
                path: ROUTES.CART,
                lazy: () => import("@/features/cart/cart.page"),
            },
            {
                path: ROUTES.PAYMENT,
                lazy: () => import("@/features/payment/payment.page"),
            },
            {
                path: ROUTES.CHECK,
                lazy: () => import("@/features/check/check.page"),
            },
            {
                path: ROUTES.PAYMENT_EXCEPTION,
                lazy: () => import("@/features/payment-exception/payment-exception.page"),
            },
            {
                path: ROUTES.HOME,
                loader: () => redirect(ROUTES.SODAS),
            },
        ],
    },
]);