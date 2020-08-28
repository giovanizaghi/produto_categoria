using Konscious.Security.Cryptography;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Entities.Helpers.Security
{
    public class SecurityArgon2
    {
        public int DegreeOfParallelism { get; set; }

        public int Iterations { get; set; }

        public int MemorySize { get; set; }

        public SecurityArgon2(int degreeOfParallelism, int iterations, int memorySize)
        {
            DegreeOfParallelism = degreeOfParallelism;
            Iterations = iterations;
            MemorySize = memorySize;
        }

        public SecurityArgon2()
        {
            /*
             * Dados servidor:
             * Windows Server 64x
             * Qtde Processadores: 2
             * Processador: Intel Xeon CPU E5-2620 2.00GHz
             * Núcleos por processador: 2
             * Total de núcleos: 4
             * Memória RAM: 4GB
             * 
             * Após testes em Servidor, foi verificado que a melhor configuração é:
             * 
             * Paralelismo: 8 cores
             * Iterações: 4 loops
             * Memória usada: 256 MB
             * 
             * Referências:
             * https://www.twelve21.io/how-to-choose-the-right-parameters-for-argon2/
             * https://www.twelve21.io/how-to-use-argon2-for-password-hashing-in-csharp/
             */

            DegreeOfParallelism = 8;
            Iterations = 4;
            MemorySize = 256 * 1024;
        }

        public void CreatePassword(string password, out byte[] hash, out byte[] salt)
        {
            salt = CreateSalt();
            hash = HashPassword(password, salt);
        }

        public bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }

        private byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = DegreeOfParallelism;
            argon2.Iterations = Iterations;
            argon2.MemorySize = MemorySize;

            return argon2.GetBytes(16);
        }
    }
}
