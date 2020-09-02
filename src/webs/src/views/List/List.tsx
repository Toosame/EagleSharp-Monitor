import React from 'react';

export class List extends React.Component {
  constructor(props) {
    super(props);
    this.state = { };
  }

  render() {
    return (
      <div>
        <div className="card-title">报警列表</div>

        <ul>
          <li>Information</li>
          <li>Information</li>
          <li>Information</li>
          <li>Information</li>
          <li>Information</li>
          <li>Information</li>
          <li>Information</li>
        </ul>
      </div>
    );
  }
}