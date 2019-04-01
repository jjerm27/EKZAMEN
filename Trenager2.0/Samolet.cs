using System;
using System.Collections.Generic;
using static System.Console;

namespace Trenager2._0
{
    class Samolet
    {
        delegate void Chek_Disp(int v, int s, bool max_skor, bool posadka, ref bool Vsyo);

        Pilot pilot;
        List<Dispetcher> disp_list;
        int Skorost;
        int Visota;
        bool nachalo_vzleta;
        bool max_skor;
        bool posadka;
        bool Vsyo;

        event Chek_Disp Event_Chek;

        public void IsFinish(bool nachalo_vzleta, bool max_skor, bool posadka)
        {
            if (nachalo_vzleta == true && max_skor == true && posadka == true)
            {
                WriteLine("\n+++++++++++++++++++++++\n");
                WriteLine("Полет прошел нормально. ");
                foreach (var item in disp_list)
                {
                    pilot.Shtraf_ochki += item.Get_Shtraf();
                }
                WriteLine($"Результат: Пилот {pilot}");
                WriteLine("\n+++++++++++++++++++++++");
                ReadLine();
                Vsyo = true;
            }
            if(nachalo_vzleta == true && max_skor == false && posadka == true)
                WriteLine($"\nМаксимальная скорость не достигнута, задание не выполнено\n");
        }

        public Samolet()
        {
            WriteLine("Введите имя пилота");
            string t = ReadLine();
            pilot = new Pilot(t);
            WriteLine("Введите имя первого диспетчера ");
            t = ReadLine();
            disp_list = new List<Dispetcher>();
            disp_list.Add(new Dispetcher(t));
            WriteLine("Введите имя второго диспетчера ");
            t = ReadLine();
            disp_list.Add(new Dispetcher(t));
            foreach (var item in disp_list)
            {
                Event_Chek += item.Chek;
            }
            Skorost = 0; Visota = 0; max_skor = false; posadka = false; nachalo_vzleta = false; Vsyo = false;
        }

