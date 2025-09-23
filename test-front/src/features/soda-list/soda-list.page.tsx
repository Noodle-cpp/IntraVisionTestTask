import {Button} from "@/shared/ui/kit/button";
import {Slider} from "@/shared/ui/kit/slider";
import {Separator} from "@/shared/ui/kit/separator";
import {useSodaList} from "@/features/soda-list/model/use-sodas-list.ts";
import {SodaCard} from "@/features/soda-list/ui/soda-card.tsx";
import BrandDropdown from "@/features/soda-list/ui/dropdown/brand-drop-down.tsx";
import { SodaListLayoutContent } from "./ui/soda-list-layout-content";
import {useGetBrands} from "@/features/soda-list/model/use-get-brands.ts";
import {useGetCarts} from "@/features/soda-list/model/use-get-carts.ts";
import {useFilter} from "@/features/soda-list/model/use-filter-state.ts";
import {href, Link} from "react-router-dom";
import {ROUTES} from "@/shared/model/routes.ts";
import {useAddToCart} from "@/features/soda-list/model/use-create-cart.ts";

function SodasListPage() {
    // const sodasFilter = useSodasFilters();
    const brandsQuery = useGetBrands();
    const cartsQuery = useGetCarts();
    const sodasFilter = useFilter();
    const sodasQuery = useSodaList({
        brandId: sodasFilter.brandId,
        minPriceValue: sodasFilter.minPriceValue,
        maxPriceValue: sodasFilter.maxPriceValue,
    })
    const {addToCart} = useAddToCart();

    const handleAddToCart = (sodaId: string) => {
        addToCart(sodaId);
    };

    if (!sodasQuery.isPending && sodasQuery.maxPrice > 0 &&
        sodasFilter.minPrice === 0 && sodasFilter.maxPrice === 0) {
        sodasFilter.updateFromApi(sodasQuery.minPrice, sodasQuery.maxPrice);
    }

    return (
        <div className="flex flex-col p-10 gap-10">
            <nav>
                <div className="flex flex-col gap-10">
                    <div className="flex flex-wrap justify-between ">
                        <span className="font-semibold text-3xl">
                            Газированные напитки
                        </span>

                        <Button className="flex flex-wrap h-18 w-[30%]
                                            text-xl
                                            bg-gray-300 text-black
                                            hover:bg-gray-500 hover:opacity-100
                                            rounded-none">
                            Импорт
                        </Button>
                    </div>

                    <div className="flex flex-wrap justify-between">
                        <div className="flex flex-wrap flex-col w-[30%] gap-4">
                            <span>Выберите бренд</span>
                            <BrandDropdown
                                brands={brandsQuery.brands}
                                value={sodasFilter.brandId || ""}
                                onValueChange={(id) => {
                                    sodasFilter.setBrandId(id);
                                }}
                                open={sodasFilter.open}
                                onOpenChange={(open) => {
                                    sodasFilter.setOpen(open);
                                }}
                            />
                        </div>

                        <div className="flex flex-wrap flex-col w-[30%] gap-2">
                            <span>Стоимость</span>
                            <div className="flex flex-row justify-between">
                                <span>{sodasFilter.minPriceValue} руб.</span>
                                <span>{sodasFilter.maxPriceValue} руб.</span>
                            </div>
                            <Slider
                                defaultValue={[sodasQuery.minPrice, sodasQuery.maxPrice]}
                                value={[sodasFilter.minPriceValue, sodasFilter.maxPriceValue]}
                                onValueChange={(value) => {
                                    sodasFilter.setPriceValues(value[0], value[1]);
                                }}
                                onValueCommit={(value) => {
                                    sodasFilter.setPriceRange(value[0], value[1]);
                                }}
                                max={sodasQuery.maxPrice}
                                min={sodasQuery.minPrice}
                                step={1}
                                className="w-full"
                                disabled={sodasQuery.isPending}
                            />
                        </div>


                        <div className="flex flex-wrap flex-col w-[30%] gap-2">
                            <Button className="flex flex-wrap h-18
                                                text-xl
                                                bg-green-600 text-white
                                                hover:bg-green-700 hover:opacity-100
                                                rounded-none"
                                    disabled={cartsQuery.purchaseCount === 0}
                                    asChild>
                                {cartsQuery.purchaseCount === 0 ? (
                                    <span>Выбрано: {cartsQuery.purchaseCount}</span>
                                ) : (
                                    <Link key="cart" to={href(ROUTES.CART)}>
                                        Выбрано: {cartsQuery.purchaseCount}
                                    </Link>
                                )}
                            </Button>
                        </div>
                    </div>
                </div>
            </nav>

            <Separator/>

            <main>
                <SodaListLayoutContent
                    isEmpty={sodasQuery.sodas.length === 0}
                    isPending={sodasQuery.isPending}
                    isPendingNext={sodasQuery.isFetchingNextPage}
                    cursorRef={sodasQuery.cursorRef}
                    hasCursor={sodasQuery.hasNextPage}
                    renderList={() =>
                        sodasQuery.sodas?.map((soda) => (
                            <SodaCard key={soda.id} soda={soda}
                                      isProductInCart={cartsQuery.isProductInCart(soda.id)}
                                      onButtonCLick={() => handleAddToCart(soda.id)}/>
                        ))
                    }
                />
            </main>
        </div>
    );
}


export const Component = SodasListPage;