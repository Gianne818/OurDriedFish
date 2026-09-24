/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Components/**/*.{razor,html,cshtml}",
    "./wwwroot/**/*.html"
  ],
  theme: {
    extend: {
      colors: {
        'brand-dark': '#111111',
        'brand-yellow': '#FFC700',
        'brand-yellow-hover': '#E8B400',
        'brand-cream': '#F8F7F4',
        'brand-card': '#FAFAFA',
        'brand-border': '#E5E5E5',
        'brand-muted': '#555555',
        'brand-light': '#777777',
      },
      fontFamily: {
        heading: ['Outfit', 'Plus Jakarta Sans', 'sans-serif'],
        body: ['Plus Jakarta Sans', '-apple-system', 'BlinkMacSystemFont', 'Segoe UI', 'Roboto', 'sans-serif'],
      },
      borderRadius: {
        'sm': '8px',
        'md': '14px',
        'lg': '20px',
        'xl': '26px',
        '2xl': '28px',
      },
      boxShadow: {
        'soft': '0 4px 20px rgba(0, 0, 0, 0.06)',
        'hover': '0 10px 30px rgba(0, 0, 0, 0.12)',
        'drawer': '-4px 0 25px rgba(0, 0, 0, 0.15)',
      }
    },
  },
  plugins: [],
}
