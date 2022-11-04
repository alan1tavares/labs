using System.Text;

namespace PocJwt
{
    public static class SecretKey
    {
        public static string Get()
        {
            return "de1fde61701241c798fceca76b375bfd";
        }

        public static byte[] GetWithEncoding()
        {
            return Encoding.UTF8.GetBytes(Get());
        }

    }
}