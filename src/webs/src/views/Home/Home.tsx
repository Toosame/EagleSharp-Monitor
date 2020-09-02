import React from 'react';

import { List } from '../List/List';
import { Frames } from '../Frames/Frames';

export class Home extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <div>
        <List />
        <Frames />
      </div>
    );
  }
}