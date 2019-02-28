using App.Client.GameModules.Player;
using App.Shared.Components.Player;
using App.Shared.GameModules.Weapon;
using App.Shared.Player;
using Core.Utils;
using Entitas;

namespace App.Shared.GameModules.Player
{
    public class PlayerDeadAnimSystem : AbstractGamePlaySystem<PlayerEntity>
    {
        private static LoggerAdapter _logger = new LoggerAdapter(typeof(PlayerDeadAnimSystem));
<<<<<<< HEAD
=======
        private IGroup<PlayerEntity> _players;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        private Contexts _contexts;

        public PlayerDeadAnimSystem(Contexts context) : base(context)
        {
            _contexts = context;
        }

        protected override IGroup<PlayerEntity> GetIGroup(Contexts contexts)
        {
            return contexts.player.GetGroup(PlayerMatcher.AllOf(PlayerMatcher.StateInterface,
                PlayerMatcher.AppearanceInterface, PlayerMatcher.GamePlay));//PlayerMatcher.PlayerAction));
        }

        protected override bool Filter(PlayerEntity entity)
        {
            return true;
        }

        protected override void OnGamePlay(PlayerEntity playerEntity)
        {
<<<<<<< HEAD
            if(null == playerEntity || null == playerEntity.playerGameState ||
               null == playerEntity.characterControllerInterface) 
                return;
            
            var gameState = playerEntity.playerGameState;
            switch (gameState.CurrentPlayerLifeState)
            {
                case PlayerLifeStateEnum.Reborn:
                    Reborn(playerEntity);
                    break;
                case PlayerLifeStateEnum.Revive:
                    Revive(playerEntity);
                    break;
                case PlayerLifeStateEnum.Dying:
                    Dying(playerEntity);
                    break;
                case PlayerLifeStateEnum.Dead:
                    Dead(playerEntity);
                    break;
            }
=======
            if(null == playerEntity || null == playerEntity.gamePlay ||
               null == playerEntity.characterControllerInterface) 
                return;
            return;
            var gamePlay = playerEntity.gamePlay;
            
            if (!gamePlay.HasLifeStateChangedFlag()) return;
            if(CreatePlayerGameStateData(playerEntity)) return;

            if (gamePlay.IsLifeState(EPlayerLifeState.Alive) &&
                gamePlay.IsLastLifeState(EPlayerLifeState.Dead))
                Reborn(playerEntity);
            
            if (gamePlay.IsLifeState(EPlayerLifeState.Alive) &&
                gamePlay.IsLastLifeState(EPlayerLifeState.Dying))
                Revive(playerEntity);
            
            if(gamePlay.IsLifeState(EPlayerLifeState.Dying))
                Dying(playerEntity);

            if (gamePlay.IsLifeState(EPlayerLifeState.Dead))
                Dead(playerEntity);
        }
        
        private static bool CreatePlayerGameStateData(PlayerEntity player)
        {
            var gamePlay = player.gamePlay;
            var playerGameState = player.playerGameState;
            if(null == playerGameState || null == gamePlay) return true;
            
            if (PlayerSystemEnum.PlayerDeadAnim == playerGameState.CurrentPlayerSystemState)
            {
                _logger.InfoFormat("ChangeClearInSystem:  {0}", playerGameState.CurrentPlayerSystemState);
                gamePlay.ClearLifeStateChangedFlag();
                playerGameState.CurrentPlayerSystemState = PlayerSystemEnum.NullState;
                return true;
            }
            
            if (PlayerSystemEnum.NullState == playerGameState.CurrentPlayerSystemState)
                playerGameState.CurrentPlayerSystemState = PlayerSystemEnum.PlayerDeadAnim;

            return false;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }
        
        private void Reborn(PlayerEntity player)
        {
<<<<<<< HEAD
            _logger.InfoFormat("{0} play rebirth", player.entityKey);                   
            player.characterControllerInterface.CharacterController.PlayerReborn();
        }

        private void Dead(PlayerEntity player)
        {
            _logger.InfoFormat("{0} play die", player.entityKey);
                    
            player.WeaponController().ForceUnArmHeldWeapon();
            player.characterControllerInterface.CharacterController.PlayerDead();
            _logger.InfoFormat("PlayerDeadAnimDead");
        }

        private void Dying(PlayerEntity player)
        {
            _logger.InfoFormat("{0} play dying", player.entityKey);
            player.WeaponController().ForceUnArmHeldWeapon();
        }

=======
            _logger.InfoFormat("{0} play rebirth", player.entityKey);
                        
            player.characterControllerInterface.CharacterController.PlayerReborn();
            player.RootGo().BroadcastMessage("PlayerRelive");
        }

        private void Dead(PlayerEntity player)
        {
            _logger.InfoFormat("{0} play die", player.entityKey);
                    
            player.WeaponController().ForceUnmountCurrWeapon(_contexts);
            player.characterControllerInterface.CharacterController.PlayerDead();
            player.RootGo().BroadcastMessage("PlayerDead");
            _logger.InfoFormat("PlayerDeadAnimDead");
        }

        private void Dying(PlayerEntity player)
        {
            _logger.InfoFormat("{0} play dying", player.entityKey);
            player.WeaponController().ForceUnmountCurrWeapon(_contexts);
        }

>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        private void Revive(PlayerEntity player)
        {
            _logger.InfoFormat("{0} play revive", player.entityKey);
        }
    }
}