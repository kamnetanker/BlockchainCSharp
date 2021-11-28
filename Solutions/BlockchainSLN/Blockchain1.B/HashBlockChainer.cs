using SimpleJSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain2.B
{
    class HashBlockChainer
    {
        // Идентификатор подписчика
        public string hbcId { get; set; }
        // Рабочая директория
        public string blockDestination { get; set; }
        // Идентификатор последнего сохранённого блока
        public string lastBlockHash { get; set; }
        // Размер блока
        public int blockSize { get; set; }
        // Алгоритм хеширования
        public int hashAlgo { get; set; }
        // Список подписанных блоков
        public List<string> signedBlocks { get; set; }
        // Текущий блок
        public BlockChain curBlock { get; set; }
        // Хешировщик
        public vvaah hasher;
        // Переменная, отвечающая за инициализированность
        public bool inited = false;
        public HashBlockChainer(Config conf)
        {
            hasher = new vvaah(conf.getHashAlgorithm(), conf.getEncoding());
            // Размер блока
            this.blockSize = conf.blockSize;
            // Алгоритм хэширования
            this.hashAlgo = conf.hashAlgo;

            // Устанавливаем рабочую директорию
            blockDestination = conf.destDir;
            // Если файл конфигурации в указанной директории существует, то загружаем его
            if (File.Exists(this.blockDestination + "/curData.conf"))
            {
                // Читаем файл конфигурации
                string currentConfig = File.ReadAllText(this.blockDestination + "/curData.conf");
                // Преобразуем его в JSON объект
                JSONNode json_config = JSON.Parse(currentConfig);
                // Если файл не пустой и конфиг в json не пустой
                if(json_config!=null && currentConfig != "")
                {
                    // Устанавливаем идентификатор
                    this.hbcId = json_config["hbcId"];
                    // Устанавливаем последний изменённый блок
                    this.lastBlockHash = json_config["lastBlockHash"];
                    // Создаём список подписанных блоков
                    this.signedBlocks = new List<string>();
                    // Записываем их идентификаторы
                    foreach(JSONNode jn in json_config["signedBlocks"])
                    {
                        this.signedBlocks.Add(jn.Value);
                    }
                    // Загружаем из файла блока текущий блок.
                    //Console.WriteLine(this.blockDestination + "/" + json_config["currentBlock"] + ".blck");
                    this.curBlock = BlockChain.FromFile(this.blockDestination+"/"+json_config["currentBlock"]+".blck");
                    
                }
            }
            else
            {
                // Если же конфигурации не существует, её необходимо создать
                // Генерируем случайный идентификатор для hbc
                this.hbcId = hasher.ComputeHash(Convert.ToHexString(conf.getRandomByteArray(512)));
                // Устанавливаем пустой идентификатор последнего блока
                this.lastBlockHash = "";
                // Создаём блок
                this.curBlock = new BlockChain(hbcId, lastBlockHash, conf.hashAlgo, conf.blockSize);
                // Подписываем блок
                this.SignBlock();
                // Создаём список подписанных блоков
                this.signedBlocks = new List<string>();
                // Добавляем блок в подписанные
                this.signedBlocks.Add(this.curBlock.blockHash);
                // Сохраняем текущую конфигурацию
                this.SaveStatement();
            }
            inited = true;
        }
        public void SignBlock()
        {
            // Вычисляем подпись блока, как хеш от суммы идентификатора создателя, времени создания и родительского блока
            curBlock.blockHash = hasher.ComputeHash(curBlock.hbcCreator + curBlock.timeStamp + curBlock.parentHash);
        }
        public void CreateBlock(Config conf)
        {
            // Сохраняем текущий блок
            this.SaveBlock();
            // Создаём блок
            this.curBlock = new BlockChain(hbcId, lastBlockHash, conf.hashAlgo, conf.blockSize);
            // Подписываем блок
            this.SignBlock();
            // Добавляем блок в подписанные
            this.signedBlocks.Add(this.curBlock.blockHash);
            // Сохраняем текущую конфигурацию
            this.SaveStatement();
        }
        public void CreateBlock()
        {
            // Сохраняем текущий блок
            this.SaveBlock();
            // Создаём блок
            this.curBlock = new BlockChain(hbcId, lastBlockHash, this.hashAlgo, this.blockSize);
            // Подписываем блок
            this.SignBlock();
            // Добавляем блок в подписанные
            this.signedBlocks.Add(this.curBlock.blockHash);
            // Сохраняем текущую конфигурацию
            this.SaveStatement();
        }
        public void SaveBlock()
        {
            // Записываем текущий блок в файл
            this.curBlock.ToFile(this.blockDestination + "/" + this.curBlock.blockHash + ".blck");
            // Запоминаем идентификатор последнего сохранённого блока
            this.lastBlockHash = this.curBlock.blockHash;
        }
        public void SaveStatement()
        {
            // Сохраняем текущий блок
            this.SaveBlock();
            //Вручную заполняем информацию для записи на диск
            string json_to_save = "{";
            // Заполняем идентификатор подписчика
            json_to_save += "\"hbcId\":\"" + this.hbcId + "\",";
            // Заполняем последний сохранённый блок
            json_to_save += "\"lastBlockHash\":\"" + this.lastBlockHash + "\",";
            // Заполняем текущий блок
            json_to_save += "\"currentBlock\":\"" + this.curBlock.blockHash + "\",";
            // Записываем список всех подписанных блоков
            json_to_save += "\"signedBlocks\":[";
            for(int i=0; i<this.signedBlocks.Count; i++)
            {
                json_to_save += "\"" + this.signedBlocks[i] + "\"";
                // Для всех, кроме последнего заполняем запятую
                if(i!= this.signedBlocks.Count - 1)
                {
                    json_to_save += ",";
                }
            }
            // Закрываем json
            json_to_save += "}";
            // Записываем в файл
            File.WriteAllText(this.blockDestination + "/curData.conf", json_to_save);
        }
        public void AddString(string str)
        {
            // Создаём переменную последнего хэша
            string lastHash = "";
            // Если уже есть записанные хэши
            if (this.curBlock.iter > 0)
            {
                // То берём последний для подписи
                lastHash = this.curBlock.hashes[this.curBlock.iter-1];
            }
            else
            {
                // Иначе берём идентификатор родительского блока
                lastHash = this.curBlock.parentHash;
            }
            // Вычисляем новый хэш
            string hash = this.hasher.ComputeHash(str + lastHash);
            // Добавляем строку и хэш в блок
            this.curBlock.AddHash(str, hash);
            // Если блок заполнен, то создаём новый блок
            if (this.curBlock.iter == this.blockSize)
            {
                this.CreateBlock();
            }
        }

        public void Diff(string blockA, string blockB, out List<string> diff)
        {
            List<string> diffLog = new List<string>();
            BlockChain A = null, B = null;
            if (File.Exists(this.blockDestination + "/" + blockA + ".blck") && File.Exists(this.blockDestination + "/" + blockB + ".blck"))
            {
                A = BlockChain.FromFile(this.blockDestination + "/" + blockA + ".blck");
                B = BlockChain.FromFile(this.blockDestination + "/" + blockB + ".blck");
            }
            else
            {
                diffLog.Add("Sorry, but blocks with this Id's seems not exist");
                diff = diffLog;
                return;
            }
            string toDiffLog = "Id's: " + A.blockHash + "\t" + B.blockHash+((A.blockHash==B.blockHash)?"\t Equal": "\t Not equal");
            diffLog.Add(toDiffLog);
            diffLog.Add("=======================================================================================================");
            toDiffLog = "Hash algo id's: " + A.hashAlgo + "\t" + B.hashAlgo + ((A.hashAlgo == B.hashAlgo) ? "\t Equal" : "\t Not equal");
            diffLog.Add(toDiffLog);
            diffLog.Add("=======================================================================================================");
            toDiffLog = "Creator Id: " + A.hbcCreator + "\t" + B.hbcCreator + ((A.hbcCreator == B.hbcCreator) ? "\t Equal" : "\t Not equal");
            diffLog.Add(toDiffLog);
            diffLog.Add("=======================================================================================================");
            toDiffLog = "Parent block: " + A.parentHash + "\t" + B.parentHash + ((A.parentHash == B.parentHash) ? "\t Equal" : "\t Not equal");
            diffLog.Add(toDiffLog);
            diffLog.Add("=======================================================================================================");
            toDiffLog = "Time stamp: " + A.timeStamp + "\t" + B.timeStamp + ((A.timeStamp == B.timeStamp) ? "\t Equal" : "\t Not equal");
            diffLog.Add(toDiffLog);
            diffLog.Add("=======================================================================================================");
            toDiffLog = "Time stamp: " + A.size + "\t" + B.size + ((A.size == B.size) ? "\t Equal" : "\t Not equal");
            diffLog.Add(toDiffLog);
            diffLog.Add("=======================================================================================================");
            for (int i = 0, k = 0; i < A.size || k < B.size;  )
            {
                string str1=(i<A.size)? A.strs[i]:"\t";
                string str2 = (k < B.size) ? B.strs[k] : "\t"; ;
                string hash1 = (i < A.size) ? A.hashes[i] : "\t"; ;
                string hash2 = (k < B.size) ? B.hashes[k] : "\t"; ;
                toDiffLog = "strs: " + str1 + "\t" + str2 + ((str1 == str2) ? "\t Equal" : "\t Not equal");
                diffLog.Add(toDiffLog);
                toDiffLog = "hashes: " + hash1 + "\t" + hash2 + ((hash1 == hash2) ? "\t Equal" : "\t Not equal");
                diffLog.Add(toDiffLog);
                diffLog.Add("-----------------------------------------------------------------------------------------------------");
                if (i < A.size) i++;
                if (k < B.size) k++;
            }
            diff = diffLog;
            return;
        }
    }
}
