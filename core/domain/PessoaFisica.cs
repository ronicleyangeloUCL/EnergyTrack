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
            Console.Clear();
            Console.WriteLine("------------------");
            Console.WriteLine("Cadastro de Pessoa Física");
            Console.WriteLine("------------------");
            Console.WriteLine("Informe o seu nome");
            string nome = Console.ReadLine();

            string cpf = string.Empty;

            while (true)
            {
                Console.WriteLine("Informe o seu CPF: ");
                cpf = Console.ReadLine();

                if (ValidarCPF(cpf))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("CPF inválido. Tente novamente.");
                }
            }

            List<Medidor> listaMedidores = new List<Medidor>();

            string continuar = "S";
            while (continuar.ToUpper() == "S")
            {
                Console.Clear();
                Console.WriteLine("Digite o apelido do medidor:");
                string apelidoMedidor = Console.ReadLine();

                Console.WriteLine("Digite o serial do medidor:");
                string serialMedidor = Console.ReadLine();

                Medidor medidor = new Medidor(apelidoMedidor, serialMedidor);

                listaMedidores.Add(medidor);
                Console.WriteLine();
                Console.WriteLine("Medidor cadastrado com sucesso!");
                Console.WriteLine("Deseja cadastrar outro medidor? (S/N)");

                continuar = Console.ReadLine()?.ToUpper(); // Garantir que a entrada será maiúscula
                while (continuar != "S" && continuar != "N")
                {
                    Console.WriteLine("Erro: Entrada inválida. Digite S para sim ou N para não.");
                    continuar = Console.ReadLine()?.ToUpper();
                }
            }

            Console.WriteLine();
            Console.WriteLine("Cadastro finalizado.");
            Console.WriteLine("--------------------");

            Console.WriteLine("Lista de Medidores cadastrados:");
            foreach (var medidor in listaMedidores)
            {
                Console.WriteLine($"Apelido: {medidor.GetApelido()}, Serial: {medidor.GetSerial()}");
            }

            PessoaFisica pessoaFisica = new PessoaFisica();
            pessoaFisica.SetCpf(cpf);
            pessoaFisica.SetNome(nome);
            pessoaFisica.SetMedidorList(listaMedidores);

            Insert(pessoaFisica);

        }
        private bool ValidarCPF(string cnpj)
        {
            return cnpj.Length == 11 && cnpj.All(char.IsDigit);
        }

        private void Insert(PessoaFisica pessoaFisica)
        {
            string path = "resources/usuario.txt";

            UsuarioPessoaFisicaDTO<PessoaFisica> dados = new UsuarioPessoaFisicaDTO<PessoaFisica>(pessoaFisica);

            Arquivo<UsuarioPessoaFisicaDTO<PessoaFisica>> arquivo = new Arquivo<UsuarioPessoaFisicaDTO<PessoaFisica>>(path);
            Console.WriteLine("ARQUIVO ", arquivo);
            arquivo.EscritaArquivo("Cadastro de Usuário Pessoa Física", new List<UsuarioPessoaFisicaDTO<PessoaFisica>> { dados });
        }
    }
}