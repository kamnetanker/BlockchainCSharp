using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain2.B
{
    class BlockChain
    {
        public string blockHash { get; set; }
        public string hbcCreator { get; set; }
        public string parentHash { get; set; }
        public string timeStamp { get; set; }
        public string hashAlgo { get; set; }
        public int size { get; set; }
        public int iter { get; set; }
        public string[] hashes { get; set; }
        public string[] strs { get; set; }
        public BlockChain(string hbcHash, string parentHash)
        {

        }
        public void AddHash(string str, string hash)
        {

        }
        
    }
}
