using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBusApp.Common;
using ServiceBusApp.Common.DTO;
using ServiceBusApp.Common.Events;
using ServiceBusApp.ProducerApi.Services;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace ServiceBusApp.ProducerApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly AzureService _azureService;
		public OrderController(AzureService azureService)
		{
			_azureService = azureService;
		}


		[HttpPost]
		public async Task CreateOrder(OrderDto order)
		{
			// insert order into database

			var orderCreatedEvent = new OrderCreatedEvent()
			{
				Id = order.Id,
				ProductName = order.ProductName,
				CreatedOn = DateTime.Now
			};

			//await _azureService.CreateQueueIfNotExist(Constants.OrderCreatedQueueName);
			//await _azureService.SendMessageToQueue(Constants.OrderCreatedQueueName, orderCreatedEvent);

			await _azureService.CreateTopicIfNotExist(Constants.OrderTopic);
			await _azureService.CreateSubscriptionIfNotExists(Constants.OrderTopic, Constants.OrderCreatedSubName, "OrderCreated", "OrderCreatedOnly");

			await _azureService.SendMessageToTopic(Constants.OrderTopic, orderCreatedEvent, "OrderCreated");
		}

		[HttpDelete("{id}")]
		public async Task DeleteOrder(int id)
		{
			// delete order
			var orderDeletedEvent = new OrderDeletedEvent()
			{
				Id = id,
				CreatedOn = DateTime.Now
			};

			//await _azureService.CreateQueueIfNotExist(Constants.OrderDeletedQueueName);
			//await _azureService.SendMessageToQueue(Constants.OrderDeletedQueueName, orderDeletedEvent);

			await _azureService.CreateTopicIfNotExist(Constants.OrderTopic);
			await _azureService.CreateSubscriptionIfNotExists(Constants.OrderTopic, Constants.OrderDeletedSubName, "OrderDeleted", "OrderDeletedOnly");

			await _azureService.SendMessageToTopic(Constants.OrderTopic, orderDeletedEvent, "OrderDeleted");
		}

	}
}
