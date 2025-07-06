import axios from 'axios';
import { API_CONFIG } from 'config';

const api = axios.create({
    baseURL: API_CONFIG.home,
    headers: {
        'Content-Type': 'application/json',
    },
});

const authHeaders = (token) => ({
    headers: {
        Authorization: `Bearer ${token}`,
    },
});

export const getBooking = async (token) => {
    try {
        const response = await api.get('/get/booking', authHeaders(token));
        return response.data;
    } catch (error) {
        return { errors: error.response.data };
    }
};

export const getAllBookings = async (token, params = { page: 1, pageSize: 10 }) => {
    try {
        const response = await api.get('/get/bookings', {
            headers: {
                Authorization: `Bearer ${token}`,
            },
            params,
        });
        return response.data;
    } catch (error) {
        return { errors: error.response?.data || {} };
    }
};
	...