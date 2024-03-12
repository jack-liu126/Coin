import React, { useState, useEffect } from 'react';
import Layout from '../layout';

const AddCoin = () => {
    const [coinPairs, setCoinPairs] = useState([]);
    const [filteredCoinPairs, setFilteredCoinPairs] = useState([]);
    const [perPage, setPerPage] = useState(10);
    const [filterText, setFilterText] = useState('');

    useEffect(() => {
        // Fetch data from the "GetCoinPair" API
        fetch('API_URL')
            .then(response => response.json())
            .then(data => {
                setCoinPairs(data);
                setFilteredCoinPairs(data);
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    }, []);

    const handleAddCoinPair = () => {
        // Logic to add a new coin pair
    };

    const handleEditCoinPair = (index) => {
        // Logic to edit a coin pair at the specified index
    };

    const handleDeleteCoinPair = (index) => {
        // Logic to delete a coin pair at the specified index
    };

    const handlePerPageChange = (event) => {
        setPerPage(parseInt(event.target.value));
    };

    const handleFilterTextChange = (event) => {
        setFilterText(event.target.value);
        filterCoinPairs(event.target.value);
    };

    const filterCoinPairs = (text) => {
        const filtered = coinPairs.filter(pair =>
            pair.switchCoin1.includes(text) || pair.switchCoin2.includes(text)
        );
        setFilteredCoinPairs(filtered);
    };

    return (
        <>
            <Layout />
            <div>
                <h1>Add Coin</h1>

                <div>
                    <label>Rows per page:</label>
                    <select value={perPage} onChange={handlePerPageChange}>
                        <option value={10}>10</option>
                        <option value={20}>20</option>
                        <option value={50}>50</option>
                        <option value={100}>100</option>
                    </select>
                </div>

                <div>
                    <label>Filter:</label>
                    <input type="text" value={filterText} onChange={handleFilterTextChange} />
                </div>

                <table>
                    <thead>
                        <tr>
                            <th>Switch Coin 1</th>
                            <th>Switch Coin 2</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredCoinPairs.slice(0, perPage).map((pair, index) => (
                            <tr key={index}>
                                <td>{pair.switchCoin1}</td>
                                <td>{pair.switchCoin2}</td>
                                <td>
                                    <button onClick={() => handleEditCoinPair(index)}>Edit</button>
                                    <button onClick={() => handleDeleteCoinPair(index)}>Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>

                {/* Add coin pair form */}
                {/* Edit coin pair form */}
            </div>
        </>
    );
};

export default AddCoin;