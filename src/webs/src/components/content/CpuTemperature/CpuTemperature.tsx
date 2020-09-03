import React from 'react';
import { Chart, LineAdvance, Axis, Tooltip } from 'bizcharts';
import { ChartCard } from '@components/common/ChartCard/ChartCard';
import { homeChartConfig } from '../config';

export class CpuTemperature extends React.Component {
  constructor(props) {
    super(props);

    this.state = {};
  }

  render() {
    const data = [
      { time: 0, rate: 16 },
      { time: 3, rate: 30 },
      { time: 6, rate: 50 },
      { time: 9, rate: 55 },
      { time: 12, rate: 30 },
      { time: 15, rate: 20 },
      { time: 18, rate: 23 },
      { time: 21, rate: 28 },
      { time: 24, rate: 5 }
    ];

    const chartConfig = {
      height: 200,
      autoFit: true,
      scale: {
        time: {
          alias: '时间',
          ticks: [0, 3, 6, 9, 12, 15, 18, 21, 24]
        },
        rate: {
          alias: '使用率',
          ticks: [0, 20, 40, 60, 80, 100]
        }
      }
    };

    const lineAdvanceConfig = {
      ...homeChartConfig,
      position: "time*rate",
      color: "#B53471"
    };

    return (
      <ChartCard title="CPU温度">
        <Chart data={data} {...chartConfig}>
          <LineAdvance {...lineAdvanceConfig} />
          <Axis name="time" />
          <Axis name="rate" />
          <Tooltip title="使用率" />
        </Chart>
      </ChartCard>
    );
  }
}