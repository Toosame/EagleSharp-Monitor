import { cssVars, themes } from './theme.config';

export class Theme {
  constructor() {
    
  }

  static setTheme(themeName) {

    localStorage.setItem('ESM_THEME', themeName);

    cssVars.forEach(varName => {
      document.documentElement.style.setProperty(varName, themes[themeName][varName]);
    });
  }

  static getTheme() {
    return localStorage.getItem('ESM_THEME');
  }

  static changeTheme(themeName) {
    Theme.setTheme(themeName);
  }
}