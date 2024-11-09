using System.Collections.ObjectModel;
using System.ComponentModel;
using Energytrack.core.DTO;

namespace Energytrack.core.domain
{
    public class PessoaJuridica : Usuario
    {
        private string _cnpj;
        public PessoaJuridica() { }
        public PessoaJuridica(string cnpj, string nome, List<Medidor> medidorList) : base(nome, medidorList)
        {
            this._cnpj = cnpj;
        }

        public string GetCnpj() => this._cnpj;

        public void SetCnpj(string cnpf)
        {
            this._cnpj = cnpf;
        }
        public void CadastroUsuarioPessoaJuridica()
        {
            Console.Clear();
            Console.WriteLine("---------------------------");
            Console.WriteLine("Cadastro de Pessoa Jurídica");
            Console.WriteLine("---------------------------");

            Console.WriteLine("Informe o nome da empresa:");
            string nomeEmpresa = Console.ReadLine();

            Console.WriteLine("Informe o CNPJ da empresa:");
            string cnpj = Console.ReadLine();

            if (!ValidarCNPJ(cnpj))
            {
                Console.WriteLine("CNPJ inválido. O cadastro não pode ser realizado.");
                return;
            }
            Console.WriteLine();
            Console.WriteLine("---------------------------");
            Console.WriteLine("Cadastro do Medidor");
            Console.WriteLine("---------------------------");

            Console.WriteLine("Digite o apelido do medidor:");
            string apelidoMedidor = Console.ReadLine();

            Console.WriteLine("Digite o serial do medidor:");
            string serialMedidor = Console.ReadLine();

            Console.WriteLine("Cadastro do medidor e pessoa jurídica realizados com sucesso!");
            Console.WriteLine();

            Medidor medidor = new Medidor();
            medidor.SetApelido(apelidoMedidor);
            medidor.SetSerial(serialMedidor);

            PessoaJuridica pessoaJuridica = new PessoaJuridica();
            pessoaJuridica.SetCnpj(cnpj);
            pessoaJuridica.SetNome(nomeEmpresa);
            pessoaJuridica.SetMedidorList(medidor.MedidoresList());

            // SalvarCadastroPessoaJuridica(nomeEmpresa, cnpj, apelidoMedidor, serialMedidor);
            Insert(pessoaJuridica);
        }


        private bool ValidarCNPJ(string cnpj)
        {
            return cnpj.Length == 14 && cnpj.All(char.IsDigit);
        }
    }
}