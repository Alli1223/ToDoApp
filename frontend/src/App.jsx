import { useEffect, useState } from 'react'
import keycloak from './keycloak'

function App() {
  const [todos, setTodos] = useState([])
  const [title, setTitle] = useState('')

  useEffect(() => {
    keycloak.init({ onLoad: 'login-required' }).then(authenticated => {
      if (authenticated) {
        fetch('/api/todo', {
          headers: { Authorization: `Bearer ${keycloak.token}` }
        }).then(r => r.json()).then(setTodos)
      }
    })
  }, [])

  const addTodo = () => {
    fetch('/api/todo', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${keycloak.token}`
      },
      body: JSON.stringify(title)
    }).then(r => r.json()).then(item => {
      setTodos([...todos, item])
      setTitle('')
    })
  }

  return (
    <div>
      <h1>ToDo List</h1>
      <input value={title} onChange={e => setTitle(e.target.value)} />
      <button onClick={addTodo}>Add</button>
      <ul>
        {todos.map(t => (
          <li key={t.id}>{t.title}</li>
        ))}
      </ul>
    </div>
  )
}

export default App
