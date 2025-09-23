export function SodaListLayoutContent({
                                                children,
                                                cursorRef,
                                                hasCursor,
                                                isEmpty,
                                                isPending,
                                                isPendingNext,
                                                renderList,
                                            }: {
    children?: React.ReactNode;
    isEmpty?: boolean;
    isPending?: boolean;
    isPendingNext?: boolean;
    cursorRef?: React.Ref<HTMLDivElement>;
    hasCursor?: boolean;
    renderList?: () => React.ReactNode;
}) {
    return (
        <div>
            {isPending && <span>Загрузка</span>}
            {renderList && (
                <CharSheetsListLayoutCards>{renderList?.()}</CharSheetsListLayoutCards>
            )}
            {!isPending && children}

            {isEmpty && !isPending && (
                <span>Ничего не найдено</span>
            )}

            {hasCursor && (
                <div ref={cursorRef} className="text-center py-8">
                    {isPendingNext && <span>Загрузка</span>}
                </div>
            )}
        </div>
    );
}

function CharSheetsListLayoutCards({
                                              children,
                                          }: {
    children: React.ReactNode;
}) {
    return (
        <div className="flex flex-wrap justify-center gap-5">
            {children}
        </div>
    );
}