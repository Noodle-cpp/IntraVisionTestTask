import {Popover, PopoverContent, PopoverTrigger} from "@radix-ui/react-popover";
import {BrandDropdownContent} from "@/features/soda-list/ui/dropdown/brand-drop-down-content";
import type {ApiSchemas} from "@/shared/api/schema";
import {ChevronsUpDown} from "lucide-react";
import {Button} from "@/shared/ui/kit/button";

interface BrandDropdownProps {
    brands: ApiSchemas['BrandResponse'][];
    value: string;
    onValueChange: (value: string) => void;
    open: boolean,
    onOpenChange: (open: boolean) => void;
}

const BrandDropdown = ({ brands, value, onValueChange, open, onOpenChange }: BrandDropdownProps) => {
    const handleSelect = (selectedValue: string) => {
        onValueChange(selectedValue === value ? "" : selectedValue);
        onOpenChange(false)
    };
    const selectedBrand = brands.find((brand) => brand.id === value);

    return (
        <Popover open={open} onOpenChange={onOpenChange}>
            <PopoverTrigger asChild>
                <Button
                    variant="outline"
                    role="combobox"
                    aria-expanded={open}
                    className="justify-between rounded-none shadow-none w-full whitespace-normal">
                    {selectedBrand ? selectedBrand.name : "Все бренды"}
                    <ChevronsUpDown className="opacity-50" />
                </Button>
            </PopoverTrigger>

            <PopoverContent
                className="p-0 w-[--radix-popover-trigger-width]"
                style={{ width: 'var(--radix-popover-trigger-width)' }}
                align="start"
            >
                <BrandDropdownContent
                    brands={brands}
                    brandValue={value}
                    onSelect={handleSelect}
                />
            </PopoverContent>
        </Popover>
    );
};

export default BrandDropdown;