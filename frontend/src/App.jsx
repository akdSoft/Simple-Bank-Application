import { useState, useEffect } from 'react'

function App() {
  const [data, setData] = useState(null);

  useEffect(() => {
      fetch('http://localhost:5280/api/test')
          .then(response => response.text())
          .then(text => setData(text))
          .catch(error => console.error(error));
  }, []);

  return (
    <>
        {JSON.stringify(data, null, 2)}
    </>
  )
}

export default App
