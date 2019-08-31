using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sprzedazBiletow.Models
{

    public class Rpc
    {
        public Cities Cities = new Cities();

        public UserDataResponse SendLoginRequest(LoginRequest loginRequest)
        {
            string message = loginRequest.Login + "?" + loginRequest.Password;
            Task<string> t = InvokeAsync(message, QueueName.loginQueue);
            t.Wait();

            UserDataResponse loginResponse = ParseUserDataResponse(t.Result);

            return loginResponse;
        }

        public bool SendBuyRequest(string train, string user, string from, string to)
        {
            string message = train + "?" + user + "?" + from + "?" + to;
            Task<string> t = InvokeAsync(message, QueueName.buyQueue);
            t.Wait();
            bool result = ParseBuyResponse(t.Result);
            return result;
        }

        public UserDataResponse SendRegisterRequest(RegisterRequest registerRequest)
        {
            string message = registerRequest.Login + "?" +
                registerRequest.Password + "?" +
                registerRequest.FirstName + "?" +
                registerRequest.LastName + "?" +
                registerRequest.Email;
            Task<string> t = InvokeAsync(message, QueueName.registerQueue);
            t.Wait();

            UserDataResponse registerResponse = ParseUserDataResponse(t.Result);

            return registerResponse;
        }

        public List<SearchResponse> SendSearchRequest(SearchRequest searchRequest)
        {
            string message = searchRequest.date.ToString() + "?" +
                Cities.list[Convert.ToInt32(searchRequest.startStation)-1].Name + "?" +
                Cities.list[Convert.ToInt32(searchRequest.endStation)-1].Name;
            //+ "," + searchRequest.hour
            Task<string> t = InvokeAsync(message, QueueName.searchQueue);
            t.Wait();

            List<SearchResponse> searchResponse = ParseSearchResponse(t.Result);

            return searchResponse;
        }


        private static async Task<string> InvokeAsync(string message, QueueName queueName)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            var rpcClient = new RabbitMQ();

            var result = rpcClient.CallAsync(message, queueName);

            rpcClient.Close();

            return result;
        }

        private static UserDataResponse ParseUserDataResponse(string result)
        {
            string[] resultSplit = result.Split('?');
            return new UserDataResponse(
                bool.Parse(resultSplit[0]),
                int.Parse(resultSplit[1]),
                resultSplit[2],
                resultSplit[3],
                resultSplit[4],
                resultSplit[5]);
        }

        private static bool ParseBuyResponse(string result)
        {
            string[] resultSplit = result.Split('?');
            return bool.Parse(resultSplit[0]);
        }

        private List<SearchResponse> ParseSearchResponse(string result)
        {
            List<SearchResponse> resultList = new List<SearchResponse>();

            string[] splitToSearchResponseList = result.Split(';');
            foreach(string searchResponse in splitToSearchResponseList)
            {
                string[] splitToSearchResponse = searchResponse.Split('?');
                resultList.Add(new SearchResponse(
                    splitToSearchResponse[0],
                    splitToSearchResponse[1],
                    splitToSearchResponse[2],
                    splitToSearchResponse[3],
                    splitToSearchResponse[4],
                    splitToSearchResponse[5]));
            }
            return resultList;
        }
    }
}