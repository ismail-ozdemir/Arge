using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerializeDeserialize
{
    public static class Test1
    {

        public static void Run()
        {
            JsonSerializerSettings settings = GetJsonSettings();

            Personel p1 = new Personel("ismail", "Özdemir") { Yasi = 30, Departman = new Departman { Adi = "Yazılım" } };
            Personel p2 = new Personel("Funda", "Sarikaya") { Departman = new Departman { Adi = "Test" } };
            p1.Yonetici = p2;
            p2.Yonetici = p1;



            string json = JsonConvert.SerializeObject(p1, settings);
            Console.WriteLine(json);

            Personel p3 = new Personel("x");
            JsonConvert.PopulateObject(json, p3, settings);

            Personel? p4 = JsonConvert.DeserializeObject<Personel>(json, settings);


            string json2 = "{\"$id\":\"1\",\"$type\":\"ActivatorTest.Personel, JsonSerializeDeserialize, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\"Adi\":\"ismail\",\"Soyadi\":\"Özdemir\",\"Yasi\":30,\"Departman\":{\"$id\":\"2\",\"$type\":\"ActivatorTest.Departman2, JsonSerializeDeserialize, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\"Adi\":\"Yazılım\"},\"Yonetici\":{\"$id\":\"3\",\"$type\":\"ActivatorTest.Personel, JsonSerializeDeserialize, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\"Adi\":\"Funda\",\"Soyadi\":\"Sarikaya\",\"Departman\":{\"$id\":\"4\",\"$type\":\"ActivatorTest.Departman, JsonSerializeDeserialize, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\"Adi\":\"Test\"},\"Yonetici\":{\"$ref\":\"1\"}}}";
            Personel? obj = JsonConvert.DeserializeObject<Personel>(json2, settings);
        }

        private static JsonSerializerSettings GetJsonSettings()
        {

            return new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,// Property olarak interface,class gibi tanımlamaların FullName'ini json dosyasına ekler
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,// Döngüye girecek referans durumunlarını yok sayar
                PreserveReferencesHandling = PreserveReferencesHandling.All, // Referans eşleştirme yapar.
                DefaultValueHandling = DefaultValueHandling.Ignore, //Default değerlere sahip alanları json'a eklenip eklenmeyeceğini belirler
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,//nesneyi oluşturmak için private constructor'lara erişim sağlar
                MissingMemberHandling = MissingMemberHandling.Ignore,// Json veride olup nesnede olmayan alanlar deserialize edilmek istendiğide hata verip yada yok sayılmasını belirler
                //ObjectCreationHandling = ObjectCreationHandling.Replace,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,

            };

        }

    }
}
