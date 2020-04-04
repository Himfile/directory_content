using System;
using System.IO;

namespace ConsoleStreamUsing
{
    class Program
    {
        static void Main(string[] args)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            Console.Title = "directory_content";
            Console.WriteLine("Перечень дисков:");
            foreach (DriveInfo drive in drives) Console.WriteLine(drive.Name);
            try
            {
                for (byte i = 0; i < 3; i++)
                {
                    string dirName = "";
                    byte dirNumber = 0;
                    short level = 0;
                    byte dirLine = 0;
                    Console.Write("\nВыбирите диск из перечня:  ");

                    ConsoleKey consoleKey = Console.ReadKey().Key;
 // Выбираем логический диск C-F.
                    switch (consoleKey)
                    {
                        case ConsoleKey.C:
                            dirName = $"C:\\";
                            break;
                        case ConsoleKey.D:
                            dirName = $"D:\\";
                            break;
                        case ConsoleKey.E:
                            dirName = $"E:\\";
                            break;
                        case ConsoleKey.F:
                            dirName = $"F:\\";
                            break;
                        default:
                            break;
                    }

                    if (Directory.Exists(dirName))
                    {
                    Console.WriteLine($"\nВыбран диск   {dirName} ");
                            int k = 0;
                        while (level!= 1 && level !=2 && level !=3)
                        {
                            Console.Write("\nЗадайте глубину поиска (1,2 или 3): ");
                            level = short.Parse(Console.ReadLine());
                            k++;
                            if (k == 3)
                            {
                                level = 0;
                                Console.WriteLine($"\nЯсно. Исследуем только корневые элементы.");
                                break;
                            }
                        }
                        Console.WriteLine("\tКорневые каталоги:");
                        string[] dirs = Directory.GetDirectories(dirName);
 //Проиндексируем и выведем корневые каталоги.
                        while(dirNumber < dirs.Length) 
                        {
                            Console.WriteLine($"\t{dirNumber}) {dirs[dirNumber]}");
                            System.Threading.Thread.Sleep(100);
                            dirNumber++;
                        }
 //Выведем корневые файлы.
                        Console.WriteLine();
                        string[] files = Directory.GetFiles(dirName);
                        if (files.Length > 0)
                        {
                            Console.WriteLine($"\tКорневые Файлы:");
                            foreach (string s0 in files) Console.WriteLine($"\t{s0}");
                            Console.WriteLine($"\tНайдено файлов {files.Length}.");
                        }
                        // Выбираем подкаталог.
                        Console.WriteLine();
                        for (byte j = 0; j < 3; j++)
                        {
                            try
                            {
                                Console.Write($"\nУкажите номер корневого каталога: ");
                                dirLine = byte.Parse(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                if (j == 2) break;                                
                                Console.WriteLine("Ошибка ввода. Еще раз.");
                            }
                            if (dirLine < dirNumber)
                            {
                                string[] dirlev1 = Directory.GetDirectories(dirs[dirLine]);
                                if (dirlev1.Length > 0)
                                {
                                    Console.WriteLine($"Выбран каталог - {dirs[dirLine]}\n\n\tПодкаталоги базового уровня:");
                                    foreach (string s1 in dirlev1)
                                    {
                                        Console.WriteLine($"\t{s1}");

                                        if (level >= 2 && level != 0) //Глубина поиска папок 2.
                                        {
                                            string[] dirlev2 = Directory.GetDirectories(s1);
                                            if (dirlev2.Length > 0)
                                            {
                                                Console.WriteLine($"\t\tПодкаталоги второго уровня:");
                                                foreach (string s2 in dirlev2)
                                                {
                                                    Console.WriteLine($"\t\t{s2}");

                                                    if (level >= 3 && level != 0) //Глубина поиска папок 3.
                                                    {
                                                        string[] dirlev3 = Directory.GetDirectories(s2);
                                                        if (dirlev3.Length > 0)
                                                        {
                                                            Console.WriteLine($"\t\t\tПодкаталоги третьего уровня:");
                                                            foreach (string s3 in dirlev3)
                                                            {
                                                                Console.WriteLine($"\t\t\t{s3}");
                                                            }
                                                            Console.WriteLine($"\t\t\tНайдено подкаталогов {dirlev3.Length}.");
                                                        }
                                                    }
                                                }
                                                Console.WriteLine($"\t\tНайдено подкаталогов {dirlev2.Length}.");
                                                Console.WriteLine();
                                                string[] files2 = Directory.GetFiles(s1);
                                                if (files2.Length > 0)
                                                {
                                                    Console.WriteLine($"\t\tФайлы второго уровня:");
                                                    foreach (string ss1 in files2) Console.WriteLine($"\t\t{ss1}");
                                                    Console.WriteLine($"\t\tНайдено файлов {files2.Length}.");
                                                }
                                            }
                                        }
                                    }
                                    Console.WriteLine($"\tНайдено подкаталогов {dirlev1.Length}.");
                                    Console.WriteLine();
                                    string[] files1 = Directory.GetFiles(dirs[dirLine]);
                                    if (files1.Length > 0)
                                    {
                                        Console.WriteLine($"\tФайлы базового уровня:");
                                        foreach (string ss1 in files1) Console.WriteLine($"\t{ss1}");
                                        Console.WriteLine($"\tНайдено файлов {files1.Length}.");
                                    }
                                }
                            }
                            else
                            {
                                if (j == 2)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nНе удается выбрать подкаталог. Зайдите в другой раз(");
                                    Console.ResetColor();
                                    break;
                                }
                                Console.WriteLine("Такого подкаталога нет. Попробуйте еще!");
                                continue;
                            }
                            break;
                        }
                    }
                    else
                    {
                        if (i == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nНе удается выбрать диск. Попробуйте в другой раз(");
                            Console.ResetColor();
                            break;
                        }
                        Console.WriteLine(" - такого диска нет. Попробуйте еще!");
                        continue;
                    }
                    break;
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Выход.");
                Console.ResetColor();
            }
        }
    }
}
