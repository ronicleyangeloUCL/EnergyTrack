using System;
using System.Text;

class Program
{
    public static void Main(String[] args)
    {
        // string caminhoArquivo = "resources/teste.txt";
        // Arquivo arquivo = new Arquivo(caminhoArquivo);
        // arquivo.LeituraArquivo();

        MenuService menuService = new MenuService();
        menuService.Menu();

    }
}
