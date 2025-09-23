import { Command, CommandList, CommandEmpty, CommandGroup } from "@/shared/ui/kit/command";
import type {ApiSchemas} from "@/shared/api/schema";
import {BrandDropdownItem} from "@/features/soda-list/ui/dropdown/brand-drop-down-item.tsx";

interface BrandDropdownContentProps {
    brands: ApiSchemas["BrandResponse"][];
    brandValue: string;
    onSelect: (value: string) => void;
}

export function BrandDropdownContent ({
                                  brands,
                                  brandValue,
                                  onSelect
                              }: BrandDropdownContentProps) {
    return (
        <Command>
            <CommandList>
                <CommandEmpty className="px-4 py-2 text-sm text-gray-500">
                    Ничего не найдено.
                </CommandEmpty>
                <CommandGroup>
                    <BrandDropdownItem
                        id="allBrand"
                        key="allBrand"
                        value=""
                        label="Все бренды"
                        isSelected={!brandValue}
                        onSelect={onSelect}
                    />

                    {brands?.map((brand) => (
                        <BrandDropdownItem
                            id={brand.id}
                            key={brand.id}
                            value={brand.id}
                            label={brand.name}
                            isSelected={brandValue === brand.name}
                            onSelect={onSelect}
                        />
                    ))}
                </CommandGroup>
            </CommandList>
        </Command>
    );
}