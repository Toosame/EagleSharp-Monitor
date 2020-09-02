import React from 'react';
import { Dashboard } from './views/Dashboard/Dashboard';

class App extends React.Component {
  constructor(props) {
    super(props)
    this.state = { }
  }

  render() {
    return (
      <Dashboard />
    );
  }
}

export default App;