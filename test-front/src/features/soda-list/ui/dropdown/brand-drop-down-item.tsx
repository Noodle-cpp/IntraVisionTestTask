import { Check } from "lucide-react";
import {CommandItem} from "cmdk";
import {cn} from "@/shared/lib/css.ts";

interface BrandDropdownItemProps {
    id: string;
    value: string;
    label: string;
    isSelected: boolean;
    onSelect: (value: string) => void;
}

export function BrandDropdownItem ({
                               id,
                               value,
                               label,
                               isSelected,
                               onSelect
                           }: BrandDropdownItemProps) {
    return (
        <CommandItem
            id={id}
            key={id}
            value={value}
            onSelect={onSelect}
            className="aria-selected:bg-gray-100 hover:bg-gray-100 px-4 py-2"
        >
            <div className="flex items-center justify-between w-full">
            {label}
            <Check className={cn("ml-auto", isSelected ? "opacity-100" : "opacity-0")}/>
            </div>
        </CommandItem>
    );
}