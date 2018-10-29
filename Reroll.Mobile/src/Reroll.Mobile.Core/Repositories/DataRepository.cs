using MvvmCross;
using MvvmCross.Plugin.Messenger;
using Reroll.Mobile.Core.Interfaces;
using Reroll.Mobile.Core.Models.MvxMessages;
using Reroll.Models;

namespace Reroll.Mobile.Core.Repositories
{
    public class DataRepository : IDataRepository
    {
        public DataRepository()
        {
            _messenger = Mvx.Resolve<IMvxMessenger>();
            this._messageToken = this._messenger.Subscribe<UpdateMessage>(ReceivedUpdate);
            this.PlayerModel = new PlayerModel();
        }

        IMvxMessenger _messenger;
        MvxSubscriptionToken _messageToken;

        private PlayerModel _playerModel;
        public PlayerModel PlayerModel
        {
            get => _playerModel;
            set
            {
                _playerModel = value;
                RefreshUi();
            }
        }

        void ReceivedUpdate(UpdateMessage obj)
        {
            this.PlayerModel = obj.PlayerModel;
        }
        
        private void RefreshUi()
        {
            this._messenger.Publish(new RefreshMessage(this));
        }
    }
}
