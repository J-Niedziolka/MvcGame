
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using MvcGame.Models;
using System.Text;

namespace Todo.Controllers
{
    public class ServiceBusController : Controller
    {
        private readonly QueueClient _queueClient;

        public ServiceBusController()
        {
            _queueClient = new QueueClient("Endpoint=sb://janekprojektqueue.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=jyojCq8JPyHPFhqJTGX2RYnrzITezr+dQ+ASbD1oTQo=",
                "janekprojektq");
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            var messageToSend = new Message(Encoding.UTF8.GetBytes(message));
            Console.WriteLine(messageToSend);
            await _queueClient.SendAsync(messageToSend);
            await _queueClient.CloseAsync();

            Console.WriteLine(message);
            return RedirectToAction("Index");
        }



        public ActionResult Index()
        {
            return View();
        }
    }
}