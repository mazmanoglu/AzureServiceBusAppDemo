using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusApp.Common
{
	public static class Constants
	{
		public const string ConnectionString = "Connection";

		public const string OrderCreatedQueueName = "OrderCreatedQueue";
		public const string OrderDeletedQueueName = "OrderDeletedQueue";

		public const string OrderTopic = "OrderTopic";
		public const string OrderCreatedSubName = "OrderCreatedSubname";
		public const string OrderDeletedSubName = "OrderDeletedSubname";
	}
}
