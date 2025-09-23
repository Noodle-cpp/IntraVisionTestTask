import {Button} from "@/shared/ui/kit/button";
import {cn} from "@/shared/lib/css.ts";

export function DisabledButton() {
    return (
        <BaseButton onClick={() => {}}
                    className="bg-gray-200 text-black"
                    idDisabled={true}>
            <span>Закончился</span>
        </BaseButton>
    )
}

export function SelectedButton() {
    return (
        <BaseButton onClick={() => console.log('Товар уже корзине')}
                    className="bg-green-600 hover:bg-green-700 text-white"
                    idDisabled={false}>
            <span>Выбрано</span>
        </BaseButton>
    )
}

export function SelectButton() {
    return (
        <BaseButton onClick={() => console.log('Товар теперь корзине')}
                    className="bg-yellow-400 hover:bg-yellow-600 text-black"
                    idDisabled={false}>
            <span>Выбрать</span>
        </BaseButton>
    )
}

function BaseButton({className, onClick, children, idDisabled}: {
    className: string,
    onClick: () => void,
    children ? : React.ReactNode,
    idDisabled: boolean
}) {
    return(
        <Button className={cn("w-full cursor-pointer rounded-none disabled:opacity-100", className)}
                disabled={idDisabled}
                onClick={() => onClick()}>
            {children}
        </Button>
    )
}