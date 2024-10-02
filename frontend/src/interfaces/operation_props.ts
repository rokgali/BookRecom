export interface OperationProps {
    message: string,
    duration: number,
    onComplete(): void
}