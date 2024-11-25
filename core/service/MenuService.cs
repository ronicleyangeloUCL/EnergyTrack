using System;
using Energytrack.core.domain;

class MenuService
{
    private int _escolha = 0;

    public MenuService() {}

    public void Menu()
    {
        do 
        {
            Console.Clear();
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Escolha uma opção");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("1 - Usuário");
            Console.WriteLine("2 - Registrar Medição");
            Console.WriteLine("3 - Visualizar Consumo");
            Console.WriteLine("4 - Relatório");
            Console.WriteLine("0 - Sair");
            Console.Write("Digite sua escolha: ");

            if (int.TryParse(Console.ReadLine(), out _escolha))
            {
                this.MenuInterativo(_escolha);
            }
            else
            {
                Console.WriteLine("Entrada inválida. Por favor, digite um número.");
            }
        } while (_escolha != 0);
    }

    private void MenuInterativo(int escolha)
    {
        Console.WriteLine("Resultado: {0}", escolha);

        switch (escolha)
        {
            case 1: 
                int escolhaPessoa;
                Console.Clear();
                Console.WriteLine("1 - Pessoa Física");
                Console.WriteLine("2 - Pessoa Jurídica");
                if(int.TryParse(Console.ReadLine(), out escolhaPessoa))
                {
                    switch(escolhaPessoa)
                    {
                        case 1 : 
                            PessoaFisica pessoaFisica = new PessoaFisica();
                            pessoaFisica.cadastroUsuarioPessoaFisica();
                            break;
                        case 2 : 
                            PessoaJuridica pessoaJuridica = new PessoaJuridica();
                            pessoaJuridica.CadastroUsuarioPessoaJuridica();
                            break;
                        default: 
                            Console.WriteLine("Comando inválido");
                            break;
                    }
                } 
                break;
            case 2:
                Medicao medicao = new Medicao();
                medicao.RegistraMedidor();
                Console.WriteLine("Medidor selecionado.");
                break;
            case 3:
                Console.WriteLine("Visualizar Consumo.");
                break;
            case 4:
                Fatura fatura = new Fatura();
                fatura.ProcessarFatura();
                Console.WriteLine("Gerar Relatório.");
                break;
            case 0:
                Console.WriteLine("Saindo...");
                break;
            default: 
                Console.WriteLine("Nenhuma alternativa foi válida.");
                break;
        }

        Console.WriteLine("Pressione qualquer tecla para continuar...");
    }
}
