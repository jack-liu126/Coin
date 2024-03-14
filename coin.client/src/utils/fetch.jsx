const baseUrl = 'https://localhost:7212';

const fetchData = async (url, setState, method = 'GET', body = null) => {
    try {
        const options = {
            method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: body ? JSON.stringify(body) : null
        };

        const response = await fetch(url, options);
        if (response.ok) {
            const data = await response.json();
            if (Array.isArray(setState)) {
                setState.forEach(setStateFunc => setStateFunc(data));
            } else {
                setState(data);
            }
        } else {
            console.error('Error fetching data:', response.status);
        }
    } catch (error) {
        console.error('Error fetching data:', error);
    }
};

export const GetCoins = (setCoins) => {
    return fetchData(baseUrl + '/Coins/GetCoins', setCoins);
}

export const AddCoin = (coin, setMsg) => {
    return fetchData(baseUrl + '/Coins/AddCoin', setMsg, 'POST', coin);
}

export const EditCoin = (coin, setMsg) => {
    return fetchData(baseUrl + '/Coins/EditCoin', setMsg, 'POST', coin);
}

export const DeleteCoin = (coinId, setMsg) => {
    return fetchData(baseUrl + '/Coins/DeleteCoin', setMsg, 'POST', { id: coinId });
}

export const GetTradeDetails = (setTradeDetails) => {
    return fetchData(baseUrl + '/Trade/GetTradeDetails', setTradeDetails);
}

export const AddTrade = (trade, setMsg) => {
    return fetchData(baseUrl + '/Trade/TradingDetailAdd', setMsg, 'POST', trade);
}

export const EditTrade = (trade, setMsg) => {
    return fetchData(baseUrl + '/Trade/TradingDetailEdit', setMsg, 'POST', trade);
}

export const DeleteTrade = (tradeId, setMsg) => {
    return fetchData(baseUrl + '/Trade/TradingDetailDelete', setMsg, 'POST', { id: tradeId });
}