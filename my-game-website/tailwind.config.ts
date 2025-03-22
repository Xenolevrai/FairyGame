import type { Config } from 'tailwindcss';

const config: Config = {
  content: ['./src/**/*.{html,js,svelte,ts}'], // Ensure Tailwind scans your files
  theme: {
    extend: {
      colors: {
        medievalRed: '#8B0000', // Example color for medieval red
        parchment: '#F5F5DC', // Example color for parchment-style backgrounds
      },
      backgroundImage: {
        'medieval-background': "url('/fairywood-design.png')", // Path to your image
      },
    },
  },
  plugins: [],
};

export default config;