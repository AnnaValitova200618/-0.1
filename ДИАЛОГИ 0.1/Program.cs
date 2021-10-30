using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ДИАЛОГИ_0._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:/Magic/dialog.tlk";
            int stroka = 0, sdvig;
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))// открытие и
            //использование файла
            {
                reader.BaseStream.Seek(12, SeekOrigin.Current);//пропуск 12 байт

                stroka = reader.ReadInt32();// считываем кол-во строк
                sdvig = reader.ReadInt32();// считываем сдвиг, начало 3-ей части
                Console.WriteLine($"Количество строк:{stroka}, Cдвиг на часть 3: { sdvig} байт");//вывод на консоль
                Console.ReadKey();//ожидание нажатия клавиши

                int m = 0;
                int zap1 = 0, zap2 = 0;
                while (m < stroka)//цикл для чтения описания строк
                {
                    reader.BaseStream.Seek(28, SeekOrigin.Current); //пропуск 28 байт
                   
                    zap1 = reader.ReadInt32(); //читаем сдвиг
                    zap2 = reader.ReadInt32(); //читаем длину строки
                                              

                    reader.BaseStream.Seek(4, SeekOrigin.Current); //пропуск 4 байт
                    long pos = reader.BaseStream.Position; // запоминаем позицию во второй части файла
                    

                    reader.BaseStream.Seek(zap1 + sdvig, SeekOrigin.Begin); //находим позицию начала текстовой
                    //строки относительно начала файла

                    byte[] bytes = reader.ReadBytes(zap2); //читаем строку определённой длины

                    Encoding u = Encoding.GetEncoding(0x4e3); // перекодировка из Юникода
                    string text = u.GetString(bytes);

                    //Console.WriteLine($"{m}; {pos}; {(m+1)*40+20}; {zap1}; {zap2}; ");
                    Console.WriteLine($"Cтрока номер {m}: '{text}'");

                   reader.BaseStream.Seek(pos , SeekOrigin.Begin);//возвращаемся на следующую позицию описания
                                                                 //строк во второй части файла
                    Console.ReadKey();
                    m++;
                }

            }




        }
    }
}
