using System;
#pragma warning disable 0108

namespace BananaLoader
{
    public abstract class BananaPlugin : BananaBase
    {
        [Obsolete()]
        public BananaPluginInfoAttribute InfoAttribute { get => LegacyPluginInfo; }
        [Obsolete()]
        public BananaPluginGameAttribute[] GameAttributes { get => LegacyPluginGames; }

        public virtual void OnPreInitialization() { }
    }
}