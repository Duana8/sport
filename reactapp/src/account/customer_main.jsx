import React, { useState } from 'react';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { useToken } from 'context/token_context';
import { ... } from 'api/...';
	...

const CustomerSchedule = () => {
    const { token } = useToken();
    const queryClient = useQueryClient();
...

export default CustomerSchedule;