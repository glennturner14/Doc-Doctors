using DocDoctors.Models;
using System;

namespace DocDoctors.DAL
{
    public class ClientGateway
    {
        public ClientCollection GetClients()
        {
            ClientCollection clients = new ClientCollection();
            clients.Add(new Client() { ClientId = 1, Name = "Glenn" });
            clients.Add(new Client() { ClientId = 2, Name = "Dan" });

            return clients;
        }
    }
}
