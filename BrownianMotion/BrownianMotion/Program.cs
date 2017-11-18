using System;

namespace BrownianMotion
{
    #if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BrownianMotionGame game = new BrownianMotionGame())
            {
                game.Run();
            }
        }
    }
    #endif
}

