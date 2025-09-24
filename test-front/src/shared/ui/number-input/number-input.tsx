import { Input } from "@/shared/ui/kit/input.tsx"
import {useCallback} from "react"
import {Minus, Plus} from "lucide-react";
import {Button} from "@/shared/ui/kit/button.tsx";

interface NumberInputProps {
    min?: number
    max?: number
    value: string
    onChange: (value: string) => void
    allowDecimals?: boolean
    allowNegative?: boolean
}

export function NumberInput({   min,
                                max,
                                value,
                                onChange,
                                allowDecimals = true,
                                allowNegative = false,
}: NumberInputProps) {

    const handleChange = useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
        const inputValue = e.target.value

        let pattern = allowNegative ? "^-?" : "^"
        pattern += "[0-9]*"

        if (allowDecimals) {
            pattern += "[.,]?[0-9]*"
        } else {
            pattern += "$"
        }

        if (inputValue === "" || new RegExp(pattern).test(inputValue)) {
            if (min !== undefined && parseInt(inputValue) < min) {
                onChange(`${min}`)
                return
            }

            if (max !== undefined && parseInt(inputValue) > max) {
                onChange(`${max}`)
                return
            }

            onChange(inputValue)
        }

    }, [onChange, allowDecimals, allowNegative])

    let numValue = parseFloat(value.replace(',', '.'))

    if (min !== undefined && numValue < min) {
        numValue = min
    }

    if (max !== undefined && numValue > max) {
        numValue = max
    }

    const handleBlur = useCallback(() => {
        if (value) {
            const normalizedValue = value.replace(',', '.')
            if (!isNaN(parseFloat(normalizedValue))) {
                onChange(parseFloat(normalizedValue).toString())
            } else if (value === "-") {
                onChange("")
            }
        }
    }, [value, onChange])

    const increment = () => {
        const numValue = parseFloat(value.replace(',', '.')) || min || 0

        if(numValue === max) return

        onChange((numValue + 1).toString())
    }

    const decrement = () => {
        const numValue = parseFloat(value.replace(',', '.')) || min || 0

        if(numValue === min) return

        onChange((numValue - 1).toString())
    }

    return (
        <div className="flex items-center gap-2">
            <Button size="icon" onClick={decrement} className="bg-black rounded-none hover:bg-gray-900">
                <Minus className="h-4 w-4 text-white"/>
            </Button>

            <Input
                type="text"
                inputMode="numeric"
                value={value}
                onChange={handleChange}
                onBlur={handleBlur}
                className="text-center"
            />
            <Button variant="outline" size="icon" onClick={increment} className="bg-black rounded-none hover:bg-gray-900">
                <Plus className="h-4 w-4 text-white"/>
            </Button>
        </div>
    )
}