using AthameRPG.Enums;

namespace AthameRPG.Contracts
{
    public delegate void OnEvent(ISoundable soundable);

    public interface ISoundable
    {
        SoundStatus SoundStatus { get; }
        event OnEvent OnEvent;
    }
}
