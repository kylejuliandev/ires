import { useEffect, useState } from 'react'
import './App.css'

interface Person {
  id: string
  given_name: string
  family_name: string
  nickname?: string | null
  gender?: string
  date_of_birth: string
  notes?: string[]
}

interface GetPeopleResponse {
  people: Person[]
}

function App() {
  const [people, setPeople] = useState<Person[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    setLoading(true)
    fetch('/api/v1/people')
      .then(async (res) => {
        if (!res.ok) throw new Error(`${res.status} ${res.statusText}`)
        const data = (await res.json()) as GetPeopleResponse
        setPeople(data.people ?? [])
      })
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false))
  }, [])

  return (
    <>
      <h1>People</h1>

      {loading ? (
        <p>Loading people…</p>
      ) : error ? (
        <p style={{ color: 'red' }}>Error: {error}</p>
      ) : people.length === 0 ? (
        <p>No people found.</p>
      ) : (
        <div style={{ overflowX: 'auto' }}>
          <table className="people-table" style={{ borderCollapse: 'collapse', width: '100%' }}>
            <thead>
              <tr>
                <th style={{ textAlign: 'left', padding: 8 }}>Name</th>
                <th style={{ textAlign: 'left', padding: 8 }}>Nickname</th>
                <th style={{ textAlign: 'left', padding: 8 }}>Gender</th>
                <th style={{ textAlign: 'left', padding: 8 }}>Date of Birth</th>
                <th style={{ textAlign: 'left', padding: 8 }}>Notes</th>
              </tr>
            </thead>
            <tbody>
              {people.map((p) => (
                <tr key={p.id}>
                  <td style={{ padding: 8 }}>
                    {p.given_name} {p.family_name}
                  </td>
                  <td style={{ padding: 8 }}>{p.nickname ?? '—'}</td>
                  <td style={{ padding: 8 }}>{p.gender ?? '—'}</td>
                  <td style={{ padding: 8 }}>
                    {isValidDate(p.date_of_birth) ? new Date(p.date_of_birth).toLocaleDateString() : p.date_of_birth}
                  </td>
                  <td style={{ padding: 8 }}>
                    {p.notes && p.notes.length > 0 ? (
                      <ul style={{ margin: 0, paddingLeft: 16 }}>
                        {p.notes.map((n, i) => (
                          <li key={i}>{n}</li>
                        ))}
                      </ul>
                    ) : (
                      '—'
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

    </>
  )

  function isValidDate(d: string) {
    const dt = new Date(d)
    return !Number.isNaN(dt.getTime())
  }
}

export default App
