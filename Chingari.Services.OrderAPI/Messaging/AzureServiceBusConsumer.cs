using Azure.Messaging.ServiceBus;
using Chingari.Services.OrderAPI.Models;
using Chingari.Services.OrderAPI.Repository;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Chingari.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionCheckOut;
        private readonly string checkoutMessageTopic;

        private readonly OrderRepository _orderRepository;
        private ServiceBusProcessor checkOutProcessor;

        private readonly IConfiguration _configuration;

        public AzureServiceBusConsumer(OrderRepository orderRepository, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionCheckOut = _configuration.GetValue<string>("SubscriptionCheckOut");
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");
            
            var client = new ServiceBusClient(serviceBusConnectionString);

            checkOutProcessor = client.CreateProcessor(checkoutMessageTopic, subscriptionCheckOut);
        }

        public async Task Start()
        {
            checkOutProcessor.ProcessMessageAsync += OnCheckOutMessageReceived;
            checkOutProcessor.ProcessErrorAsync += ErrorHandler;
            await checkOutProcessor.StartProcessingAsync();
        }
        public async Task Stop()
        {
            await checkOutProcessor.StopProcessingAsync();
            await checkOutProcessor.DisposeAsync();
        }
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnCheckOutMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            OrderDetails checkoutHeaderDto = JsonConvert.DeserializeObject<OrderDetails>(body);

            OrderDetails orderDetails = new()
            {
                Name = checkoutHeaderDto.Name,
                Price = checkoutHeaderDto.Price,
                Description = checkoutHeaderDto.Description,
                CategoryName = checkoutHeaderDto.CategoryName,
                Count = checkoutHeaderDto.Count,
            };
            await _orderRepository.AddOrder(orderDetails);
        }
    }
}
