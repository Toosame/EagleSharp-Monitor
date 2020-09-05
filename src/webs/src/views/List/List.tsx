import React from 'react';
import './List.scss';

export class List extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <div>
        <div className="card-title theme-title">报警列表</div>

        <ul className="list">
          <li>Information Information Information</li>
          <li>Information Information Information</li>
          <li>Information Information Information</li>
          <li>Information Information Information</li>
          <li>Information Information Information</li>
          <li>Information Information Information</li>
          <li>Information Information Information</li>
        </ul>
      </div>
    );
  }
}