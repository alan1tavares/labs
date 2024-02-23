import React from 'react'
import './App.css'

interface Item {
  nome: string;
  preco: number;
}

function App() {

  const [dados, setDados] = React.useState<Item | null>(null);
  const [isCarregando, setIsCarregando] = React.useState<boolean>(false);
  const baseUrl = 'https://ranekapi.origamid.dev/json/api/produto/'

  async function fetchData(produto: string) {
    setIsCarregando(true);
    const resp = await fetch(baseUrl + produto);
    const json = await resp.json();
    setDados(json);
    setIsCarregando(false);
  }

  return (
    <>
      <h1>
        Exercicio 0301
      </h1>
      <div style={{
        display: 'flex',
        gap: '2rem'
      }}>
        <button onClick={() => fetchData('notebook')}>notebook</button>
        <button onClick={() => fetchData('smartphone')}>smartphone</button>
        <button onClick={() => fetchData('tablet')}>tablet</button>
      </div>
      {
        isCarregando ?
          'Carregando' :
          <div>
            <p>{dados?.nome}</p>
            <p>{dados?.preco}</p>
          </div>
      }
    </>
  )
}

export default App
