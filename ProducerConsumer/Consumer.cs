using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
	public class Consumer
	{
		private readonly Producer _producer;
		private readonly TimeSpan _consumingInterval;
		private bool _running;

		public Consumer(Producer producer, TimeSpan consumingInterval)
		{
			_producer = producer;
			_consumingInterval = consumingInterval;
		}

		public void Start()
		{
			if (!_running)
			{
				_running = true;
				Task.Factory.StartNew(() =>
					{
						while (_running)
						{
							Product product;
							_producer.ProductStorrageLimit.Release();
							_producer.Products.TryDequeue(out product);
							Console.WriteLine("Product with id: {0}", product == null ? "null" : product.Id.ToString());
							Thread.Sleep(_consumingInterval);
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