
export const cssVars = [
  '--theme-background-color',
  '--theme-color',
  '--theme-body-bgcolor'
];

export const themeNames = ['light', 'dark', 'deep-dark'];

export const themes = {
  'light': {
    '--theme-body-bgcolor': '#dfe4ea',
    '--theme-background-color': '#fff',
    '--theme-color': '#22252a'
  },
  'dark': {
    '--theme-body-bgcolor': '#57606f',
    '--theme-background-color': '#2f3542',
    '--theme-color': '#16dad3'
  },
  'deep-dark': {
    '--theme-body-bgcolor': '#22252a',
    '--theme-background-color': '#2d3035',
    '--theme-color': '#16dad3'
  }
};


// TODO:明天来写
function generateThemeConfig() {

  themeNames.forEach(name => {
    let obj = {};
  });

}