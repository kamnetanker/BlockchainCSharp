using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;//Подключаем алгоритмы шифрования и хеширования.


namespace Blockchain
{ 
    public class HashSequence
    {
        // Строка данных
        public string data { get; set; }
        // Хеш в формате Base64
        public string hash { get; set; }
        public HashSequence prevHash { get; set; }
        public HashSequence()
        {
            data = "";
            hash = "";
            prevHash = null;
        }
        public HashSequence(string data, string hash)
        {
            this.data = data;
            this.hash = hash;
            this.prevHash = null;
        }
    }
    //Перечисление алгоритмов хеширования
    public enum HashAlgorithms{
        MD5 = 1,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }
    public class HashController
    {
        // ToDo: добавить возможность выбора кодировки исходных строк


        // Переменная, куда будем пушить последовательность
        public HashSequence hs { get; set; }
        
        // Базовый класс для алгоритмов хеширования, предоставляет интерфейс создания экземпляра хеш алгоритма
        // и возможность для хеширования байтовых массивов через ComputeHash(byte[] arr);
        public HashAlgorithm ha { get; set; }

        // Делаем конструктор по умолчанию приватным, чтобы гарантировать, что инициализация алгоритма будет задана вручную
        // ебанёт? не должно.
        private HashController() { }

        //Метод, занимающийся инициализацией общих переменных. (Избегаем дублирования, как можем)
        private void initLocalVars(string method, bool init)
        { 
            ha = HashAlgorithm.Create(method);
            if (init)
            {
                hs = new HashSequence();
            }
        }

        // Конструктор с параметром алгоритма хеширования. По умолчанию будем использовать MD5
        public HashController(string hashMethod = "MD5")
        {
            this.initLocalVars(hashMethod, true); 
        }
        // Конструктор с перечислением, чтобы проще было выбирать алгоритм при инициализации
        public HashController(HashAlgorithms hashMethod = HashAlgorithms.MD5)
        {
            string method="";
            switch (hashMethod)
            {
                case HashAlgorithms.SHA1: method = "SHA1"; break;
                case HashAlgorithms.SHA256: method = "SHA256"; break;
                case HashAlgorithms.SHA384: method = "SHA384"; break;
                case HashAlgorithms.SHA512: method = "SHA512"; break;
                default:
                    method = "MD5"; break;
            }
            this.initLocalVars(method, true); 
        }
        // Инициализация нового блока хеширования, в качестве результата возвращает старый блок
        public HashSequence InitNewBlock(string data = "")
        {
            HashSequence linkToOldBlock = this.hs;

            //У первого блока нет никакого предыдущего хеша, поэтому просто считаем хеш переданных данных
            byte[] hash = ha.ComputeHash(Encoding.UTF8.GetBytes(data));//Получаем набор байт хеша
            string shash = Convert.ToBase64String(hash);//Переводим набор байт хеша в 64-тиричное строковое представление base64

            this.hs = new HashSequence(data, shash);//Последовательность создана с первым элементом.

            return linkToOldBlock;//Возвращаем старый блок.
        }
        
        

        // Метод добавления новой строки в последовательность хешей
        public HashSequence AddNewString(string data)
        { 
            // Подготавливаем массивы байт для хеширования
            byte[] bdata = Encoding.UTF8.GetBytes(data);
            byte[] bprevhash = Convert.FromBase64String(hs.hash);

            // Конкатенируем массивы
            bdata = bdata.Concat(bprevhash).ToArray();            

            //Вычисляем хеш и переводим его в строку
            byte[] hash = ha.ComputeHash(bdata);
            string shash = Convert.ToBase64String(hash);

            // Создаём новый блок хеша
            HashSequence newHS = new HashSequence(data, shash);
            newHS = this.hs;
            this.hs = newHS;

            return this.hs;
        }
             
    }
}
