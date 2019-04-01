using System;
using static System.Console;

namespace Trenager2._0
{
    class Dispetcher
    {
        public string Name;
        int Korrect_pogod_usl;
        int tecush_skor_samoleta;
        int Shtraf;
        int Rekomend_visota_poleta;

        public override string ToString()
        {
            return $"Имя {Name} ";
        }

        public Dispetcher(string n)
        {
            Random rand = new Random();
            Name = n;
            Korrect_pogod_usl = rand.Next(-201, 201);
            tecush_skor_samoleta = 0; Shtraf = 0; Rekomend_visota_poleta = 0;
        }

        void Get_tek_skor_samoleta(int s) => tecush_skor_samoleta = s;

        public int Get_Shtraf() => Shtraf;

        void Get_rekomend_h_poleta() { Rekomend_visota_poleta = 7 * tecush_skor_samoleta - Korrect_pogod_usl; }

        public void Print_disp_recomend()
        {          
            WriteLine("\n==================");
            WriteLine($"Диспетчер {Name}");
            WriteLine($"Рекомендованная высота полета - {Rekomend_visota_poleta}");
            WriteLine($"Штрафных очков набрано {Shtraf}");
            if (tecush_skor_samoleta >= 1000)
                WriteLine("Вы достигли максимально необходимой скорости, можно начинать снижение ");
            WriteLine("==================\n");
        }

        public void Chek(int v, int s, bool max_skor, bool posadka, ref bool Vsyo)
        {
            if(s>50)
            {
                Get_tek_skor_samoleta(s);
                Get_rekomend_h_poleta();
            }
            
            try
            {
                if (Rekomend_visota_poleta - v >= 300 && Rekomend_visota_poleta - v < 600 ||
                Rekomend_visota_poleta - v >= -300 && Rekomend_visota_poleta - v < -600)
                    Shtraf += 25;
                else
                if (Rekomend_visota_poleta - v >= 600 && Rekomend_visota_poleta - v <= 1000 ||
                Rekomend_visota_poleta - v >= -600 && Rekomend_visota_poleta - v <= -1000)
                    Shtraf += 50;
                else
                if (Rekomend_visota_poleta - v > 1000 || Rekomend_visota_poleta - v < -1000)
                {
                    Vsyo = true;
                    throw new Exception("Самолет разбился! ");
                }

                else
                if (v < 0)
                {
                    Vsyo = true;
                    throw new Exception("Самолет разбился! ");
                }

                else
                if (v == 0 && s == 0 && posadka == false)
                {
                    Vsyo = true;
                    throw new Exception("Самолет разбился! ");
                }

                else
                if (s <= 0 && posadka == false || s >= 250 && v == 0)
                {
                    Vsyo = true;
                    throw new Exception("Самолет разбился! ");
                }

                if (Shtraf >= 1000)
                {
                    Vsyo = true;
                    throw new Exception("Непригоден к полетам! ");
                }

                if (tecush_skor_samoleta > 1000)
                {
                    WriteLine("Немедленно снизьте скорость!");
                    Shtraf += 100;
                }
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
                return;
            }
            if (tecush_skor_samoleta > 50)
            {
                Print_disp_recomend();
            }
        }
    }
}
