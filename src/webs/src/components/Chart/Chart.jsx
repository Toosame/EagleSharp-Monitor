import React from 'react';
import './Chart.scss';

export class Chart extends React.Component {

  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    const { children, title } = this.props;
    
    return (
      <div className="chart-card">
        <div className="title">{title}</div>
        <div className="body">
          {children}
        </div>
      </div>
    );
  }
}