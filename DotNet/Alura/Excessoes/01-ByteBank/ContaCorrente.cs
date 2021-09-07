using System;

namespace ByteBank {
    public class ContaCorrente {
        private double _saldo = 100;
        private int Agencia { get; }
        public int Numero { get; set; }
        public Cliente Titular { get; set; }
        public static int TotalDeContasCriadas { get; private set; }
        public static double TaxaOperacao { get; private set; }
        public int ContadorSaquesNaoPermitidos { get; private set; }
        public int ContadorTransferenciasNaoPermitidas { get; private set; }

        public ContaCorrente(int numeroAgencia, int numeroConta) {
            if (numeroAgencia <= 0)
                throw new ArgumentException("O argumento agencia deve ser maior que zero.", nameof(numeroAgencia));
            if(numeroConta <= 0)
                throw new ArgumentException("O argumento numero deve ser maior que zero.", nameof(numeroConta));

            Agencia = numeroAgencia;
            Numero = numeroConta;

            TotalDeContasCriadas++;
            TaxaOperacao = 30 / TotalDeContasCriadas;
        }


        public double Saldo {
            get {
                return _saldo;
            }
            set {
                if (value >= 0)
                    _saldo = value;
            }
        }
               
        public void Sacar(double valor) {
            if (valor < 0)
                throw new ArgumentException("Valor de saque nao pode ser negativo", nameof(valor));
            if (valor > _saldo) {
                ContadorSaquesNaoPermitidos++;
                throw new SaldoInsuficienteException(_saldo, valor);
            }

            _saldo -= valor;
        }

        public void Depositar(double valor) {
            _saldo += valor;
        }

        public void Transferir(double valor, ContaCorrente contaDestino) {
            if (valor < 0)
                throw new ArgumentException("Valor invalido para a transferencia.", nameof(valor));
            try {
                Sacar(valor);
            } catch(SaldoInsuficienteException e) {
                ContadorTransferenciasNaoPermitidas++;
                throw new OperacaoFinanceiraException("Opercao nao realizada.", e);
            }
            
            contaDestino.Depositar(valor);
        }
    }
} 