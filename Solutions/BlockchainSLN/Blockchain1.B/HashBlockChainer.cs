using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain2.B
{
    class HashBlockChainer
    {
        public string hbcId { get; set; }
        public string blockDestination { get; set; }
        public string lastBlockHash { get; set; }
        public int blockSize { get; set; }
        public List<string> signedBlocks { get; set; }
        public BlockChain curBlock { get; set; }
        public vvaah hasher;
        public HashBlockChainer(Config conf)
        {

        }
        public void SignBlock()
        {

        }
        public void CreateBlock(Config conf)
        {

        }
        public void SaveBlock()
        {

        }
        public void SaveStatement()
        {

        }
        public void Diff(string blockA, string blockB, out List<string> diff)
        {
            List<string> diffLog = new List<string>();

            diff = diffLog;
            return;
        }
        public void AddString(string str)
        {

        }
    }
}
