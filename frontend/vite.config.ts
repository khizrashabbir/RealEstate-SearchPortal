import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
  https: false,
    proxy: {
      '/auth': 'https://localhost:7112',
      '/properties': 'https://localhost:7112',
      '/favorites': 'https://localhost:7112',
    }
  }
})
