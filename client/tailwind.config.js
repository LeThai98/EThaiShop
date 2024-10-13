
/*
 * Tailwind CSS configuration file.
 * 
 * @type {import('tailwindcss').Config}
 * 
 * @property {Array<string>} content - Specifies the paths to all of the template files in the project.
 * @property {Object} theme - Allows customization of the default theme.
 * @property {Object} theme.extend - Provides a way to extend the default theme.
 * @property {Array} plugins - An array to register plugins.
 * @property {boolean} important - Adds `!important` to all Tailwind CSS utility classes.
 * 
 */


/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {},
  },
  plugins: [],
  important: true,
}