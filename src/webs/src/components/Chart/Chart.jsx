import React from 'react';
import './Chart.scss';

export class Chart extends React.Component {

  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    const { title } = this.props;

    return (
      <div className="chart-container">
        <div className="title">{title}</div>
        <div className="body">
          图表
        </div>
      </div>
    );
  }
}