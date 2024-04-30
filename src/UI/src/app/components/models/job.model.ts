export class Job {
    constructor(
        readonly id: number,
        readonly title: string,
        readonly description: string
    ) {}

    toString(): string {
        return `${this.title}`;
    }
}