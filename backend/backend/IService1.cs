using DatabaseConnection.entities;
using System.ServiceModel;

namespace backend
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę interfejsu „IService1” w kodzie i pliku konfiguracji.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        Traveller GetTravellerDataByLogin(string login);

        // TODO: dodaj tutaj operacje usługi
    }
}
