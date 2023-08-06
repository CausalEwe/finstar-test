import {httpClient} from "../../httpClient/axios";
import {type StrangeItemCreateModel, type StrangeItemFilter, type StrangeItemViewModel} from "./strangeItem.models";
import {type GenericAbortSignal} from "axios";

const endpoints = {
    GET_BY_FILTER: "strangeItems/GetByFilter",
    GET_BY_ID: "strangeItems/GetById",
    CREATE: "strangeItems/Create",
    GET_COUNT: "strangeItems/GetCount"
};

export class StrangeItemApi {
    static getByFilter(params: StrangeItemFilter, signal?: GenericAbortSignal) {
        return httpClient.get<Array<StrangeItemViewModel>>(endpoints.GET_BY_FILTER, { params, signal });
    };

    static create(data: Array<StrangeItemCreateModel>, signal?: GenericAbortSignal) {
        return httpClient.post<void>(endpoints.CREATE, data, { signal });
    };

    static getCount(params: { findCode?: number, findValue?: string, findId?: number }, signal?: GenericAbortSignal) {
        return httpClient.get<number>(endpoints.GET_COUNT, {params, signal});
    }
}