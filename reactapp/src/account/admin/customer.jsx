import React, { useEffect, useState } from 'react';
import { Modal, Button, Spinner, Alert, Form } from 'react-bootstrap';
import useCrud from 'hooks/useCrud';
import { useToken } from 'context/token_context';
import usePagination from 'hooks/usePage';
import { useNavigate } from 'react-router-dom';
import UploadImage from 'components/photo_mang.jsx';
import { ... } from 'api/...';
	...

// Хелпер для рендеринга полей ввода
	...

// Компонент пагинации
	...

const AdminCustomer = () => {
    const { token } = useToken();
	...
    
export default AdminCustomer;