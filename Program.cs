using System;
using System.IO;

namespace ConsoleStreamUsing {

    class Program {
        static void Main (string[] args) {
            int s = 0;
            void Folder (string path) {
                // Метод использует array DirectoryInfo и рекурсию.
                s++;
                DirectoryInfo DI = new DirectoryInfo (path);
                DirectoryInfo[] SubDir = DI.GetDirectories ();
                //Console.WriteLine($"Выбран каталог {path}.");
                if (SubDir.Length > 0) {
                    Console.WriteLine ($"Папки уровня {s}:");
                    for (int i = 0; i < SubDir.Length; ++i) {
                        Folder (SubDir[i].FullName);
                        Console.WriteLine (SubDir[i].FullName);
                    }
                    Console.WriteLine ($"Найдено папок {SubDir.Length}");
                    Console.WriteLine ($"\nФайлы уровня {s}:");
                    FileInfo[] FI = DI.GetFiles ();
                    for (int i = 0; i < FI.Length; ++i)
                        Console.WriteLine ($"{FI[i].FullName}");
                    Console.WriteLine ($"Найдено файлов {FI.Length}");
                }
            }
            DriveInfo[] drives = DriveInfo.GetDrives ();
            Console.Title = "directory_content";
            Console.WriteLine ("Перечень дисков:");
            foreach (DriveInfo drive in drives) Console.WriteLine (drive.Name);
            try {
                for (byte i = 0; i < 3; i++) {
                    string dirName = "";
                    byte dirNumber = 0;
                    short level = 0;
                    byte dirLine = 0;
                    Console.Write ("\nВыбирите диск из перечня:  ");
                    ConsoleKey consoleKey = Console.ReadKey ().Key;
                    // Выбираем логический диск C-F.
                    switch (consoleKey) {
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
                    if (Directory.Exists (dirName)) {
                        Console.WriteLine ($"\nВыбран диск   {dirName} ");
                        int k = 0;
                        while (level != 1 && level != 2 && level != 3) {
                            Console.Write ("\nЗадайте глубину поиска (1,2 или 3): ");
                            level = short.Parse (Console.ReadLine ());
                            k++;
                            if (k == 3) {
                                level = 0;
                                Console.WriteLine ($"\nЯсно. Исследуем только корневые элементы.");
                                break;
                            }
                        }
                        Console.WriteLine ("\tКорневые каталоги:");
                        string[] dirs = Directory.GetDirectories (dirName);
                        //Проиндексируем и выведем корневые каталоги.
                        while (dirNumber < dirs.Length) {
                            Console.WriteLine ($"{dirNumber}) {dirs[dirNumber]}");
                            System.Threading.Thread.Sleep (100);
                            dirNumber++;
                        }
                        //Выведем корневые файлы.
                        Console.WriteLine ();
                        string[] files = Directory.GetFiles (dirName);
                        if (files.Length > 0) {
                            Console.WriteLine ($"Корневые Файлы:");
                            foreach (string s0 in files) Console.WriteLine ($"{s0}");
                            Console.WriteLine ($"Найдено файлов {files.Length}.");
                        }
                        // Выбираем подкаталог.
                        Console.WriteLine ();
                        for (byte j = 0; j < 3; j++) {
                            try {
                                Console.Write ($"\nУкажите номер корневого каталога: ");
                                dirLine = byte.Parse (Console.ReadLine ());
                            } catch (FormatException) {
                                if (j == 2) break;
                                Console.WriteLine ("Ошибка ввода. Еще раз.");
                            }
                            if (dirLine < dirNumber) {
                                //Начало метода.
                                Folder (dirs[dirLine]);
                                Console.ReadKey ();
                            } else {
                                if (j == 2) {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine ("\nНе удается выбрать подкаталог. Зайдите в другой раз(");
                                    Console.ResetColor ();
                                    break;
                                }
                                Console.WriteLine ("Такого подкаталога нет. Попробуйте еще!");
                                continue;
                            }
                            break;
                        }
                    } else {
                        if (i == 2) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine ("\nНе удается выбрать диск. Попробуйте в другой раз(");
                            Console.ResetColor ();
                            break;
                        }
                        Console.WriteLine (" - такого диска нет. Попробуйте еще!");
                        continue;
                    }
                    break;
                }
            } catch {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine ("Выход.");
                Console.ResetColor ();
            }
        }
    }
}