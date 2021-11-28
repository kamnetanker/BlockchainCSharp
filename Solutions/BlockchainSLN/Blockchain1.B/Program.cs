using System;
using System.IO;

namespace Blockchain2.B
{
    class Program
    {
        public static Config conf;
        public static HashBlockChainer hBC;
        public static string configPath;
        public static string hash1;
        public static string hash2;
        public static string str;
        public static void ReadInput(string[] args)
        {
            configPath = "./config.conf";
            hash1 = "";
            hash2 = "";
            str = "";
            for(int i=0; i<args.Length; i++)
            {
                if (args[i] == "-c" && i < (args.Length - 1))
                {
                    configPath = args[i + 1];
                    i += 1;
                }
                else if (args[i] == "-d" && i < (args.Length - 2))
                {
                    hash1 = args[i + 1];
                    hash2 = args[i + 2];
                    i += 2;
                }
                else
                {
                    str = args[i];
                    return;
                }
            }
        }
        static void Main(string[] args)
        {
            /*
            Начало
            Чтение параметров командной строки
            Если указан конфиг, то 
	            Попытаться загрузить конфиг из указанного места
            Иначе
	            Попытаться загрузить config.conf из локальной директории
            Если конфиг успешно загружен, то провести инициализацию всех переменных
            Если указан параметр -d то 
	            Если указаны 2 хэша после
		            Найти хэши и вывести diff
            Если указана строка в конце
	            Добавить строку к цепочке блоков
            Записать текущее состояние цепочки блоков
            Конец 
            */
            //Чтение параметров командной строки
            ReadInput(args);
            if (File.Exists(configPath))
            {

            }

        }
    }
}
