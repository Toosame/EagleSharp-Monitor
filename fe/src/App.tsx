import React from 'react';
import { Home } from './views/Home'

class App extends React.Component {
  constructor(props) {
    super(props)
    this.state = { }
  }

  render() {
    return (
      <Home />
    );
  }
}

export default App;