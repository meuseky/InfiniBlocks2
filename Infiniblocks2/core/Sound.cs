using System;
//using Microsoft.Xna.Framework.Audio;

namespace InfiniBlocks2
{
	static class Sound
	{
		// Make this a global class perhaps

		//private static SoundEffectInstance blockHit = Game1.gameEffects[2].CreateInstance();
		public static bool soundOn = true;

		public static bool SoundOn
		{
			get
			{
				return soundOn;
			}
			set
			{
				soundOn = value;
			}
		}

		static Sound()
		{

		}

		public static void PlayEffect(int track)
		{
			if (soundOn == true)
			{
				//Game1.gameEffects[track].Play();
			}
			/*
            if (track == 2)
            {
                SoundEffectInstance blockHit = Game1.gameEffects[2].CreateInstance();
                blockHit.Play();
            }
            else
            {
                Game1.gameEffects[track].Play();
            }
            */
		}

		public static void StartTrack(int track)
		{
			if (soundOn == true)
			{
				if (track == 0)
				{
					track = Game1.RNG.Next(0, 4);
				}
				// TODO make MediaPlayer global
				// MediaPlayer.Play(Game1.gameMusic[track]);
			}
		}

		public static void StopTrack()
		{
			// TODO make MediaPlayer global
			// MediaPlayer.Stop();
		}
	}
}

