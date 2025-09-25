import {useCheck} from "@/features/check/model/use-check.ts";

export function CheckCartPage() {
    const getCheck = useCheck()

    return(
        <div className="flex flex-wrap">
            {getCheck.changeCoins.map((change) =>
                <div className="flex flex-col">
                    <span>
                        {change.banknote} - {change.count}
                    </span>
                </div>
            )}
        </div>
    )
}

export const Component = CheckCartPage;
