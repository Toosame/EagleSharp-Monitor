import React from 'react';
import { HelloWorld } from './HelloWorld'

 class App extends React.Component {
  constructor(props) {
    super(props)
  }

  render() {
    return (
      <div>
        Learn React
        <HelloWorld />
      </div>
    );
  }
}

export default App;