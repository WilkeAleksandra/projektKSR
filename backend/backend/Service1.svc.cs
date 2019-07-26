using DatabaseConnection.entities;
using DatabaseConnection.Repositories;

namespace backend
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę klasy „Service1” w kodzie, usłudze i pliku konfiguracji.
    // UWAGA: aby uruchomić klienta testowego WCF w celu przetestowania tej usługi, wybierz plik Service1.svc lub Service1.svc.cs w eksploratorze rozwiązań i rozpocznij debugowanie.
    public class Service1 : IService1
    {
        public Traveller GetTravellerDataByLogin(string login)
        {
            TravellerRepository travellerRepository = new DatabaseConnection.Repositories.TravellerRepository();
            return travellerRepository.FindUserByLogin(login);
        }
    }
}
