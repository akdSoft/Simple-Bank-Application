import { useState, useEffect } from 'react'

function App() {
  const [users, setUsers] = useState([]);
  const [searchId, setSearchId] = useState('');
  const [userById, setUserById] = useState(null);

  const makeAPICall = async () => {
    const url = 'http://localhost:5280/api/User';
    const response = await fetch(url);
    const body = await response.json();
    setUsers(body);
  };

  const searchUserById = async () => {
    if(!searchId) return;
    const url = `http://localhost:5280/api/User/${searchId}`;
    const response = await fetch(url);
    const body = await response.json();
    setUserById(body);
  }


  return (
      <div style={{display: 'flex', gap: '20px'}}>
        <div style={{flex: 1}}>
          <button onClick={makeAPICall}>Get All Users</button>
          {
            users.map((user) => (
                <div key={user.id}>
                  <h3>{user.name}</h3>
                </div>
            ))}
        </div>

        <div style={{flex: 1}}>
          <input
              type="number"
              placeholder="Enter ID"
              value={searchId}
              onChange={(e) => setSearchId(e.target.value)}
          />
          <button onClick={searchUserById}>Search User by ID</button>


        {userById && (
            <div style={{marginTop: '20px', flex: 1}}>
              <h3>{userById.name}</h3>
            </div>
        )}
      </div>
      </div>
  )
}

export default App
