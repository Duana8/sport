import React, { createContext, useContext, useState, useEffect } from 'react';
import { jwtDecode } from 'jwt-decode';

const TokenContext = createContext();

export const TokenProvider = ({ children }) => {
    const [token, setTokenState] = useState(() => localStorage.getItem('jwtToken'));

    // ��������� ������ � ��� �������
    const setToken = (newToken) => {
        localStorage.setItem('jwtToken', newToken);
        setTokenState(newToken);
    };

    const clearToken = () => {
        localStorage.removeItem('jwtToken');
        setTokenState(null);
    };

    // �������� ��������� ����� �������� ������
    useEffect(() => {
        if (!token) return;

        const decoded = jwtDecode(token);
        const expirationTime = decoded.exp * 1000 - Date.now();

        if (expirationTime > 0) {
            const timer = setTimeout(() => {
                clearToken();
                window.location.href = '/auth';
            }, expirationTime);
            return () => clearTimeout(timer);
        } else {
            clearToken();
            window.location.href = '/auth';
        }
    }, [token]);

    return (
        <TokenContext.Provider value={{ token, setToken, clearToken }}>
            {children}
        </TokenContext.Provider>
    );
};

export const useToken = () => useContext(TokenContext);
