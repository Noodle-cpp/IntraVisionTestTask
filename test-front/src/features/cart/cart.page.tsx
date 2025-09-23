import {CONFIG} from "@/shared/model/config.ts";
import {useGetCarts} from "@/features/cart/model/use-get-cart"
import {MinusIcon, PlusIcon, Trash2Icon} from "lucide-react";
import {Separator} from "@/shared/ui/kit/separator";
import {Button} from "@/shared/ui/kit/button";
import {Input} from "@/shared/ui/kit/input";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/shared/ui/kit/table";
import {useNavigate} from "react-router-dom";

export function CartPage() {
    const cartQuery = useGetCarts();
    const totalAmount = cartQuery.carts.reduce((sum, item) => sum + (item.soda.price * item.count), 0);
    const navigate = useNavigate();
    const handleGoBack = () => {
        navigate(-1)
    }

    return (
        <div className="flex flex-wrap p-10">
            <div className="flex flex-col w-full">
                <div className="flex flex-col w-full">
                    <Table>
                        <TableHeader>
                            <TableRow>
                                <TableHead className="w-[45%] p-5 text-lg">Товар</TableHead>
                                <TableHead className="w-[25%] p-5 text-lg">Количество</TableHead>
                                <TableHead className="w-[20%] p-5 text-lg">Цена</TableHead>
                                <TableHead className="w-[1%] p-5"></TableHead>
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {cartQuery.carts.map((cart) => (
                                <TableRow key={cart.id} className="border-0 hover:bg-transparent">
                                    <TableCell>
                                        <div className="flex items-center gap-4">
                                            <img
                                                src={`${CONFIG.API_BASE_URL}/sodas/${cart.sodaId}/img`}
                                                loading="lazy"
                                                className="w-50 h-50 object-contain"
                                                alt={cart.sodaName}
                                            />
                                            <span className="text-xl">{cart.sodaName}</span>
                                        </div>
                                    </TableCell>

                                    <TableCell>
                                        <div className="flex items-center gap-2">
                                            <Button
                                                variant="secondary"
                                                size="icon"
                                                className="size-8 bg-black text-white hover:bg-gray-800"
                                            >
                                                <MinusIcon className="h-4 w-4"/>
                                            </Button>
                                            <Input
                                                value={cart.count}
                                                className="w-16 text-center"
                                                readOnly
                                            />
                                            <Button
                                                variant="secondary"
                                                size="icon"
                                                className="size-8 bg-black text-white hover:bg-gray-800"
                                            >
                                                <PlusIcon className="h-4 w-4"/>
                                            </Button>
                                        </div>
                                    </TableCell>

                                    <TableCell>
                                       <span className="font-semibold text-2xl">
                                            {cart.soda.price * cart.count} руб.
                                        </span>
                                    </TableCell>

                                    <TableCell className="text-right">
                                        <Trash2Icon className="h-10 w-10"/>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </div>

                <Separator/>

                <div className="flex flex-col w-full gap-10 pt-10">
                    <div className="flex flex-row items-center justify-end gap-5">
                        <span className="text-2xl">Итоговая сумма</span>
                        <span className="text-4xl font-bold">{totalAmount}</span>
                    </div>
                    <div className="flex flex-row justify-between">
                        <Button className="bg-yellow-400 hover:bg-yellow-600 text-black text-xl rounded-none h-18 w-[20%]"
                                onClick={handleGoBack}>
                            Вернуться
                        </Button>

                        <Button className="bg-green-600 hover:bg-green-700 text-white text-xl rounded-none h-18 w-[20%]">
                            Оплата
                        </Button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export const Component = CartPage;