import React, { useState, useEffect } from 'react';
import { DeleteCoin, GetCoins } from '../utils/fetch';
import Layout from '../layout';
import CoinModal from '../components/CoinModal';
import Swal from 'sweetalert2';

const AddCoin = () => {
    const [coins, setCoins] = useState([]);
    const [filteredCoins, setFilteredCoins] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage, setItemsPerPage] = useState(10);
    const [filter, setFilter] = useState('');
    const [msg, setMsg] = useState('');
    const [showModal, setShowModal] = useState(false);
    const [coinId, setCoinId] = useState(0);
    const [coinName, setCoinName] = useState('');

    useEffect(() => {
        GetCoins([setCoins, setFilteredCoins]);
    }, []);

    // Handle filter change
    const handleFilterChange = e => {
        const value = e.target.value;
        setFilter(value);
        const filteredData = coins.filter(coin =>
            coin.Name.toLowerCase().includes(value.toLowerCase())
        );
        setFilteredCoins(filteredData);
    };

    // Handle items per page change
    const handleItemsPerPageChange = e => {
        const value = parseInt(e.target.value);
        setItemsPerPage(value);
    };

    // Calculate pagination
    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = filteredCoins.slice(indexOfFirstItem, indexOfLastItem);

    // Render table rows
    const renderTableRows = () => {
        return currentItems.map(coin => (
            <tr key={coin.Id}>
                <td className='text-center'>{coin.Id}</td>
                <td>{coin.Name}</td>
                <td className='text-center'>
                    <input type='button' value='Edit' className='btn btn-primary me-2' onClick={() => handleEditCoin(coin.Id, coin.Name)} />
                    <input type='button' value='Delete' className='btn btn-danger' onClick={() => handleDeleteCoin(coin.Id, coin.Name)} />
                </td>
            </tr>
        ));
    };

    // Render pagination
    const renderPagination = () => {
        const pageNumbers = Math.ceil(filteredCoins.length / itemsPerPage);
        return (
            <ul className="pagination">
                {currentPage > 1 && (
                    <li className="page-item" onClick={() => setCurrentPage(currentPage - 1)}>
                        <a href="#" className="page-link">&laquo;</a>
                    </li>
                )}
                {currentPage === 1 && (
                    <li className="page-item disabled">
                        <a href="#" className="page-link">&laquo;</a>
                    </li>
                )}
                {Array.from({ length: pageNumbers }, (_, index) => (
                    <li
                        key={index}
                        className={currentPage === index + 1 ? 'page-item active' : 'page-item'}
                        onClick={() => setCurrentPage(index + 1)}
                    >
                        <a href="#" className="page-link">{index + 1}</a>
                    </li>
                ))}
                {currentPage < pageNumbers && (
                    <li className="page-item" onClick={() => setCurrentPage(currentPage + 1)}>
                        <a href="#" className="page-link">&raquo;</a>
                    </li>
                )}
                {currentPage === pageNumbers && (
                    <li className="page-item disabled">
                        <a href="#" className="page-link">&raquo;</a>
                    </li>
                )}
            </ul>
        );
    };

    const handleAddCoin = () => {
        setShowModal(true);
        setCoinId(0);
        setCoinName('');
    }

    const handleEditCoin = (id, name) => {
        setShowModal(true);
        setCoinId(id);
        setCoinName(name);
    }

    const handleDeleteCoin = (id, name) => {
        Swal.fire({
            title: "Are you sure delete " + name + "?",
            showDenyButton: true,
            confirmButtonText: "Yes, delete it!",
            denyButtonText: "No, don't delete!",
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                DeleteCoin(id, setMsg);
                GetCoins([setCoins, setFilteredCoins]);
                Swal.fire({
                    icon: "info",
                    title: "Deleted",
                    text: "Coin deleted successfully!",
                });
            }
        });
    }

    return (
        <div>
            <Layout />
            <h1>Coin List</h1>
            <div className='d-flex justify-content-between'>
                <div>
                    <input type='button' value='Add Coin' className='btn btn-success' onClick={() => handleAddCoin()} />
                </div>
                <div className='mb-3 row'>
                    <label className='col-form-label col'>Filter:</label>
                    <div className='col'>
                        <input
                            className='form-control'
                            style={{ width: '200px' }}
                            type="text"
                            value={filter}
                            onChange={handleFilterChange}
                            placeholder="Filter by Name"
                        />
                    </div>
                </div>
            </div>
            <table className='table'>
                <thead className='text-center'>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th style={{ width: '200px' }}>Action</th>
                    </tr>
                </thead>
                <tbody>{renderTableRows()}</tbody>
            </table>
            <div className="row">
                <div className='col-4'></div>
                <div className='col-4 d-flex justify-content-center'>
                    <div>
                        {renderPagination()}
                    </div>
                </div>
                <div className='col-4 d-flex justify-content-end'>
                    <label className='col-form-label me-2'>Items per page:</label>
                    <div>
                        <select className="form-control" value={itemsPerPage} onChange={handleItemsPerPageChange}>
                            <option value={10}>10</option>
                            <option value={20}>20</option>
                            <option value={50}>50</option>
                        </select>
                    </div>
                </div>
            </div>
            <CoinModal
                id={coinId}
                name={coinName}
                Msg={msg}
                setMsg={setMsg}
                show={showModal}
                setShow={setShowModal}
                setCoins={setCoins}
                setFilteredCoins={setFilteredCoins}
            />
        </div>
    );
};

export default AddCoin;