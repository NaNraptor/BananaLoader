using System;
#pragma warning disable 0108

namespace BananaLoader
{
    public abstract class BananaMod : BananaBase
    {
        [Obsolete()]
        public BananaModInfoAttribute InfoAttribute { get => LegacyModInfo; }
        [Obsolete()]
        public BananaModGameAttribute[] GameAttributes { get => LegacyModGames; }

        public virtual void OnLevelIsLoading() {}
        public virtual void OnLevelWasLoaded(int level) {}
        public virtual void OnLevelWasInitialized(int level) {}
        public virtual void OnFixedUpdate() {}
    }
}