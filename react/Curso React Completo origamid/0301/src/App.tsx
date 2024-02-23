import React from 'react'
import './App.css'

interface Produto {
  nome: string;
  preco: number;
}

function App() {

  const [dados, setDados] = React.useState<Produto | null>(null);
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
          dados && <Produtos produto={dados} />
      }
    </>
  )
}

interface ProdutosProps {
  produto: Produto
}

function Produtos(props: ProdutosProps) {
  const { produto } = props;
  return (
    <div>
      <p>{produto.nome}</p>
      <p>Preço: R$ {produto.preco}</p>
    </div>

  )
}

export default App
