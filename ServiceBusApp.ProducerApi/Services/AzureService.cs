using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus.Management;
using Newtonsoft.Json;
using ServiceBusApp.Common;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusApp.ProducerApi.Services
{
	public class AzureService
	{
		private readonly ManagementClient _managementClient;
		public AzureService(ManagementClient managementClient)
		{
			_managementClient = managementClient;
		}
		public async Task SendMessageToQueue(string queueName, object messageContent, string messageType = null)
		{
			IQueueClient queueClient = new QueueClient(Constants.ConnectionString, queueName);
			await SendMessage(queueClient, messageContent, messageType) ;
		}

		public async Task CreateQueueIfNotExist(string queueName)
		{
			if (!await _managementClient.QueueExistsAsync(queueName))
				await _managementClient.CreateQueueAsync(queueName);
		}

		public async Task SendMessageToTopic(string topicName, object messageContent, string messageType = null)
		{
			ITopicClient topicClient = new TopicClient(Constants.ConnectionString, topicName);
			await SendMessage(topicClient, messageContent, messageType);
		}

		public async Task CreateSubscriptionIfNotExists(string topicName, string subscriptionName, string messageType = null, string ruleName = null)
		{
			if (!await _managementClient.SubscriptionExistsAsync(topicName, subscriptionName))
				return;

			if (messageType != null)
			{
				SubscriptionDescription subscriptionDescription = new SubscriptionDescription(topicName, subscriptionName);

				CorrelationFilter correlationFilter = new CorrelationFilter();
				correlationFilter.Properties["MessageType"] = messageType;

				RuleDescription ruleDescription = new RuleDescription(ruleName?? messageType + "Rule", correlationFilter);

				await _managementClient.CreateSubscriptionAsync(subscriptionDescription, ruleDescription);
			}
			else
			{
				await _managementClient.CreateSubscriptionAsync(topicName, subscriptionName);
			}
		}

		public async Task CreateTopicIfNotExist(string topicName)
		{
			if (!await _managementClient.TopicExistsAsync(topicName))
				await _managementClient.CreateTopicAsync(topicName);
		}

		private async Task SendMessage(ISenderClient client, object messageContent, string messageType = null)
		{
			var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageContent));

			var message = new Message(byteArray);
			message.UserProperties["MessageType"] = messageType;

			await client.SendAsync(message);
		}

	}
}
