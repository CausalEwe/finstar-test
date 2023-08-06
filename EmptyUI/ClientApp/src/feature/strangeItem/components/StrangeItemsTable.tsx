import { Table } from "react-bootstrap";
import { type StrangeItemViewModel } from "../strangeItem.models";
import { TablePagination } from "@mui/material";
import styled from "@emotion/styled";

interface StrangeItemsTableProps {
    items: Array<StrangeItemViewModel>;
    totalItems: number;
    rowsPerPage: number;
    page: number;
    onPageChange: (newPage: number) => void;
}

export const StrangeItemsTable = ({ items, totalItems, rowsPerPage, page, onPageChange }: StrangeItemsTableProps) => {

    const handleChangePage = (event: React.MouseEvent<HTMLButtonElement> | null, newPage: number) => {
        onPageChange(newPage);
    };

    return (
        <>
            <Table striped bordered hover size="sm">
                <thead>
                <tr>
                    <th>Id</th>
                    <th>Code</th>
                    <th>Value</th>
                </tr>
                </thead>
                <tbody>
                {items.map((x) => (
                    <tr key={x.id}>
                        <th>{x.id}</th>
                        <th>{x.code}</th>
                        <th>{x.value}</th>
                    </tr>
                ))}
                </tbody>
            </Table>
            <TablePagination
                component="div"
                count={totalItems}
                page={page}
                onPageChange={(event, newPage) => onPageChange(newPage)}
                rowsPerPage={rowsPerPage}
                rowsPerPageOptions={[]}
            />
        </>
    );
};