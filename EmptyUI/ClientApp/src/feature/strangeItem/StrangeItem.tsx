import {StrangeItemsTable} from "./components/StrangeItemsTable";
import {Search} from "../../shared/ui/Search";
import {useEffect, useState} from "react";
import {type StrangeItemViewModel} from "./strangeItem.models";
import {StrangeItemApi} from "./strangeItem.api";
import {Container, Row} from "react-bootstrap";

export const StrangeItem = () => {
    const [loading, setLoading] = useState(false);

    const [items, setItems] = useState<Array<StrangeItemViewModel>>([]);

    useEffect(() => {

        setLoading(true);

        StrangeItemApi.getByFilter({}).then(response => {
            setItems(response.data)
        }).finally(() => setLoading(false));

    }, [])

    const onChangeSearchByCode = (query: string) => {

        setLoading(true);

        StrangeItemApi.getByFilter({ findCode: Number(query) }).then(response => {
            setItems(response.data)
        }).finally(() => setLoading(false));

    }

    const onChangeSearchByValue = (query: string) => {

        setLoading(true);

        StrangeItemApi.getByFilter({ findValue: query }).then(response => {
            setItems(response.data)
        }).finally(() => setLoading(false));

    }

    const onChangeSearchById = (query: string) => {

        setLoading(true);

        StrangeItemApi.getById({ id: Number(query) }).then(response => {
            setItems([response.data])
        }).finally(() => setLoading(false));

    }

    return (
        <>
            <Container>
                <Row>
                    <Search placeholder={'Поиск по коду'} onChange={onChangeSearchByCode}/>
                    <Search placeholder={'Поиск по значению'} onChange={onChangeSearchByValue}/>
                    <Search placeholder={'Поиск по идентификатору'} onChange={onChangeSearchById}/>
                </Row>
            </Container>
            <Container className="mt-2">
                { loading ? <div>Loading...</div> : <StrangeItemsTable items={ items }/> }
            </Container>
        </>
    );
};