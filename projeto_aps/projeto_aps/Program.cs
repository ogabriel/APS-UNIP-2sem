using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace projeto_aps
{
    class Program
    {
        // String que sempre é exibida no final do loop
        static string mensagemFinal = null;
        static void Main(string[] args)
        {
            // Cabeçalho do programa
            Console.WriteLine(" _   _  _   _  _  ____\n| | | || \\ | ||	||    |\n| |_| ||  \\| || ||  __|\n|_____||_|\\__||_||_|");
            Console.WriteLine("CIÊNCIA DA COMPUTAÇÃO\nAPS 2017 - 2ºSEMESTRE\n ");

            // Loop que roda até o usuário queira sair
            bool sair = false;
            while (!sair)
            {
                
                // mostrando as opcoes
                Console.Write("Opções de criptografia: \n1 - Criptografar e descriptografar mensagem\n2 - Criptografar mensagem\n3 - Descriptografar mensagem cifrada\n4 - Sair\nOpção: ");
                string option = Console.ReadLine();

                //if que verifica a opção digitada
                if (option == "1")
                {
                    Console.Write("Digite a mensagem a ser criptografada e descriptografada:\n>");
                    string mensagem = Console.ReadLine();

                    if (ChecarMensagem(mensagem))
                    {
                        string chave = GerarChave();
                        // Instancia a classe com a Cifra AES
                        Criptografia_aes aes = new Criptografia_aes();
                        string mensCriptografada = aes.Criptografar(mensagem, chave);
                        string mensDescriptografada = aes.Descriptografar(mensCriptografada, chave);

                        mensagemFinal = "Mensagem Criptografada (Base64): " + mensCriptografada + "\nMensagem Descriptografada: " + mensDescriptografada + "\nChave usada: " + chave;
                    }                                                  
                }
                else if (option == "2")
                {
                    Console.Write("Digite a mensagem a ser criptografada:\n>");
                    string mensagem = Console.ReadLine();

                    if (ChecarMensagem(mensagem))
                    {
                        string chave = GerarChave();
                        // Instancia a classe com a Cifra AES
                        Criptografia_aes aes = new Criptografia_aes();
                        string mensCriptografada = aes.Criptografar(mensagem, chave);

                        mensagemFinal = "Mensagem Criptografada (Base64): " + mensCriptografada + "\nChave usada: " + chave; 
                    }
                }
                else if (option == "3")
                {
                    // Pede os dados necessários para descriptografar
                    Console.Write("Digite a mensagem cifrada a ser descriptografada:\n>");
                    string mensCriptografada = Console.ReadLine();
                    Console.Write("Digite a chave para descriptografar(Base64):\n>");
                    string chave = Console.ReadLine();

                    if (ChecarMensagem(mensCriptografada) && ChecarMensagem(chave))
                    {
                        // Instancia a classe com a Cifra AES
                        Criptografia_aes aes = new Criptografia_aes();
                        string mensDescriptografada = aes.Descriptografar(mensCriptografada, chave);

                        mensagemFinal = "Mensagem Descriptografada: " + mensDescriptografada; 
                    }
                }
                else if (option == "4")
                {
                    mensagemFinal = "Tchau!";
                    sair = true;
                }
                else
                {
                    mensagemFinal = "Essa opção não esta disponível.";                   
                }
                ExibirMensagem(mensagemFinal);
            }
        }

        // Gera a chave usada na criptografia
        static string GerarChave()
        {
            Console.WriteLine("\n-Criando chave.");
            var random = new RNGCryptoServiceProvider();
            var key = new byte[16];
            Console.WriteLine("-Preenchendo a chave com uma matriz de bytes com uma sequência criptograficamente forte de valores aleatórios...");
            random.GetBytes(key);


            return Convert.ToBase64String(key);
        }

        // Método que checa se a mensagem esta correta
        static bool ChecarMensagem(string mensagem)
        {
            if (mensagem.Length == 0 || mensagem == null || mensagem == "")
            {
                mensagemFinal = "Dados requeridos não foram digitados.";
                return false;
            }
            if (mensagem.Length > 0 && mensagem.Length <= 128)
            {
                return true;
            }
            else
            {
                mensagemFinal = "Mensagem excede o limite de caracteres.";
                return false;
            }
        }

        // Exibe a mensagem separada por símbulos
        static void ExibirMensagem(string mensagem)
        {
            Console.WriteLine("\n" + new String('=', 75));
            Console.WriteLine(mensagem);
            Console.WriteLine(new String('=', 75) + "\n");
        }
    }
}
