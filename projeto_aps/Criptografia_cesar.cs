using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_aps
{
    class Criptografia_cesar
    { 

        public string Criptografar(string mensagem)
        {
            Console.WriteLine("-Iniciando Cifra de César...");
            string msgFinal = CriptografarTextoToCesar(mensagem);
            Console.WriteLine("-Finalizando Cifra de César.");
            return msgFinal;
        }
        public string Descriptografar(string mensagem)
        {
            Console.WriteLine("-Iniciando decifragem em Cifra de César...");
            string msgFinal = DescriptografarCesarToTexto(mensagem);
            Console.WriteLine("-Finalizando decifragem em Cifra de César.");
            return msgFinal;
        }

        static string CriptografarTextoToCesar(string msg)
        {
            int i;
            string msgFinal = "";
            char[] words = new char[msg.Length];
            char[] newwords = new char[msg.Length];

            for (i = 0; i < msg.Length; i++)
            {
                char letter = msg[i];
                int asc = letter;
                int ready = asc - 3;
                letter = Convert.ToChar(ready);
                words[i] = letter;
            }

            for (i = msg.Length - 1; i >= 0; i--)
            {
                int asc2 = words[i];
                int ready2 = asc2 - i;
                if (ready2 < 0)
                {
                    ready2 = ready2 + 127;
                }
                words[i] = Convert.ToChar(ready2);
                newwords[i] = words[i];
            } 

            for (i = 0; i < newwords.Length; i++)
            {

                msgFinal = String.Concat(msgFinal, newwords[i]);
            }

            return msgFinal;
        }

        static string DescriptografarCesarToTexto(string msg)
        {
            int i;
            char[] words = new char[msg.Length];
            char[] newwords = new char[msg.Length];
            char[] converted = new char[msg.Length];

            for (i = 0; i < msg.Length; i++)
            {
                char letter = msg[i];
                int asc = letter; ;
                int ready = asc + i;
                if (ready > 128)
                {
                    ready = ready - 127;
                }
                words[i] = Convert.ToChar(ready);
            }
            for (i = msg.Length - 1; i >= 0; i--)
            {
                newwords[i] = words[i]; // invertendo de novo
            }

            for (i = 0; i < msg.Length; i++)
            {
                char letter = newwords[i];
                int asc = letter;
                int ready = asc + 3;
                letter = Convert.ToChar(ready);
                converted[i] = letter;
                newwords[i] = converted[i];
            }
             
            return new string(newwords);
        }
    }
}
