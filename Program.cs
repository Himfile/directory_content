using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleStreamUsing
{
    class Program
    {
        static void Main(string[] args)
        {
            void MyFolder(string path)
            {
                // Метод List и Array. Есть рекурсия. Нет глубины сканирования.
                List<string> ls = new List<string>();
                ls.Add("Папка: " + path);
                string[] startfiles = Directory.GetFiles(path);
                foreach (string filename in startfiles)
                {
                    ls.Add("\tФайл: " + filename);
                }
                if (startfiles.Length > 0)
                    ls.Add("\tФайлов " + startfiles.Length);
                try
                {
                    string[] folders = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
                    foreach (string foldername in folders)
                    {
                        ls.Add("Папка: " + foldername);
                        string[] files = Directory.GetFiles(foldername, "*", SearchOption.TopDirectoryOnly);
                        foreach (string filename in files)
                        {
                            ls.Add("\tФайл: " + filename);
                        }
                        if (files.Length > 0)
                            ls.Add("\tФайлов " + files.Length);
                    }
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                foreach (string filename in ls)
                {
                    Console.WriteLine(filename);
                }
            }
            DriveInfo[] drives = DriveInfo.GetDrives();
            Console.Title = "directory_content";
            Console.WriteLine("Проверьте информацию на дисках:");
            foreach (DriveInfo drive in drives) Console.WriteLine(drive.Name);
            try
            {
                for (byte i = 0; i < 3; i++)
                {
                    string dirName = "";
                    byte dirNumber = 0;
                    byte dirLine = 0;
                    Console.Write("\nВыбирите диск для проверки:  ");
                    ConsoleKey consoleKey = Console.ReadKey().Key;
                    // Выбираем логический диск.
                    dirName = Convert.ToString(consoleKey) + ":\\";
                    if (Directory.Exists(dirName))
                    {
                        Console.WriteLine($"\nВыбран диск   {dirName} ");
                        Console.WriteLine("Корневые каталоги:");
                        string[] dirs = Directory.GetDirectories(dirName);
                        //Проиндексируем и выведем корневые каталоги.
                        while (dirNumber < dirs.Length)
                        {
                            Console.WriteLine($"{dirNumber}) {dirs[dirNumber]}");
                            //Таймер задержки
                            System.Threading.Thread.Sleep(100);
                            dirNumber++;
                        }
                        //Выведем корневые файлы.
                        Console.WriteLine();
                        string[] files = Directory.GetFiles(dirName);
                        if (files.Length > 0)
                        {
                            Console.WriteLine($"Корневые Файлы:");
                            foreach (string s0 in files) Console.WriteLine($"{s0}");
                            Console.WriteLine($"Файлов {files.Length}.");
                        }
                        // Выбираем подкаталог.
                        Console.WriteLine();
                        for (byte j = 0; j < 3; j++)
                        {
                            try
                            {
                                Console.Write($"\nУкажите номер корневого каталога: ");
                                dirLine = byte.Parse(Console.ReadLine());
                                Console.Write($"\nСканируем каталог: ");
                            }
                            catch (FormatException)
                            {
                                Console.Write("Или");
                                for (int m = 0; m < 3; m++)
                                {
                                    Console.Write(" перетащите сюда путь к своей папке - ");
                                    string checkFold = Console.ReadLine();
                                    if (Directory.Exists(checkFold))
                                    {
                                        Console.WriteLine($"\nВы сканируете:");
                                        MyFolder(checkFold);
                                    }
                                    else
                                        Console.WriteLine("Путь указан неверно!");
                                }
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Ошибка ввода. Еще раз.");
                            }
                            if (dirLine < dirNumber)
                            {
                                // Начало метода.
                                MyFolder(dirs[dirLine]);
                            }
                            else
                            {
                                if (j == 2)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nНе удается выбрать подкаталог. Зайдите в другой раз(");
                                    Console.ResetColor();
                                    Console.Write("Или");
                                    for (int m = 0; m < 3; m++)
                                    {
                                        Console.Write(" перетащите сюда путь к своей папке - ");
                                        string checkFold = Console.ReadLine();
                                        if (Directory.Exists(checkFold))
                                        {
                                            Console.WriteLine($"\nВы сканируете:");
                                            MyFolder(checkFold);
                                        }
                                        else
                                            Console.WriteLine("Путь указан неверно!");
                                    }
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
                            Console.Write("Или");
                            for (int m = 0; m < 3; m++)
                            {
                                Console.Write(" перетащите сюда путь к своей папке - ");
                                string checkFold = Console.ReadLine();
                                if (Directory.Exists(checkFold))
                                {
                                    Console.WriteLine($"\nВы сканируете:");
                                    MyFolder(checkFold);
                                }
                                else
                                    Console.WriteLine("Путь указан неверно!");
                            }
                            break;
                        }
                        Console.WriteLine(" - такого диска нет. Попробуйте еще!");
                        continue;
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Выход. Неверный формат ввода.");
                Console.ResetColor();
            }
        }
    }
}