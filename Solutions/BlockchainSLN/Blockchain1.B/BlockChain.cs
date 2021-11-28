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
        public BlockChain(string hbcHash, string parentBlock, int algo, int size)
        {

        }
        public void AddHash(string str, string hash)
        {

        }
        public static BlockChain FromFile(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            else
            {
                return JsonSerializer.Deserialize<BlockChain>(File.ReadAllText(path));
            }
        }
        public void ToFile(string path)
        { 
            File.WriteAllText(path, JsonSerializer.Serialize<BlockChain>(this)); 
        }
    }
}
