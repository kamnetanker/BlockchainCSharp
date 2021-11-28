using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleJSON;
using System.IO;
using System.Security.Cryptography;

namespace Blockchain2.B
{
    // Перечисление алгоритмов хеширования
    public enum HashAlgorithms
    {
        MD5 = 1,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }
    // Перечисление кодировок
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
        public int encoding;
        public int hashAlgo;
        public int blockSize;
        public bool loaded = false;
        public Config(string path)
        {
            if (!File.Exists(path))
                return;
            string config = File.ReadAllText(path);
            JSONNode json_config = JSON.Parse(config);
            if (json_config != null && config != "")
            {
                this.destDir = json_config["destDir"];
                this.hashAlgo = json_config["hashAlgo"];
                this.encoding = json_config["encoding"];
                this.blockSize = json_config["blockSize"];
                this.loaded = true;
            }
            else
            {
                loaded = false;
            }
        }
        public HashAlgorithm getHashAlgorithm()
        {
            if (!this.loaded) return MD5.Create();
            HashAlgorithm hA = null;
            switch (this.hashAlgo)
            {
                case (int)HashAlgorithms.SHA1:hA= SHA1.Create(); break;
                case (int)HashAlgorithms.SHA256: hA = SHA256.Create(); break;
                case (int)HashAlgorithms.SHA384: hA = SHA384.Create(); break;
                case (int)HashAlgorithms.SHA512: hA = SHA512.Create(); break;
                case (int)HashAlgorithms.MD5:
                default: hA = MD5.Create(); break;
            }
            return hA;
        }
        public Encoding getEncoding()
        {
            if (!this.loaded) return Encoding.Default;
            Encoding retE = null;
            switch (this.encoding)
            {
                case (int)EncodingConf.UTF8: retE = Encoding.UTF8; break;
                case (int)EncodingConf.UTF7: retE = Encoding.UTF7; break;
                case (int)EncodingConf.UTF32: retE = Encoding.UTF32; break;
                case (int)EncodingConf.Unicode: retE = Encoding.Unicode; break;
                case (int)EncodingConf.BigEndianUnicode: retE = Encoding.BigEndianUnicode; break;
                case (int)EncodingConf.Latin1: retE = Encoding.Latin1; break;
                case (int)EncodingConf.ASCII: retE = Encoding.ASCII; break;
                case (int)EncodingConf.Default:
                default: retE = Encoding.Default; break;
            }
            return retE;
        }
    }
}
