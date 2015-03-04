using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
	public class Producer
	{
		private readonly TimeSpan _productCreationInterval;
		private readonly int _queueSize;
		public readonly SemaphoreSlim ProductStorrageLimit;
		private bool _running;

		public ConcurrentQueue<Product> Products { get; set; }

		public Producer(TimeSpan productCreationInterval, int queueSize)
		{
			_productCreationInterval = productCreationInterval;
			_queueSize = queueSize;
			ProductStorrageLimit = new SemaphoreSlim(queueSize);
			Products = new ConcurrentQueue<Product>();
		}

		public void Start()
		{
			if (!_running)
			{
				_running = true;
				Task.Factory.StartNew(() =>
					{
						var id = 0;
						while (_running)
						{
							ProductStorrageLimit.Wait();
							Products.Enqueue(new Product {Id = ++id});
							Console.WriteLine("Created Product with id: {0}", id);
							Console.WriteLine("Products in storage: {0}", Products.Count);
							Thread.Sleep(_productCreationInterval);
						}
					});
			}
		}

		public void Stop()
		{
			_running = false;
		}
	}
}