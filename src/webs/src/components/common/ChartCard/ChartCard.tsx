import React from 'react';
import './ChartCard.scss';

interface IProps {
  title: string
}

interface IState {
  name?: string
}

export class ChartCard extends React.Component<IProps, IState> {
  constructor(props) {
    super(props);
    this.state = {
    };
  }

  render() {
    const { title, children } = this.props;

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