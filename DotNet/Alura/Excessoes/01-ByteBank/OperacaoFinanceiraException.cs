using System;
using System.Collections.Generic;
using System.Text;

namespace ByteBank {
    public class OperacaoFinanceiraException : Exception{
        public OperacaoFinanceiraException() { }
        public OperacaoFinanceiraException(String mensagem) : base(mensagem) { }
        public OperacaoFinanceiraException(string mensagem, Exception excecaoInterna) 
            : base(mensagem, excecaoInterna) { }
    }
}
