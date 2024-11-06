using Assets.Scripts.Meta.Serialization;
using Assets.Scripts.Serialization.Meta;

namespace Assets.Scripts.Serialization
{
#nullable enable
    public class SaveLoader
    {
        private PlayerMeta? _playerMeta_m = null;

        private ISerialization _serialization;

        public SaveLoader(ISerialization serialization)
        {
            _serialization = serialization;
        }

        public PlayerMeta GetPlayerMeta()
        {
            if (_playerMeta_m != null) return _playerMeta_m;

            var meta = _serialization.Load<PlayerMeta>(MetaKeys.PlayerMeta);
            _playerMeta_m = meta;

            return meta;
        }

        public void SavePlayerMeta(PlayerMeta playerMeta)
        {
            _serialization.Save(MetaKeys.PlayerMeta, playerMeta);
            _playerMeta_m = playerMeta;
        }
    }
}
#nullable disable