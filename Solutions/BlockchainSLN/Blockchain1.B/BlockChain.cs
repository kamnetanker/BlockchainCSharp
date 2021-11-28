using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blockchain2.B
{
    class BlockChain
    {
        // Идентификатор блока
        public string blockHash { get; set; }
        // Идентификатор создателя
        public string hbcCreator { get; set; }
        // Идентификатор родительского блока
        public string parentHash { get; set; }
        // Время создания блока
        public string timeStamp { get; set; }
        // Идентификатор алгоритма хэширования
        public int hashAlgo { get; set; }
        // Размер блока
        public int size { get; set; }
        // Итератор
        public int iter { get; set; }
        // Массив хэшей
        public string[] hashes { get; set; }
        // Массив строк
        public string[] strs { get; set; } 
        public BlockChain()
        {

        }
        public BlockChain(string hbcHash, string parentBlock, int algo, int size)
        {
            // Указываем того, кто подписывает блок
            this.hbcCreator = hbcHash;
            // Указываем родительский блок
            this.parentHash = parentBlock;
            // Указываем тип алгоритма, которым подписан блок
            this.hashAlgo = algo;
            // Указываем размер блока
            this.size = size;
            // выделяем память под хэши и строки
            this.hashes = new string[size];
            this.strs = new string[size];
            // Устанавливаем итератор в начало
            this.iter = 0;
            // Устанавливаем время создания блока
            this.timeStamp = DateTime.Now.ToString(); 
        }
        public void AddHash(string str, string hash)
        {
            // Записываем в позицию указателя новый хэш и новую строку
            this.hashes[this.iter] = hash;
            this.strs[this.iter] = str;
            // Смещаем указатель
            this.iter++;
        }
        public static BlockChain FromFile(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            else
            {
                //Console.WriteLine(File.ReadAllText(path));
                return JsonSerializer.Deserialize<BlockChain>(File.ReadAllText(path));
            }
        }
        public void ToFile(string path)
        { 
            File.WriteAllText(path, JsonSerializer.Serialize<BlockChain>(this)); 
        }
    }
}
