using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using Reroll.Mobile.Core.Models.MvxMessages;
using Reroll.Mobile.Core.Interfaces;
using Reroll.Models;
using Reroll.Models.Enums;

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
                            .WithUrl("http://192.168.1.9:50794/rerollHub")
                            .Build();
            
            _connection.On<ResponseStatusEnum>("groupExistsResponse", GroupExistsResponse);
            _connection.On<string, string>("sendToAll", (user, message) =>
            {
                _messenger.Publish(new NewMessage(this, user, message));
            });
            _connection.On<Player>("sendUpdateToPlayer", ReceiveUpdateMessage);
            _connection.On<Player>("receiveInitialPlayerData", ReceiveInitialData);
            _connection.On<string>("receiveDiceRoll", ReceiveDiceMessage);
        }

        private void ReceiveInitialData(Player player)
        {
            if(Mvx.TryResolve(out IDataRepository dataRepository))
                dataRepository.Player = player;
        }

        private void ReceiveUpdateMessage(Player obj)
        {
            _messenger.Publish(new UpdateMessage(this, obj));
        }

        private void ReceiveDiceMessage(string obj)
        {
            _messenger.Publish(new DiceMessage(this, obj));
        }

        private void GroupExistsResponse(ResponseStatusEnum response)
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

        public void JoinGroup(string roomName, string roomPassword, string playerName)
        {
            _connection.InvokeAsync("joinGroup", roomName, playerName, roomPassword, false);
        }

        public void SendUpdate(Player data)
        {
            _connection.InvokeAsync("UpdateModel", data);
        }

        public void SendLog(string message)
        {
            _connection.InvokeAsync("SendActivityLog", message);
        }

        public void SendDiceRoll(int value, string diceType)
        {
            _connection.InvokeAsync("SendDiceRoll", value, diceType);
        }
    }
}
