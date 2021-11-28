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
        public string blockHash { get; set; }
        public string hbcCreator { get; set; }
        public string parentHash { get; set; }
        public string timeStamp { get; set; }
        public int hashAlgo { get; set; }
        public int size { get; set; }
        public int iter { get; set; }
        public string[] hashes { get; set; }
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
