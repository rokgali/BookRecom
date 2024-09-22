import { useState } from 'react';
import './App.css';
import Routes from './routes/routes';

function App() {
  const [userIsAuthenticated, setUserIsAuthenticated] = useState<boolean>(true);
  const [userRoles, setUserRoles] = useState<string[]>([]);

  const [serverResponseLoading, setServerResponseLoading] = useState<boolean>(true);

  return (
    <>
      <Routes userIsAuthenticated={userIsAuthenticated} userRoles={userRoles} />
    </>
  );
}

export default App;
