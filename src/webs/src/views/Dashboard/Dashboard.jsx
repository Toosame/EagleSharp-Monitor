import React from 'react';
import './Dashboard.scss';

import { List } from '../List/List';
import { Frames } from '../Frames/Frames';

import { Chart } from '../../components/Chart/Chart';

export class Dashboard extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <div className="container">
        <div className="es-grid">
          <div>
            <List />
          </div>
          <div>
            <Frames />
          </div>
          <div>
            <div>
              <div>摄像头</div>
              <div>散热器</div>
            </div>

            <div>
              操作键盘
          </div>
          </div>
          <div style={{ gridColumn: "1 / 4" }}>

            <div className="charts">
              <Chart title="CPU使用率"/>
              <Chart title="CPU温度"/>
              <Chart title="内存使用率"/>
              <Chart title="储存空间"/>
            </div>
          </div>
        </div>
      </div>
    );
  }
}