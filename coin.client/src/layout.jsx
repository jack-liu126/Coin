import React from 'react';
import { Link } from 'react-router-dom';
import HomeLogo from './assets/bitcoin-removebg.png';

const Layout = () => {
    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-black">
            <div className="container-fluid">
                <Link className="navbar-brand" to="/">
                    <img src={HomeLogo} alt="Home" className="navbar-icon" width="50" />
                </Link>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav">
                        <li className="nav-item">
                            <Link className="nav-link active" to="/add-coin-pair">
                                Add Coin
                            </Link>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    );
};

export default Layout;