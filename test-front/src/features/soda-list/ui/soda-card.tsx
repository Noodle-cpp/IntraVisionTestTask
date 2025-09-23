import {Card, CardContent, CardFooter} from "@/shared/ui/kit/card.tsx";
import type {ApiSchemas} from "@/shared/api/schema";
import {DisabledButton, SelectButton, SelectedButton} from "@/features/soda-list/ui/soda-card-button.tsx";
import {CONFIG} from "@/shared/model/config.ts";

export function SodaCard({soda, isProductInCart}: {soda: ApiSchemas["SodaResponse"], isProductInCart: boolean}) {
    return (
        <Card className="basis-[calc(25%-15px)]
                        min-w-75
                        rounded-none shadow-none items-center
                        p-2">
            <img src={`${CONFIG.API_BASE_URL}/sodas/${soda.id}/img`}
                 loading="lazy"
                 className="w-full h-[100%] p-10"
                 alt={soda.name}/>
            <CardContent>
                <div className="flex flex-col gap-2 items-center">
                    <span className="text-xl">{soda.name}</span>
                    <span className="text-xl">{soda.price} руб.</span>
                </div>
            </CardContent>
            <CardFooter className="w-full">
                {isProductInCart && <SelectedButton/>}
                {!isProductInCart && soda.count > 0 && <SelectButton/>}
                {!isProductInCart && soda.count <= 0 && <DisabledButton/>}
            </CardFooter>
        </Card>
    )
}