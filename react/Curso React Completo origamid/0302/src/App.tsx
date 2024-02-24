
import React from 'react';
import './App.css'

interface IProduto {
  id: string;
  nome: string;
  preco: number;
}

function App() {
  const [dados, setDados] = React.useState<IProduto | null>(null);

  React.useEffect(() => {
    if (dados) {
      localStorage.setItem('produto', dados.id);
    }
  }, [dados]);

  React.useEffect(() => {
    const produtoId = localStorage.getItem('produto');
    produtoId && fetchProduto(produtoId);
  }, []);

  async function fetchProduto(nomeProduto: string) {
    const baseUrl = 'https://ranekapi.origamid.dev/json/api/produto/';

    const req = await fetch(baseUrl + nomeProduto);
    const json = await req.json();
    setDados(json);
  }

  return (
    <>
      <h1>Preferência: {dados && dados.id}</h1>
      <div style={{ display: 'flex', gap: '1rem' }}>
        <button onClick={() => fetchProduto('notebook')}>notebook</button>
        <button onClick={() => fetchProduto('smartphone')}>smartphone</button>
      </div>

      {dados && <Produto produto={dados} />}
    </>
  )
}

interface ProdutoProps {
  produto: IProduto
}

function Produto(props: ProdutoProps) {
  const { produto } = props;
  return (
    <>
      <h2>{produto.nome}</h2>
      <p>Preço R$ {produto.preco}</p>
    </>
  )
}



export default App
