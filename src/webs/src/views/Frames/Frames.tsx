import React from 'react';
import demo from '../../assets/image/demo.jpg';

export class Frames extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <img style={{ width: '100%', height: '100%' }} src={demo} alt="" />
    );
  }
}