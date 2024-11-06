using System;

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
            Console.WriteLine("2 - Medidor");
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
                
                break;
            case 2:
                Console.WriteLine("Medidor selecionado.");
                break;
            case 3:
                Console.WriteLine("Visualizar Consumo.");
                break;
            case 4:
                Console.WriteLine("Gerar Relatório.");
                break;
            case 0:
                Console.WriteLine("Saindo...");
                break;
            default: 
                Console.WriteLine("Nenhuma alternativa foi válida.");
                break;
        }

        // Pausa para que o usuário veja o resultado antes de continuar
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}
