import { Theme } from '@utils/theme/theme';

export class Startup {
  constructor() { }

  /**
   * 初始化主题
   * @param themeName 主题名称
   */
  initTheme() {
    let themeName = Theme.getTheme();
    Theme.setTheme(themeName ? themeName : 'dark');
  }

}