using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Runtime.Remoting.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace MidLandPath
{
   



    internal class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static DbManager db = new DbManager();
        static void Main(string[] args)
        { 
            var bot = new TelegramBotClient("7678294611:AAGi-5MLzA7b4J8Usc3U09MssLbmMcLwKSY");
            bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync);
            Console.ReadLine();
        }
        private static Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw exception;
        }
        private static async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;
            User user = new User();
            user.TgId = message.Chat.Id.ToString();
            string botToken = "7678294611:AAGi-5MLzA7b4J8Usc3U09MssLbmMcLwKSY";
            string chatId = message.Chat.Id.ToString();
            if (message != null && message.Text == "/start")
            { 
                string photoPath = @"C:\Users\vasil\OneDrive\Рабочий стол\Проект картинки\MidlandMap.jpg";
                await SendPhotoAsync(botToken, chatId, photoPath);
                await client.SendTextMessageAsync(message.Chat.Id, "Добро пожаловать в Средиземье, мир Вашего приключения!");
                await client.SendTextMessageAsync(message.Chat.Id, "Напишите /info чтобы узнать доступные команды");
            }
            if (message != null && message.Text == "/info")
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Доступные команды: /beginjourney - начать приключение заново, /addsteps - ввести пройденные Вами шаги");
            }
            if (message != null && message.Text == "/beginjourney")
            {
                db.AddPlayer(user);
                string photoPath = @"C:\Users\vasil\OneDrive\Рабочий стол\Проект картинки\Shir.jpg";
                await SendPhotoAsync(botToken, chatId, photoPath);
                await client.SendTextMessageAsync(message.Chat.Id, "Ваш путь, как и путь Фродо, начинается из Шира");
                await client.SendTextMessageAsync(message.Chat.Id, "Ваши шаги: 0");
            }
            if (message != null && message.Text.Contains("/addsteps"))
            {
                db.AddPlayerSteps(int.Parse(message.Chat.Id.ToString()), int.Parse(message.Text.Split()[1]));
                await client.SendTextMessageAsync(message.Chat.Id, $"Ваши шаги: {db.GetPlayerSteps(int.Parse(message.Chat.Id.ToString()))}");
                if(db.GetLocation(int.Parse(message.Chat.Id.ToString())) == 0 && db.GetPlayerSteps(int.Parse(message.Chat.Id.ToString())) >= 240000)
                {
                    db.SetLocation(int.Parse(message.Chat.Id.ToString()), 1);
                    string photoPath = @"C:\Users\vasil\OneDrive\Рабочий стол\Проект картинки\Bri.png";
                    await SendPhotoAsync(botToken, chatId, photoPath);
                    await client.SendTextMessageAsync(message.Chat.Id, "Вы прибываете в деревню Бри!");
                }
                if (db.GetLocation(int.Parse(message.Chat.Id.ToString())) == 1 && db.GetPlayerSteps(int.Parse(message.Chat.Id.ToString())) >= 840000)
                {
                    db.SetLocation(int.Parse(message.Chat.Id.ToString()), 2);
                    string photoPath = @"C:\Users\vasil\OneDrive\Рабочий стол\Проект картинки\Rivendell.jpg";
                    await SendPhotoAsync(botToken, chatId, photoPath);
                    await client.SendTextMessageAsync(message.Chat.Id, "Вы прибываете в Ривенделл, город, в котором образовалось Братство Кольца!");
                }
                if (db.GetLocation(int.Parse(message.Chat.Id.ToString())) == 2 && db.GetPlayerSteps(int.Parse(message.Chat.Id.ToString())) >= 1190000)
                {
                    db.SetLocation(int.Parse(message.Chat.Id.ToString()), 3);
                    string photoPath = @"C:\Users\vasil\OneDrive\Рабочий стол\Проект картинки\Lotlorien.jpg";
                    await SendPhotoAsync(botToken, chatId, photoPath);
                    await client.SendTextMessageAsync(message.Chat.Id, "Вы прибываете в  Лотлориэн, волшебный лес эльфов! Отсюда Братство Кольца отправилось в Парт Гален на лодках, что упростило путь и им, и Вам.");
                }
                if (db.GetLocation(int.Parse(message.Chat.Id.ToString())) == 3 && db.GetPlayerSteps(int.Parse(message.Chat.Id.ToString())) >= 1510000)
                {
                    db.SetLocation(int.Parse(message.Chat.Id.ToString()), 4);
                    string photoPath = @"C:\Users\vasil\OneDrive\Рабочий стол\Проект картинки\Black.jpg";
                    await SendPhotoAsync(botToken, chatId, photoPath);
                    await client.SendTextMessageAsync(message.Chat.Id, "Вы прибываете к Темным Вратам Мордора!");
                }
                if (db.GetLocation(int.Parse(message.Chat.Id.ToString())) == 4 && db.GetPlayerSteps(int.Parse(message.Chat.Id.ToString())) >= 1730000)
                {
                    db.SetLocation(int.Parse(message.Chat.Id.ToString()), 5);
                    string photoPath = @"C:\Users\vasil\OneDrive\Рабочий стол\Проект картинки\Minas.jpg";
                    await SendPhotoAsync(botToken, chatId, photoPath);
                    await client.SendTextMessageAsync(message.Chat.Id, "Вы прибываете в крепость Минас Моргул!");
                }
                if (db.GetLocation(int.Parse(message.Chat.Id.ToString())) == 5 && db.GetPlayerSteps(int.Parse(message.Chat.Id.ToString())) >= 1870000)
                {
                    db.SetLocation(int.Parse(message.Chat.Id.ToString()), 6);
                    string photoPath = @"C:\Users\vasil\OneDrive\Рабочий стол\Проект картинки\Mountain.jpeg";
                    await SendPhotoAsync(botToken, chatId, photoPath);
                    await client.SendTextMessageAsync(message.Chat.Id, "Вы прибываете к Роковой Горе! Поздравляем, Ваше приключение окончено!");
                }
            }

        }
        static async Task SendPhotoAsync(string botToken, string chatId, string photoPath)
        {
            var url = $"https://api.telegram.org/bot{botToken}/sendPhoto";

            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent(chatId), "chat_id");
                form.Add(new StreamContent(System.IO.File.OpenRead(photoPath))
                {
                    Headers =
                {
                    ContentType = new MediaTypeHeaderValue("image/jpeg") // Adjust according to your image type
                }
                }, "photo", System.IO.Path.GetFileName(photoPath));
                var response = await client.PostAsync(url, form);
                response.EnsureSuccessStatusCode(); // Throws an exception if the request failed
            }
        }
    }
}
