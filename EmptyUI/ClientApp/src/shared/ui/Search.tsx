import {type ChangeEvent, useEffect, useState} from "react";
import {DebounceInput} from "react-debounce-input";
import {MdClear} from "react-icons/md";
import {BsSearch} from "react-icons/bs";
import "./Search.sass";

export interface ISearchProps {
    onChange: (query: string) => void;
    placeholder?: string;
    debounceTimeout?: number;
    value?: string;
}

export const Search = ({ value, onChange, placeholder, debounceTimeout = 500 }: ISearchProps) => {
    const [isShowReset, setShowReset] = useState(false);
    const [query, setQuery] = useState(value);

    useEffect(() => {
        setQuery(value);

        if (value)
            setShowReset(true);

    }, [value])

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {

        const val = e.target.value;
        if (!isShowReset && val.length > 0) {
            setShowReset(true);
        } else if (isShowReset && !val.length) {
            setShowReset(false);
        }
        setQuery(val)
        onChange(val);
    };

    const reset = () => {
        setQuery('');
        setShowReset(prev => !prev);
        onChange('');
    };

    return <div className="position-relative mt-2">
        <DebounceInput
            className="form-control search-input"
            placeholder={placeholder}
            minLength={1}
            debounceTimeout={debounceTimeout}
            onChange={handleChange}
            value={query}
        />
        <span className="search-icon p-2">
            {isShowReset
                ? <MdClear className="reset" onClick={reset}/>
                : <BsSearch className="text-primary"/>
            }
        </span>
    </div>
}