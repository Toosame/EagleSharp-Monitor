import React from 'react';
import './Dashboard.scss';

import { List } from '../List/List';
import { Frames } from '../Frames/Frames';

import { CpuUsageRate } from '@components/content/CpuUsageRate/CpuUsageRate';
import { CpuTemperature } from '@components/content/CpuTemperature/CpuTemperature';
import { MemoryStatus } from '@components/content/MemoryStatus/MemoryStatus';
import { StorageSpace } from '@components/content/StorageSpace/StorageSpace';

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
          <div>
            <div className="es-flex">
              <div className="es-flex-item">
                <CpuUsageRate />
              </div>
              <div className="es-flex-item">
                <CpuTemperature />
              </div>
              <div className="es-flex-item">
                <MemoryStatus />
              </div>
              <div className="es-flex-item">
                <StorageSpace />
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}