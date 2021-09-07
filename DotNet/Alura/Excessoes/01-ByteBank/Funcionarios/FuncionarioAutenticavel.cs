using ByteBank.Sistemas;
using System;
using System.Collections.Generic;
using System.Text;

namespace ByteBank.Funcionarios {
    public abstract class FuncionarioAutenticavel : Funcionario, IAutenticavel {
        public string Senha { get; set; }
        public FuncionarioAutenticavel(string cpf, double salario) : base(cpf, salario) {
        }

        public bool Autenticar(string senha) {
            return Senha == senha;
        }
    }
}
