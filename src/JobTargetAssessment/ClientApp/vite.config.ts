import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  build: {
    outDir: '../wwwroot',
    emptyOutDir: true,
    manifest: true,
  },
  server: {
    port: 3000,
    // Enable hot reload
    hmr: true,
    // Watch for file changes
    watch: {
      usePolling: true, // Needed for WSL/Docker
    }},
  base: '/',
})
