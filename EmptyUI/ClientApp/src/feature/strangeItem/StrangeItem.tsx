import { Container, Row } from "react-bootstrap";
import { Search } from "../../shared/ui/Search";
import { useEffect, useState } from "react";
import { type StrangeItemViewModel } from "./strangeItem.models";
import { StrangeItemApi } from "./strangeItem.api";
import { StrangeItemsTable } from "./components/StrangeItemsTable";

export const StrangeItem = () => {

    const [loading, setLoading] = useState(false);
    const [items, setItems] = useState<Array<StrangeItemViewModel>>([]);
    const [totalItems, setTotalItems] = useState(0);
    const [page, setPage] = useState(0);
    const [searchCode, setSearchCode] = useState("");
    const [searchValue, setSearchValue] = useState("");
    const [searchId, setSearchId] = useState("");
    const rowsPerPage = 10;

    useEffect(() => {

        fetchData();

        if (totalItems === 0) {
            getCount();
        }

    }, [page]);

    useEffect(() => {

        onChangeSearch();

    }, [searchCode, searchValue, searchId]);

    const fetchData = () => {

        setLoading(true);

        const findCode = searchCode.trim() !== "" ? Number(searchCode) : undefined;
        const findValue = searchValue.trim() !== "" ? searchValue : undefined;
        const findId = searchId.trim() !== "" ? Number(searchId) : undefined;

        getCount();

        StrangeItemApi.getByFilter({ from: page * rowsPerPage, count: rowsPerPage, findCode, findValue, findId }).then((response) => {
            setItems(response.data);
        }).finally(() => setLoading(false));

    };

    const getCount = () => {

        const findCode = searchCode.trim() !== "" ? Number(searchCode) : undefined;
        const findValue = searchValue.trim() !== "" ? searchValue : undefined;
        const findId = searchId.trim() !== "" ? Number(searchId) : undefined;

        StrangeItemApi.getCount({ findCode, findValue, findId }).then((response) => {
            setTotalItems(response.data);
        });

    };

    const onChangeSearch = () => {

        setLoading(true);

        setPage(0);

        fetchData();

    };

    return (
        <>
            <Container>
                <Row>
                    <Search placeholder={"Поиск по коду"} value={searchCode} onChange={(query) => setSearchCode(query)} />
                    <Search placeholder={"Поиск по значению"} value={searchValue} onChange={(query) => setSearchValue(query)} />
                    <Search placeholder={"Поиск по идентификатору"} onChange={(query) => setSearchId(query)} />
                </Row>
            </Container>
            <Container className="mt-2">
                {loading ? (
                    <div>Loading...</div>
                ) : (
                    <>
                        <StrangeItemsTable
                            items={items}
                            totalItems={totalItems}
                            rowsPerPage={rowsPerPage}
                            page={page}
                            onPageChange={(newPage) => setPage(newPage)}
                        />
                    </>
                )}
            </Container>
        </>
    );
};