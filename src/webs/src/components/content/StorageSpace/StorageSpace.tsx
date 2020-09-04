import React, { createElement } from 'react';
// import { Chart, LineAdvance, Axis, Tooltip } from 'bizcharts';
import { DonutChart, Chart, Coordinate, Interval, Axis, Tooltip } from 'bizcharts';
import { ChartCard } from '@components/common/ChartCard/ChartCard';
// import { homeChartConfig } from '../config';

export class StorageSpace extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    const data = [
      {
        type: '可用空间',
        value: 128,
      },
      {
        type: '已用空间',
        value: 286,
      }
    ];

    return (
      <ChartCard title="存储空间">
        <Chart data={data} height={200} autoFit>
          <Coordinate type="theta" radius={0.8} innerRadius={0} />
          <Axis visible={false} />
          <Tooltip showTitle={false} />
          <Interval
            color={['type', ['#0652DD', '#1B1464']]}
            adjust="stack"
            position="value"
            shape="sliceShape"
          />
        </Chart>
      </ChartCard>
    );
  }
} 