import React from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import { AddCoin, EditCoin, GetCoins } from '../utils/fetch';
import Swal from 'sweetalert2';

const CoinModal = ({ id, name, Msg, setMsg, show, setShow, setCoins, setFilteredCoins }) => {
    const isEdit = !!id;

    const handleSubmit = (event) => {
        event.preventDefault();
        if (isEdit) {
            EditCoin({ Id: id, Name: event.target.coinName.value }, setMsg)
                .then(() => {
                    GetCoins([setCoins, setFilteredCoins]);
                    setShow(false);
                    if (Msg !== null) {
                        if (Msg.Status === "Success") {
                            Swal.fire({
                                icon: "success",
                                title: "Success",
                                text: "Coin updated successfully!",
                            });
                        }
                    }
                    else {
                        Swal.fire({
                            icon: "error",
                            title: "Error",
                            text: "Coin edit failed!",
                        });
                    }
                })
        } else {
            AddCoin({ Name: event.target.coinName.value }, setMsg)
                .then(() => {
                    GetCoins([setCoins, setFilteredCoins]);
                    setShow(false);
                    if (Msg !== null) {
                        if (Msg.Status === "Success") {
                            Swal.fire({
                                icon: "success",
                                title: "Success",
                                text: "Coin added successfully!",
                            });
                        }
                        else {
                            Swal.fire({
                                icon: "error",
                                title: "Error",
                                text: "Coin add failed!",
                            });
                        }
                    }
                })
        }
    };

    const handleClose = (event) => {
        setShow(false);
        document.getElementById('coinName').value = ''
    }

    return (
        <Modal show={show} onHide={() => handleClose()} centered>
            <Modal.Header closeButton>
                <Modal.Title>{isEdit ? 'Edit Coin' : 'Add Coin'}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmit}>
                    <Form.Group controlId="coinName">
                        <Form.Label>Coin Name</Form.Label>
                        <Form.Control type="text" defaultValue={name} />
                    </Form.Group>
                    <br />
                    <div className='d-flex justify-content-end'>
                        <Button variant="primary" type="submit" className='me-2'>
                            {isEdit ? 'Save Changes' : 'Add Coin'}
                        </Button>
                        <Button variant="secondary" onClick={() => handleClose()}>Cancel</Button>
                    </div>
                </Form>
            </Modal.Body>
        </Modal>
    );
};

export default CoinModal;