import React from 'react';
import './Charts.scss';

import { Chart, LineAdvance } from 'bizcharts';

export class Charts extends React.Component {

  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    const { title } = this.props;
    const data = [
      {
        month: "Jan",
        city: "Tokyo",
        temperature: 7
      },
      {
        month: "Feb",
        city: "Tokyo",
        temperature: 13
      },
      {
        month: "Mar",
        city: "Tokyo",
        temperature: 16.5
      },
      {
        month: "Apr",
        city: "Tokyo",
        temperature: 14.5
      },
      {
        month: "May",
        city: "Tokyo",
        temperature: 10
      },
      {
        month: "Jun",
        city: "Tokyo",
        temperature: 7.5
      },
      {
        month: "Jul",
        city: "Tokyo",
        temperature: 9.2
      },
      {
        month: "Aug",
        city: "Tokyo",
        temperature: 14.5
      },
      {
        month: "Sep",
        city: "Tokyo",
        temperature: 9.3
      },
      {
        month: "Oct",
        city: "Tokyo",
        temperature: 8.3
      },
      {
        month: "Nov",
        city: "Tokyo",
        temperature: 8.9
      },
      {
        month: "Dec",
        city: "Tokyo",
        temperature: 5.6
      }
    ];


    return (
      <div className="chart-card">
        <div className="title">{title}</div>
        <div className="body">
          <Chart height={200} autoFit data={data}>
            <LineAdvance
              shape="smooth"
              point
              area
              position="month*temperature"
              color="city"
            />
          </Chart>
        </div>
      </div>
    );
  }
}