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
          <div className="frames">
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

            <div className="es-flex">
              <div className="es-flex-item">
                <Chart title="CPU使用率"></Chart>
              </div>
              <div className="es-flex-item">
                <Chart title="CPU温度" />
              </div>
              <div className="es-flex-item">
                <Chart title="内存使用率" />
              </div>
              <div className="es-flex-item">
                <Chart title="储存空间" />
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}