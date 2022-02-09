using ActivatorTest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerializeDeserialize
{
    public static class Test2
    {
        public static void Run()
        {

            Personel personel = GetPersonel();
            JsonSerializerSettings settings = GetJsonSettings();

            string hataliJson = ToJsonErroNameSpace(personel, settings);


            Console.WriteLine(hataliJson);
            Personel? personel2 = JsonConvert.DeserializeObject<Personel>(hataliJson, settings);



        }

        /// <summary>
        /// seriliaze ve deserialize edilecek personel objesini oluşturur
        /// </summary>
        /// <returns></returns>
        private static Personel GetPersonel()
        {

            Personel p1 = new Personel("ismail", "Özdemir") { Yasi = 30, Departman = new Departman { Adi = "Yazılım" } };
            Personel p2 = new Personel("Funda", "Sarikaya") { Departman = new Departman { Adi = "Test" } };
            p1.Yonetici = p2;
            p2.Yonetici = p1;
            return p1;

        }


        private static JsonSerializerSettings GetJsonSettings()
        {

            return new JsonSerializerSettings()
            {
                SerializationBinder = new JsonSerializationBinder(new DefaultSerializationBinder()),
                TypeNameHandling = TypeNameHandling.All,// Property olarak interface,class gibi tanımlamaların FullName'ini json dosyasına ekler
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
                Error = (sender, args) =>
                {
                    if (args.CurrentObject == args.ErrorContext.OriginalObject
                        && args.ErrorContext.Error.InnerExceptionsAndSelf().OfType<JsonSerializationBinderException>().Any())
                    {
                        args.ErrorContext.Handled = true;
                    }
                },

                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,// Döngüye girecek referans durumunlarını yok sayar
                PreserveReferencesHandling = PreserveReferencesHandling.All, // Referans eşleştirme yapar.
                DefaultValueHandling = DefaultValueHandling.Ignore, //Default değerlere sahip alanları json'a eklenip eklenmeyeceğini belirler
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,//nesneyi oluşturmak için private constructor'lara erişim sağlar
                MissingMemberHandling = MissingMemberHandling.Ignore,// Json veride olup nesnede olmayan alanlar deserialize edilmek istendiğide hata verip yada yok sayılmasını belirler
                //ObjectCreationHandling = ObjectCreationHandling.Replace,

            };

        }


        /// <summary>
        /// Departman sınıflarınında birinin namespace'ini değiştirip varolmayan bir type'a çevirerek json'ı döner
        /// </summary>
        /// <param name="personel"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private static string ToJsonErroNameSpace(Personel personel, JsonSerializerSettings settings)
        {
            var token1 = JObject.FromObject(personel, JsonSerializer.CreateDefault(settings));

            foreach (var value in token1.Descendants().OfType<JValue>().Where(v => v.Type == JTokenType.String && v.ToString().Contains(typeof(Departman).Name)).ToList())
            {
                value.Replace((JToken)value.ToString().Replace(typeof(Departman).Name, typeof(Departman).Name + "IsNowMissing"));
                break;
            }
            return token1.ToString();

        }


    }

    public static class ExceptionExtensions
    {
        public static IEnumerable<Exception> InnerExceptionsAndSelf(this Exception ex)
        {
            while (ex != null)
            {
                yield return ex;
                ex = ex.InnerException;
            }
        }
    }
}
