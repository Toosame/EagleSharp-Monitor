import React from 'react';
import ReactDOM from 'react-dom';
import './index.scss';
import App from './App';

import { Startup } from './startup';

ReactDOM.render(
  <App />,
  document.getElementById('root'),
  () => {
    // 初始化主题
    new Startup().initTheme();
  }
);