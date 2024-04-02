using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusApp.Common
{
	public static class Constants
	{
		public const string ConnectionString = "Endpoint=sb://fatihozerservicebusdemo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=rQQbhP+9jqKck77qCHImX3dAiooGNP/dQ+ASbIsaOPI=";


		public const string OrderCreatedQueueName = "OrderCreatedQueue";
		public const string OrderDeletedQueueName = "OrderDeletedQueue";

		public const string OrderTopic = "OrderTopic";
		public const string OrderCreatedSubName = "OrderCreatedSubname";
		public const string OrderDeletedSubName = "OrderDeletedSubname";
	}
}
