import {Table} from "react-bootstrap";
import {type StrangeItemViewModel} from "../strangeItem.models";

export const StrangeItemsTable = ({ items }: { items: Array<StrangeItemViewModel> }) => {

    return (
        <Table striped bordered hover size="sm">
            <thead>
            <tr>
                <th>Id</th>
                <th>Code</th>
                <th>Value</th>
            </tr>
            </thead>
            <tbody>{
                items.map(x =>
                    <tr>
                        <th>{x.id}</th>
                        <th>{x.code}</th>
                        <th>{x.value}</th>
                    </tr>
                )
            }
            </tbody>
        </Table>
    );
};