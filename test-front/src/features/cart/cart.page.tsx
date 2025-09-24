import {CONFIG} from "@/shared/model/config.ts";
import {useGetCarts} from "@/features/cart/model/use-get-cart"
import {Trash2Icon} from "lucide-react";
import {Separator} from "@/shared/ui/kit/separator";
import {Button} from "@/shared/ui/kit/button";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/shared/ui/kit/table";
import {useNavigate} from "react-router-dom";
import type {ApiSchemas} from "@/shared/api/schema";
import {useUpdateCart} from "@/features/cart/model/use-put-cart.ts";
import {useCartEdit} from "@/features/cart/model/use-cart-edit";
import {NumberInput} from "@/features/cart/ui/number-input.tsx";
import {useDeleteCart} from "@/features/cart/model/use-delete-cart.ts";

export function CartPage() {
    const cartEdit = useCartEdit();
    const cartQuery = useGetCarts();
    const navigate = useNavigate();
    const handleGoBack = () => {
        navigate(-1)
    }

    const {updateCart} = useUpdateCart();
    const {deleteCart} = useDeleteCart();

    const updateQuanity = (cartId: string, cart: ApiSchemas["UpdateCartRequest"], inputValue: string) => {
        cartEdit.setInputCount(cartId, inputValue)
        cart.count = parseInt(inputValue) || 0
        updateCart(cartId, cart)
    }

    return (
        <div className="flex flex-wrap p-10">
            <div className="flex flex-col w-full">
                <div className="flex flex-col w-full">
                    {cartQuery.carts.length > 0 &&
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
                                                <NumberInput
                                                    value={cartEdit.getInputCount(cart.id) || `${cart.count}`}
                                                    onChange={(value) => {
                                                        cartEdit.setInputCount(cart.id, value);
                                                        updateQuanity(cart.id, cart, value);
                                                    }}
                                                    min={1}
                                                    allowDecimals={false}
                                                    max={cart.soda.count}
                                                />
                                            </div>
                                        </TableCell>

                                        <TableCell>
                                           <span className="font-semibold text-2xl">
                                                {cart.soda.price * cart.count} руб.
                                            </span>
                                        </TableCell>

                                        <TableCell className="text-right">
                                            <Trash2Icon className="h-10 w-10" onClick={() => deleteCart(cart.id)}/>
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    }
                </div>

                <Separator/>

                {cartQuery.carts.length === 0 && <span className="w-full text-center p-10">У вас нет ни одного товара, вернитесь на страницу каталога</span>}

                <div className="flex flex-col w-full gap-10 pt-10">
                    <div className="flex flex-row items-center justify-end gap-5">
                        <span className="text-2xl">Итоговая сумма</span>
                        <span className="text-4xl font-bold">{cartQuery.totalPrice}</span>
                    </div>
                    <div className="flex flex-row justify-between">
                        <Button className="bg-yellow-400 hover:bg-yellow-600 text-black text-xl rounded-none h-18 w-[20%]"
                                onClick={handleGoBack}>
                            Вернуться
                        </Button>

                        <Button className="bg-green-600 hover:bg-green-700 text-white text-xl rounded-none h-18 w-[20%]"
                                disabled={cartQuery.carts.length === 0}>
                            Оплата
                        </Button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export const Component = CartPage;