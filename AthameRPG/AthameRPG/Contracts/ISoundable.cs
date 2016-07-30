using AthameRPG.Enums;

namespace AthameRPG.Contracts
{
    public delegate void OnClick(ISoundable soundable);

    public interface ISoundable
    {
        SoundStatus SoundStatus { get; }
        event OnClick OnClick;
    }
}
