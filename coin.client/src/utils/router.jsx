import { createBrowserRouter } from 'react-router-dom';
import App from '../App';
import AddCoin from '../page/AddCoin';
const router = createBrowserRouter([
    {
        path: '/',
        element: <App />
    },
    {
        path: '/add-coin-pair',
        element: <AddCoin />
    }
]);

export default router;