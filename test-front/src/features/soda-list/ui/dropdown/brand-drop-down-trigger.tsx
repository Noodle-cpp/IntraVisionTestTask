import { Button } from "@/shared/ui/kit/button";
import { ChevronsUpDown } from "lucide-react";
import type {ApiSchemas} from "@/shared/api/schema";

interface BrandDropdownTriggerProps {
    isOpen: boolean;
    brandValue: string;
    brands: ApiSchemas["BrandResponse"][];
    onOpenChange: (open: boolean) => void;
}

export function BrandDropdownTrigger ({
                                  isOpen,
                                  brandValue,
                                  brands,
                                  onOpenChange
                              }: BrandDropdownTriggerProps)
{
    return (
        <Button
            variant="outline"
            role="combobox"
            aria-expanded={isOpen}
            className="justify-between rounded-none shadow-none w-full whitespace-normal"
            onClick={() => onOpenChange(!isOpen)}
        >
            {brandValue ? brands?.find((brand) => brand.name === brandValue)?.name : "Все бренды"}
            <ChevronsUpDown className="opacity-50"/>
        </Button>
    );
}