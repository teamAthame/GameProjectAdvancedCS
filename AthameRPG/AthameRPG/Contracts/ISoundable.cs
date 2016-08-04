namespace AthameRPG.Contracts
{
    using AthameRPG.Enums;

    public delegate void OnEvent(ISoundable soundable);

    public interface ISoundable
    {
        SoundStatus SoundStatus { get; }
        event OnEvent OnEvent;
    }
}
