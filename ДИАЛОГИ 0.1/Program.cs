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
                Console.WriteLine($"{stroka},{ sdvig}");//вывод на консоль
                Console.ReadKey();//ожидание нажатия клавиши

                int re = 21; //первый байт второй части файла
                int zd = 0; // накопление сдвига
                
                int m = 0;
                int zap1 = 0, zap2 = 0;
                while (m < stroka)//цикл для чтения описания строк
                {
                    reader.BaseStream.Seek(28, SeekOrigin.Current);//пропуск 28 байт
               // мы можем расчитать позицию записи относительно начала файла как m*40+21?? 

                    Console.WriteLine(m);

                    zap1 = reader.ReadInt32();//читаем сдвиг
                    zap2 = reader.ReadInt32();//читаем длину строки; на втором проходе цикла
                                              //!!строка не определяется!!
                    zd = zd + zap1; //накапливаем сдвиг
                    

                    reader.BaseStream.Seek(4, SeekOrigin.Current);//пропуск 4 байт
                    // можем ли мы каким-то  образом сохранить текущую позицию в переменную??
                    re = re + 40;// следующие описание строки
                  
                        Console.WriteLine($"длина строки - {zap2} ");

                    reader.BaseStream.Seek(zd + sdvig, SeekOrigin.Begin);//находим позицию начала текстовой
                    //строки относительно начала файла

                    byte[] bytes = reader.ReadBytes(zap2);//читаем строку определённой длины

                    Encoding u = Encoding.GetEncoding(0x4e3);
                    string text = u.GetString(bytes);

                    Console.WriteLine($"{m}; {re}; {zap1}; {zap2}; ");
                    Console.WriteLine($"%{text}%");

                   reader.BaseStream.Seek(re , SeekOrigin.Begin);//возвращаемся на следующую позицию описания
                                                                 //строк во второй части файла
             // мы можем расчитать следующую позицию записи во второй части как (m+1)*40+21?? 

                    Console.ReadKey();
                    m++;
                }

            }




        }
    }
}
