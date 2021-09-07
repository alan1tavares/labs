using ByteBank.Sistemas;
using System;
using System.Collections.Generic;
using System.Text;

namespace ByteBank.Funcionarios {
    public class Diretor : FuncionarioAutenticavel {
        public string Senha { get; set; }

        public Diretor(string cpf) : base(cpf, 5000) {

        }

        public override double GetBonificacao() {
            return Salario * 0.5;
        }

        public override void AumentarSalario() {
            Salario *= 1.15;
        }
    }
}