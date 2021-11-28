using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain2.B
{
    //Перечисление алгоритмов хеширования
    public enum HashAlgorithms
    {
        MD5 = 1,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }
    public enum EncodingConf
    {
        Default = 1,
        UTF8,
        UTF7, 
        UTF32,
        Unicode,
        BigEndianUnicode,
        Latin1,
        ASCII
    }
    class Config
    {
        public string destDir;
        public string hashAlgo;
        public string encoding;
        public int blockSize;
        public Config(string path)
        {

        }
    }
}
