import {Button} from "@/shared/ui/kit/button";
import {cn} from "@/shared/lib/css.ts";

export function DisabledButton() {
    return (
        <BaseButton onClick={() => {}}
                    className="bg-gray-200 text-black"
                    isDisabled={true}>
            <span>Закончился</span>
        </BaseButton>
    )
}

export function SelectedButton({onClick}:{onClick?:() => void}) {
    return (
        <BaseButton onClick={() => onClick}
                    className="bg-green-600 hover:bg-green-700 text-white"
                    isDisabled={false}>
            <span>Выбрано</span>
        </BaseButton>
    )
}

export function SelectButton({onClick}:{onClick:() => void}) {
    return (
        <BaseButton onClick={onClick}
                    className="bg-yellow-400 hover:bg-yellow-600 text-black"
                    isDisabled={false}>
            <span>Выбрать</span>
        </BaseButton>
    )
}

function BaseButton({className, onClick, children, isDisabled}: {
    className: string,
    onClick: () => void,
    children ? : React.ReactNode,
    isDisabled: boolean
}) {
    return(
        <Button className={cn("w-full cursor-pointer rounded-none disabled:opacity-100", className)}
                disabled={isDisabled}
                onClick={onClick}>
            {children}
        </Button>
    )
}