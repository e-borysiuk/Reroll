using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Plugin.Messenger;
using Reroll.Mobile.Core.Models.MvxMessages;
using Reroll.Mobile.Core.Services.Interfaces;
using Reroll.Models;

namespace Reroll.Mobile.Core.Services
{
    public class SignalrService : ISignalrService
    {
        readonly HubConnection _connection;
        readonly IMvxMessenger _messenger;

        public SignalrService(IMvxMessenger messenger)
        {
            _messenger = messenger;
            _connection = new HubConnectionBuilder()
                            .WithUrl("http://192.168.1.8:50793/rerollHub")
                            .Build();

            _connection.On<ResponseStatusEnum>("groupExistsResponse", GroupExistsResponse);
            _connection.On<string, string>("sendToAll", (user, message) =>
            {
                _messenger.Publish(new NewMessage(this, user, message));
            });

        }

        void GroupExistsResponse(ResponseStatusEnum response)
        {
            _messenger.Publish(new JoinResponseMessage(this, response));
        }

        public async Task StartConnection()
        {
            await _connection.StartAsync();
        }

        public void CheckGroupExists(string roomName, string roomPassword)
        {
            _connection.InvokeAsync("groupExists", roomName, roomPassword);
        }

        public void SendMessage(string message)
        {
            _connection.InvokeAsync("sendToAll", "mobileApp", message);
        }
    }
}
