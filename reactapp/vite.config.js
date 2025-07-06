import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';

export default defineConfig({
    plugins: [plugin()],
    server: {
        port: 60598,
    },
    build: {
        sourcemap: false
    },
    resolve: {
        alias: {
            '@': '/src',
            'xxx': '/src/xxx',
		...
        },
    },
});