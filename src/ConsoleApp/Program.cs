using System;
using ServiceStack.Redis;

namespace ConsoleApp
{
    class Program
    {
        const string ChannelName = "ASP_CORE_CHAN";
        const int PublishMessageCount = 5;
        static void Main(string[] args)
        {
            var host = "redis";
            var messagesReceived = 0;

            using (var redisConsumer = new RedisClient(host))
            using (var subscription = redisConsumer.CreateSubscription())
            {
                subscription.OnSubscribe = channel =>
                {
                    Console.WriteLine(String.Format("Subscribed to '{0}'", channel));
                };
                subscription.OnUnSubscribe = channel =>
                {
                    Console.WriteLine(String.Format("UnSubscribed from '{0}'", channel));
                };
                subscription.OnMessage = (channel, msg) =>
                {
                    Console.WriteLine(String.Format("Received '{0}' from channel '{1}'", msg, channel));

                    if (++messagesReceived == PublishMessageCount)
                    {
                        subscription.UnSubscribeFromAllChannels();
                    }
                };

                Console.WriteLine("Start Subscription!");
                subscription.SubscribeToChannels(ChannelName);
            }
        }
    }
}
