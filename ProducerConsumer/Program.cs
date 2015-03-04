using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
	class Program
	{
		static void Main(string[] args)
		{
			var producer = new Producer(TimeSpan.FromMilliseconds(900), 10);
			producer.Start();
			Thread.Sleep(TimeSpan.FromSeconds(2));

			var consumer = new Consumer(producer, TimeSpan.FromMilliseconds(1200));
			consumer.Start();

			Console.ReadLine();
		}
	}
}
