using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace sprzedazBiletow.Models
{
    public class RabbitMQ
    {
        private const string QUEUE_NAME = "loginQueue";

        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;

        public RabbitMQ()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    respQueue.Add(response);
                }
            };
        }

        public string CallAsync(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(
                exchange: "",
                routingKey: QUEUE_NAME,
                basicProperties: props,
                body: messageBytes);

            channel.BasicConsume(
                consumer: consumer,
                queue: replyQueueName,
                autoAck: true);

            return respQueue.Take();
        }

        public void Close()
        {
            connection.Close();
        }
    }

    public class Rpc
    {
        public LoginResponse sendMessage(string login, string password)
        {
            string message = login + "," + password;
            Task<LoginResponse> t = InvokeAsync(message);
            t.Wait();

            return t.Result;
        }

        private static async Task<LoginResponse> InvokeAsync(string message)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            var rpcClient = new RabbitMQ();

            var result = rpcClient.CallAsync(message);
            LoginResponse loginResponse = ParseLoginResponse(result);

            rpcClient.Close();

            return loginResponse;
        }

        private static LoginResponse ParseLoginResponse(string result)
        {
            string[] resultSplit = result.Split(',');
            return new LoginResponse(
                bool.Parse(resultSplit[0]),
                int.Parse(resultSplit[1]),
                resultSplit[2],
                resultSplit[3], 
                resultSplit[4],
                resultSplit[5]);
        }
    }
}