using System.Security.Cryptography;
using System.Text;

namespace Wordle.Dll.Services
{
    public class CryptographyService
    {
        private readonly Aes _aes;
        public CryptographyService()
        {
            _aes = Aes.Create();
            _aes.Padding = PaddingMode.ISO10126;
            _aes.Key = new byte[] { 1, 2, 3, 5, 4, 48, 9, 54, 5, 22, 2, 4, 87, 52, 35, 54, 21, 35, 78, 123, 2, 4, 87, 52, 35, 54, 21, 35, 78, 123, 23, 3 };
        }

        public string Encrypt(int number)
        {
            var encryptor = _aes.CreateEncryptor();
            var numberInBytes = BitConverter.GetBytes(number);
            var chiperText = encryptor.TransformFinalBlock(numberInBytes, 0, numberInBytes.Length);
            return Convert.ToBase64String(chiperText);
        }

        public int DecryptToNumber(string data)
        {
            var decryptor = _aes.CreateDecryptor();
            var dataInBytes = Convert.FromBase64String(data);
            var chiperText = decryptor.TransformFinalBlock(dataInBytes, 0, dataInBytes.Length);
            return BitConverter.ToInt32(chiperText);
        }
    }
}
