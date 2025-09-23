import { setupWorker } from "msw/browser";
import { sodaHandlers } from "./handlers/sodas";
import { brandHandlers } from "./handlers/brands";
import { cartHandlers } from "./handlers/carts";

export const worker = setupWorker(...sodaHandlers, ...brandHandlers, ...cartHandlers);