import React from 'react';
import './Header.scss';

import { Theme } from '@utils/theme/theme';

export class Header extends React.Component {

  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <header className="header">
        <div onClick={Theme.changeTheme.bind(this, 'light')}>光</div>
        <div onClick={Theme.changeTheme.bind(this, 'dark')}>暗</div>
      </header>
    );
  }
}