        public void Add_Visota()
        {
            try
            {
                if (Skorost == 0)
                {
                    Vsyo = true;
                    throw new Exception("Самолет не может взлететь без разгона! Задание провалено!");
                }
                Visota += 250;
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }
        public void Add_Visota_Shift()
        {
            try
            {
                if (Skorost == 0)
                {
                    Vsyo = true;
                    throw new Exception("Самолет не может взлететь без разгона! Задание провалено!");
                }
                Visota += 500;
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }
        public void Del_Visota()
        {
            try
            {
                Visota -= 250;
                if (Visota == 0 && Skorost == 0 && nachalo_vzleta == true)
                    posadka = true;
                if (Skorost > 50 && Visota <= 0)
                {
                    Vsyo = true;
                    throw new Exception("Самолет разбился!");
                }
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }
        public void Del_Visota_Shift()
        {
            try
            {
                Visota -= 500;
                if (Visota == 0 && Skorost == 0 && nachalo_vzleta == true)
                    posadka = true;              
                if (Skorost > 50 && Visota <= 0|| Visota < 0)
                {
                    Vsyo = true;
                    throw new Exception("Самолет разбился!");
                }
            }
            catch (Exception e)
            {
                WriteLine(e.Message); Vsyo = true;                
            }           
        }

        public void Add_Skorost()
        {
            Skorost += 50;
            if (Skorost >= 1000 && Visota > 200)
                max_skor = true;
        }

        public void Del_Skorost()
        {
            try
            {
                Skorost -= 50;
                if (Skorost == 0 && Visota == 0 && max_skor == true)
                    posadka = true;
                if (Skorost < 0)
                    Skorost = 0;
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }
        public void Add_Skorost_Shift()
        {
            Skorost += 150;
            if (Skorost >= 1000 && Visota > 200)
                max_skor = true;
        }
        public void Del_Skorost_Shift()
        {
            Skorost -= 150;
            if (Skorost == 0 && Visota == 0 && max_skor == true)
                posadka = true;
            if (Skorost < 0)
                Skorost = 0;
        }

        public int Get_Cur_Visota() => Visota;
        public int Get_Skorost() => Skorost;

        void Add_Dispetcher()
        {
            WriteLine("Введите имя нового диспетчера");
            string t = ReadLine();
            Dispetcher d = new Dispetcher(t);
            disp_list.Add(d);
            Event_Chek += d.Chek;
            WriteLine("Диспетчер добавлен!");
            WriteLine("Для продолжения нажмите клавишу");
            ReadLine();
        }

        void Del_Dispetcher()
        {
            if (disp_list.Count > 2)
            {
                WriteLine("Введите номер диспетчера, которого хотите удалить");
                int x = 0;
                foreach (Dispetcher item in disp_list)
                {
                    WriteLine($"{x} {item}");
                    x++;
                }
                x = Convert.ToInt32(ReadLine());
                pilot.Shtraf_ochki += disp_list[x].Get_Shtraf();
                Event_Chek -= disp_list[x].Chek;
                disp_list.RemoveAt(x);
                WriteLine("\nДиспетчер удален ");
                WriteLine("\nДля продолжения нажмите клавишу");
                ReadLine();
            }
            else
            {
                WriteLine("\nДиспетчеров не может быть меньше двух !");
                WriteLine("\nДля продолжения нажмите клавишу");
                ReadLine();
            }
        }

        void Print_params()
        {
            WriteLine("\n==============================================");
            WriteLine($"Текущая скорость {Skorost} км/ч");
            WriteLine($"Текущая высота {Visota} м");
            WriteLine("==============================================\n");
        }

        public void Polet()
        {
            ConsoleKeyInfo k;
            do
            {
                Clear();
                WriteLine("Выберите действие");
                WriteLine("Стрелка вверх - добавить высоту");
                WriteLine("Стрелка вверх + Shift - ускоренное добавление высоты");
                WriteLine("Стрелка вниз - убавить высоту");
                WriteLine("Стрелка вниз + Shift - ускоренное убавление высоты");
                WriteLine("Стрелка вправо - добавить скорость");
                WriteLine("Стрелка вправо + Shift - ускоренное добавление скорости");
                WriteLine("Стрелка влево - убавить скорость");
                WriteLine("Стрелка влево + Shift - ускоренное убавление скорости");
                WriteLine("+ - добавить диспетчера");
                WriteLine("- - удалить диспетчера");
                WriteLine("Esc - выход");

                Print_params();
                if (Skorost > 50)
                    Event_Chek?.Invoke(Visota, Skorost, max_skor, posadka, ref Vsyo);
                IsFinish(nachalo_vzleta, max_skor, posadka);

                k = ReadKey();

                switch (k.Key)
                {
                    case ConsoleKey.UpArrow:
                        nachalo_vzleta = true;
                        if (k.Modifiers.ToString() == "Shift")
                            Add_Visota_Shift();
                        else
                            Add_Visota();
                        break;
                    case ConsoleKey.DownArrow:
                        if (k.Modifiers.ToString() == "Shift")
                            Del_Visota_Shift();
                        else
                            Del_Visota();
                        break;
                    case ConsoleKey.RightArrow:
                        if (k.Modifiers.ToString() == "Shift")
                            Add_Skorost_Shift();
                        else
                            Add_Skorost();
                        break;
                    case ConsoleKey.LeftArrow:
                        if (k.Modifiers.ToString() == "Shift")
                            Del_Skorost_Shift();
                        else
                            Del_Skorost();
                        break;
                    case ConsoleKey.OemPlus:
                        Add_Dispetcher();
                        break;
                    case ConsoleKey.OemMinus:
                        Del_Dispetcher();
                        break;                  
                }
            } while (k.Key != ConsoleKey.Escape && Vsyo == false);
        }
    }
}
