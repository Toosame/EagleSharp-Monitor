import React from 'react';
import './Dashboard.scss';

import { List } from '../List/List';
import { Frames } from '../Frames/Frames';

import { Charts } from '../../components/Charts/Charts';

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
                <Charts title="CPU使用率"></Charts>
              </div>
              <div className="es-flex-item">
                <Charts title="CPU温度" />
              </div>
              <div className="es-flex-item">
                <Charts title="内存使用率" />
              </div>
              <div className="es-flex-item">
                <Charts title="储存空间" />
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}