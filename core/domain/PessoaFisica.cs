using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Energytrack.core.domain
{
    public class PessoaFisica : Usuario
    {
        private string _cpf;

        public PessoaFisica() { }

        public PessoaFisica(string cpf, string nome, List<Medidor> medidor) : base(nome, medidor)
        {
            this._cpf = cpf;
        }

        public string GetCpf() => this._cpf;

        public void SetCpf(string cpf)
        {
            this._cpf = cpf;
        }

        public void cadastroUsuarioPessoaFisica()
        {
            Console.WriteLine("------------------");
            Console.WriteLine("Cadastro de Pessoa Física");
            Console.WriteLine("------------------");
            Console.WriteLine("Informe o seu nome");
        }
    }
}