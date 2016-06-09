namespace AthameRPG.Controls
{
    using System;

    using Microsoft.Xna.Framework;

    public class InputStateExtended<S> where S : struct
    {
        public InputStateExtended(GameTime gameTime, S state)
        {
            StateTime = gameTime.TotalGameTime;
            State = state;
        }
        public TimeSpan StateTime { get; set; }
        public S State { get; set; }
    }
}
