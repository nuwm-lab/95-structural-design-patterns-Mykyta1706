using System;
using System.Text;

namespace LabWork10
{
    // 1. Цільовий інтерфейс (те, що очікує наша система)
    public interface ISocialNetwork
    {
        void PostMessage(string message);
    }

    // 2. Існуючий клас (Стара соцмережа, наприклад Facebook)
    public class FacebookService : ISocialNetwork
    {
        public void PostMessage(string message)
        {
            Console.WriteLine($"Facebook: Опубліковано пост - '{message}'");
        }
    }

    // 3. Сторонній сервіс (Нове API з іншими іменами методів)
    // Він не реалізує наш інтерфейс ISocialNetwork
    public class TikTokApi
    {
        public void UploadVideoContent(string videoData)
        {
            Console.WriteLine($"TikTok API: Відео контент '{videoData}' завантажено через нове API.");
        }
    }

    // 4. Адаптер - робить TikTokApi сумісним з ISocialNetwork
    public class TikTokAdapter : ISocialNetwork
    {
        private readonly TikTokApi _tikTokApi;

        public TikTokAdapter(TikTokApi api)
        {
            _tikTokApi = api;
        }

        public void PostMessage(string message)
        {
            // Адаптер перетворює виклик PostMessage у зрозумілий для TikTok виклик
            _tikTokApi.UploadVideoContent(message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №10: Патерн Адаптер ===\n");

            // Працюємо через інтерфейс
            ISocialNetwork facebook = new FacebookService();
            facebook.PostMessage("Привіт, друзі!");

            // Підключаємо нове API через адаптер
            TikTokApi newApi = new TikTokApi();
            ISocialNetwork tikTok = new TikTokAdapter(newApi);

            // Тепер наша система думає, що працює зі звичайною соцмережею
            tikTok.PostMessage("Мій новий челендж");

            Console.WriteLine("\nСистема успішно інтегрувала нове API через Адаптер.");
            Console.ReadKey();
        }
    }
}