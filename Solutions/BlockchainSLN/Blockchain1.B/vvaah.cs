using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain2.B
{
    class vvaah
    {
        public HashAlgorithm hA;
        public Encoding encoder;
        public vvaah(HashAlgorithm ha, Encoding enc)
        {
            if (ha != null)
            {
                this.hA = ha;
            }
            else
            {
                this.hA = MD5.Create();
            }
            
            if (enc != null)
            {
                this.encoder = enc;
            }
            else
            {
                this.encoder = Encoding.UTF8;
            }
            
        }
        public string ComputeHash(string str) 
        {
            // Подготавливаем массивы байт для хеширования
            byte[] bdata = encoder.GetBytes(str);
            // Вычисляем хэш строки
            byte[] result = hA.ComputeHash(bdata);
            // Переводим массив байт в base64 строку
            return Convert.ToHexString(result);

        }
    }
}
