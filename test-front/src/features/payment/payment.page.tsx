import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from "@/shared/ui/kit/table.tsx";
import {NumberInput} from "@/shared/ui/number-input/number-input";
import {Separator} from "@/shared/ui/kit/separator.tsx";
import {Button} from "@/shared/ui/kit/button.tsx";
import {useNavigate} from "react-router-dom";
import {useGetCoins} from "@/features/payment/model/use-get-coin.ts";
import {useCoinEdit} from "@/features/payment/model/use-coin-edit.ts";
import {useGetCarts} from "@/features/payment/model/use-get-cart.ts";
import {cn} from "@/shared/lib/css.ts";
import {useBuy} from "@/features/payment/model/use-buy.ts";
import type {ApiSchemas} from "@/shared/api/schema";
import {ROUTES} from "@/shared/model/routes.ts";

export function PaymentPage() {
    const coinsQuery = useGetCoins();
    const coinEdit = useCoinEdit();
    const cartQuery = useGetCarts();

    const navigate = useNavigate();
    const handleGoBack = () => {
        navigate(-1)
    }
    const buy = useBuy()

    const handlePayment = async () => {

        const totalAmount = coinEdit.calculateTotalAmount(coinsQuery.coins);
        if (totalAmount < cartQuery.totalPrice) return;

        const changeAmount = totalAmount - cartQuery.totalPrice;
        if (changeAmount === 0) return;

        const paymentData: ApiSchemas["PaymentRequest"] = {
            coins: coinsQuery.coins.map(coin => ({
                id: coin.id,
                count: parseInt(coinEdit.getInputCount(coin.id) || "0")
            })).filter(coin => coin.count > 0)
        };

        try {
            await buy.buy(paymentData);
            console.log('Покупка совершена успешно');
        } catch {
            navigate(ROUTES.PAYMENT_EXCEPTION, {
                state: {
                    message: "Автомат не может выдать сдачу"
                }
            });
            console.log('Автомат не может выдать сдачу');
            return;
        }

        console.log(buy.changeCoins)
        // navigate(ROUTES.CHECK, {
        //     state: {
        //         changeCoins: buy.changeCoins,
        //     }
        // });
    }

    return(
        <div className="flex flex-wrap p-10">
            <div className="flex flex-col w-full gap-10">
                <span className="font-semibold text-3xl">
                    Оплата
                </span>
                <div className="flex flex-col w-full">
                        <Table>
                            <TableHeader>
                                <TableRow>
                                    <TableHead className="w-[45%] p-5 text-lg">Номинал</TableHead>
                                    <TableHead className="w-[25%] p-5 text-lg">Количество</TableHead>
                                    <TableHead className="w-[20%] p-5 text-lg">Сумма</TableHead>
                                </TableRow>
                            </TableHeader>
                            <TableBody>
                                {coinsQuery.coins.map((coin) => (
                                    <TableRow key={coin.id} className="border-0 hover:bg-transparent">
                                        <TableCell>
                                            <div className="flex items-center gap-10">
                                                <div className="rounded-full bg-gray-100 border-l border-gray-300
                                                                text-black flex items-center justify-center
                                                                w-15 h-15 text-xl">
                                                  <span className="font-medium">
                                                    {coin.banknote}
                                                  </span>
                                                </div>
                                                <span className="text-xl">{coin.banknote} руб.</span>
                                            </div>
                                        </TableCell>

                                        <TableCell>
                                            <div className="flex items-center gap-2">
                                                <NumberInput
                                                    value={coinEdit.getInputCount(coin.id) || "0"}
                                                    onChange={(value) => {
                                                        coinEdit.setInputCount(coin.id, value);
                                                    }}
                                                    min={0}
                                                    allowDecimals={false}
                                                />
                                            </div>
                                        </TableCell>

                                        <TableCell>
                                           <span className="font-semibold text-2xl">
                                                {coin.banknote * parseInt(coinEdit.getInputCount(coin.id) || "0")} руб.
                                            </span>
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                </div>

                <Separator/>

                <div className="flex flex-col w-full gap-10">
                    <div className="flex flex-row items-center justify-end gap-5">
                        <span className="text-2xl">Итоговая сумма</span>
                        <span className="text-4xl font-bold">{cartQuery.totalPrice}</span>
                        <span className="text-2xl">Вы внесли</span>
                        <span className={cn("text-4xl font-bold", coinEdit.calculateTotalAmount(coinsQuery.coins) >= cartQuery.totalPrice ? "text-green-500" : "text-red-500")}>
                            {coinEdit.calculateTotalAmount(coinsQuery.coins)}
                        </span>
                    </div>
                    <div className="flex flex-row justify-between">
                        <Button className="bg-yellow-400 hover:bg-yellow-600 text-black text-xl rounded-none h-18 w-[20%]"
                                onClick={handleGoBack}>
                            Вернуться
                        </Button>

                        <Button className="bg-green-600 hover:bg-green-700 text-white text-xl rounded-none h-18 w-[20%]"
                                onClick={handlePayment}>
                            Оплатить
                        </Button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export const Component = PaymentPage;
