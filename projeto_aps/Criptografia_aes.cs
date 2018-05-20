using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace projeto_aps
{
    class Criptografia_aes
    {

        static byte[] ConverterBase64ToByte(string base64)
        {
            return System.Convert.FromBase64String(base64);
        }

        public string Criptografar(string mensagem, string chave)
        {
            Console.WriteLine("-Iniciando processo de criptografia...");
            byte[] textoCifrado = CriptografarTextoToBytes(mensagem, ConverterBase64ToByte(chave));
            Console.WriteLine("-Finalizando processo de criptografia.");
            return Convert.ToBase64String(textoCifrado);
        }

        public string Descriptografar(string textoCifrado, string chave)
        {
            Console.WriteLine("-Iniciando processo de descriptografia...");
            string mensagem = DescriptografarBytesToMensagem(ConverterBase64ToByte(textoCifrado), ConverterBase64ToByte(chave));
            Console.WriteLine("-Finalizando processo de descriptografia.");
            return mensagem;
        }

        static byte[] CriptografarTextoToBytes(string mensagem, byte[] Key)
        {
            byte[] textoCifrado;
            byte[] IV;

            Console.WriteLine("-Instanciando AES...");
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;

                Console.WriteLine("-Criando Vetor de inicialização (IV) para mais segurança...");
                aesAlg.GenerateIV();
                IV = aesAlg.IV;

                Console.WriteLine("-Definindo modo de operação (CBC)...");
                aesAlg.Mode = CipherMode.CBC;

                Console.WriteLine("-Criando o objeto criptografador");
                var criptografia = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
 
                using (var msEncriptador = new MemoryStream())
                {
                    using (var csEncriptador = new CryptoStream(msEncriptador, criptografia, CryptoStreamMode.Write))
                    {
                        using (var swEncriptador = new StreamWriter(csEncriptador))
                        { 
                            swEncriptador.Write(mensagem);
                        }
                        Console.WriteLine("-Criando o vetor combinado(Cifra + IV)...");
                        textoCifrado = msEncriptador.ToArray();
                    }
                }
            }
            Console.WriteLine("-Finalizando AES (Dispose)...");

            Console.WriteLine("-Criando o vetor combinado (texto cifrado + IV)...");
            var cifraIvCombinada = new byte[IV.Length + textoCifrado.Length];
            Array.Copy(IV, 0, cifraIvCombinada, 0, IV.Length);
            Array.Copy(textoCifrado, 0, cifraIvCombinada, IV.Length, textoCifrado.Length);

            return cifraIvCombinada;
        }

        static string DescriptografarBytesToMensagem(byte[] textoIvCombinado, byte[] Key)
        {
            string textoClaro;

            Console.WriteLine("-Instanciando AES..."); 
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;

                Console.WriteLine("-Moldando os vetores...");
                byte[] IV = new byte[aesAlg.BlockSize/8];
                byte[] textoCifrado = new byte[textoIvCombinado.Length - IV.Length];

                Console.WriteLine("-Separando a mensagem cifrada e o vetor de inicialização...");
                Array.Copy(textoIvCombinado, IV, IV.Length);
                Array.Copy(textoIvCombinado, IV.Length, textoCifrado, 0, textoCifrado.Length);

                aesAlg.IV = IV;

                Console.WriteLine("-Definindo modo de operação (CBC)...");
                aesAlg.Mode = CipherMode.CBC;

                Console.WriteLine("-Criando o objeto descriptografador...");
                ICryptoTransform decodificador = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDesencriptador = new MemoryStream(textoCifrado))
                {
                    using (var csDesencriptador = new CryptoStream(msDesencriptador, decodificador, CryptoStreamMode.Read))
                    {
                        using (var srDesencriptador = new StreamReader(csDesencriptador))
                        {
                            Console.WriteLine("-Colocando os bytes descriptografados na mensagem...");
                            textoClaro = srDesencriptador.ReadToEnd();
                        }
                    }
                }
            }
            Console.WriteLine("-Finalizando AES (Dispose)...");

            return textoClaro;
        }
    }
}
