using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using File = System.IO.File;
using Owin;

namespace EloRationAEBit
{
    public static class Bot
    {
        public static readonly TelegramBotClient Api = new TelegramBotClient("664976586:AAF6swbfPU194idaQhK6m5zUlDrd94opUlM");
    }
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {
            using (WebApp.Start<Startup>("https://+:8443"))
            {
                Bot.Api.GetUpdatesAsync().Wait();
                // Register WebHook
                // You should replace {YourHostname} with your Internet accessible hosname
                Bot.Api.SetWebhookAsync("https://{YourHostname}:8443/WebHook").Wait();

                Console.WriteLine("Server Started");

                // Stop Server after <Enter>
                Console.ReadLine();

                // Unregister WebHook
                Bot.Api.DeleteWebhookAsync().Wait();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }

    }
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            configuration.Routes.MapHttpRoute("WebHook", "{controller}");

            app.UseWebApi(configuration);
        }
    }

    public class WebHookController : ApiController
    {
        public async Task<IHttpActionResult> Post(Update update)
        {
            var message = update.Message;

            Console.WriteLine("Received Message from {0}", message.Chat.Id);

            if (message.Type == MessageType.Text)
            {
                // Echo each Message
                await Bot.Api.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
            else if (message.Type == MessageType.Photo)
            {
                // Download Photo
                var file = await Bot.Api.GetFileAsync(message.Photo.LastOrDefault()?.FileId);

                var filename = file.FileId + "." + file.FilePath.Split('.').Last();

                using (var saveImageStream = File.Open(filename, FileMode.Create))
                {
                    await Bot.Api.DownloadFileAsync(file.FilePath, saveImageStream);
                }

                await Bot.Api.SendTextMessageAsync(message.Chat.Id, "Thx for the Pics");
            }

            return Ok();
        }
    }
}
