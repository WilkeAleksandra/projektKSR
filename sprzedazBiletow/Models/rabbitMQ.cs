using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace sprzedazBiletow.Models
{
    public enum QueueName { loginQueue, registerQueue, searchQueue }

    public class RabbitMQ
    {
        private readonly Dictionary<QueueName, string> Queues = new Dictionary<QueueName, string>(){
                                                {QueueName.loginQueue,"loginQueue"},
                                                {QueueName.registerQueue, "registerQueue"},
                                                {QueueName.searchQueue, "searchQueue" }
                                            };
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

        public string CallAsync(string message, QueueName queuename)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(
                exchange: "",
                routingKey: Queues[queuename],
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
        public UserDataResponse SendLoginRequest(LoginRequest loginRequest)
        {
            string message = loginRequest.Login + "," + loginRequest.Password;
            Task<UserDataResponse> t = InvokeAsync(message, QueueName.loginQueue);
            t.Wait();

            return t.Result;
        }

        public UserDataResponse SendRegisterRequest(RegisterRequest registerRequest)
        {
            string message = registerRequest.Login + "," +
                registerRequest.Password + "," +
                registerRequest.FirstName + "," +
                registerRequest.LastName + "," +
                registerRequest.Email;
            Task<UserDataResponse> t = InvokeAsync(message, QueueName.registerQueue);
            t.Wait();

            return t.Result;
        }
        public UserDataResponse SendSearchRequest(SearchView searchView)
        {
            string message = searchView.startStation + "," +
                searchView.endStation + "," +
                searchView.date + "," +
                searchView.hour;
            Task<UserDataResponse> t = InvokeAsync(message, QueueName.searchQueue);
            t.Wait();

            return t.Result;
        }
        private static async Task<UserDataResponse> InvokeAsync(string message, QueueName queueName)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            var rpcClient = new RabbitMQ();

            var result = rpcClient.CallAsync(message, queueName);

            UserDataResponse loginResponse = ParseUserDataResponse(result);

            rpcClient.Close();

            return loginResponse;
        }

        private static UserDataResponse ParseUserDataResponse(string result)
        {
            string[] resultSplit = result.Split(',');
            return new UserDataResponse(
                bool.Parse(resultSplit[0]),
                int.Parse(resultSplit[1]),
                resultSplit[2],
                resultSplit[3], 
                resultSplit[4],
                resultSplit[5]);
        }
    }
}