import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
    plugins: [react()],
    //Local developpement
    /*
    server: {
        proxy: {
            '/api/pixels': {
                target: 'http://localhost:5116',
                changeOrigin:true,
            },
            '/health': {
                target: 'http://localhost:5116',
                changeOrigin: true,
            },
            'metrics': {
                target: 'http://localhost:5116',
                changeOrigin: true,
            },
        }
    }
    */
})
