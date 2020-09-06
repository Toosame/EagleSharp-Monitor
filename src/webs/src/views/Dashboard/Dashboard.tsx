import React from 'react';
import './Dashboard.scss';

import { List } from '../List/List';
import { Frames } from '../Frames/Frames';

import { CpuUsageRate } from '@components/content/CpuUsageRate/CpuUsageRate';
import { CpuTemperature } from '@components/content/CpuTemperature/CpuTemperature';
import { MemoryStatus } from '@components/content/MemoryStatus/MemoryStatus';
import { StorageSpace } from '@components/content/StorageSpace/StorageSpace';
import { Header } from 'views/Header/Header';

export class Dashboard extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <div>
        <Header />
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
              <CpuUsageRate />
            </div>
            <div>
              <CpuTemperature />
            </div>
            <div>
              <MemoryStatus />
            </div>
            <div>
              <StorageSpace />
            </div>
          </div>
        </div>
      </div>
    );
  }
}