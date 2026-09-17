import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// Configures Vite to understand React files and use the CORS-approved development port.
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    strictPort: true,
  },
});
