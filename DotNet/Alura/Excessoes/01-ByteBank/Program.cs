using ByteBank.Funcionarios;
using ByteBank.Sistemas;
using System;

namespace ByteBank {
    class Program {
        static void Main(string[] args) {
            Cliente gabriela = new Cliente();
            gabriela.Nome = "Gabriela";

            Cliente bruno = new Cliente();
            bruno.Nome = "Bruno";

            
            try {
                ContaCorrente conta = new ContaCorrente(123, 10);
                ContaCorrente conta2 = new ContaCorrente(1234, 100);

                conta.Transferir(1000, conta2);

            } catch (OperacaoFinanceiraException e) {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("\n");
                Console.WriteLine(e.InnerException.StackTrace);
                Console.WriteLine("\n");
                Console.WriteLine(e.Message);
            }

            ContaCorrente contaDaGabriela = new ContaCorrente(123, 123);
            contaDaGabriela.Titular = gabriela;

            ContaCorrente contaDoBruno = new ContaCorrente(321, 321);
            contaDoBruno.Titular = bruno;

            contaDaGabriela.Saldo = -10;

            contaDaGabriela.Sacar(50);
            contaDaGabriela.Depositar(500);

            contaDaGabriela.Transferir(200, contaDoBruno);

            Console.WriteLine(contaDaGabriela.Saldo);
            Console.WriteLine(contaDoBruno.Saldo);
            Console.WriteLine(ContaCorrente.TotalDeContasCriadas);

            Console.WriteLine("###########");

            GerenciadorBonificacao gerenciadorBonificacao = new GerenciadorBonificacao();

            Funcionario carlos = new GerenteDeConta("123");
            carlos.Nome = "Carlos";
            carlos.AumentarSalario();

            gerenciadorBonificacao.Registrar(carlos);
            
            Diretor roberta = new Diretor("321");
            roberta.Nome = "Roberta";
            roberta.AumentarSalario();

            gerenciadorBonificacao.Registrar(roberta);

            Console.WriteLine(carlos.GetBonificacao());
            Console.WriteLine(roberta.GetBonificacao());
            Console.WriteLine(gerenciadorBonificacao.GetTotalBonificacao());
            Console.WriteLine(Funcionario.TotalDeFuncionarios);

            Console.WriteLine("#######");

            SistemaInterno sistemaInterno = new SistemaInterno();
            ParceiroComercial parceiroComercial = new ParceiroComercial();
            parceiroComercial.Senha = "123456";

            sistemaInterno.Logar(parceiroComercial, "123456");
            sistemaInterno.Logar(roberta, "123");


            Console.ReadLine();
            
        }
    }
}
