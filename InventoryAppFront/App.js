import MainContainter from './src/MainContainter';
import ApplicationProviders from './src/contexts/ApplicationProviders';

function App(){
  return(
    <ApplicationProviders>
      <MainContainter/>
    </ApplicationProviders>
  );
}

export default App;