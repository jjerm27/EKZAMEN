
namespace Trenager2._0
{
    class Pilot
    {
        public string Name;
        public int Shtraf_ochki;
        public Pilot(string n)
        {
            Name = n; Shtraf_ochki = 0;
        }

        public override string ToString()
        {
            return $"{Name} Штрафные очки {Shtraf_ochki}";
        }
    }
}
