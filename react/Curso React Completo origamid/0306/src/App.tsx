import React from 'react'
import './App.css'
import useFetch from './useFetch'

interface IProduto {
  id: string;
  nome: string;
}

function App() {
  const { request, data, loading, error } = useFetch<Array<IProduto>>();

  React.useEffect(() => {
    async function requesData() {
      const obj = await request('https://ranekapi.origamid.dev/json/api/produto')
      console.log(obj);
    }
    requesData();
  }, []);

  return (
    <>
      <h1>App</h1>
      {loading && <p>Carregando</p>}
      {(error) && <p>{error}</p>}
      <ListProduto data={data} />
    </>
  )
}

function ListProduto({ data }: { data: Array<IProduto> | null }) {
  if (!data) {
    return null;
  }
  return (
    <ul>
      {data.map((item) => <li key={item.id}>{item.nome}</li>)}
    </ul>
  );
}

export default App;
