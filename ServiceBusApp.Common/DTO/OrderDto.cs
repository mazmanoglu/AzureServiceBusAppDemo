﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusApp.Common.DTO
{
	public class OrderDto
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
	}
}
