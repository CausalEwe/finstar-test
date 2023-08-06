export interface StrangeItemViewModel {
    id: number;
    code: number;
    value: string;
}

export interface StrangeItemFilter {
    from?: number;
    count?: number;
    findCode?: number;
    findValue?: string;
    findId?: number;
}

export interface StrangeItemCreateModel {
    code: number;
    value: string;
}