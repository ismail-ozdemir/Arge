// See https://aka.ms/new-console-template for more information

namespace JsonSerializeDeserialize
{
    public class Personel
    {
        private Personel()
        {

        }
        public Personel(string _adi) => Adi = _adi;

        public Personel(string _adi, string _soyadi) : this(_adi) => Soyadi = _soyadi;
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int Yasi { get; set; }
        public IDepartman Departman { get; set; }
        public override string ToString() => $"Adi : {Adi}, Soyadi : {Soyadi}, Yasi : {Yasi}, Departman :{Departman.Adi}";

        public Personel Yonetici { get; set; }
    }


}
