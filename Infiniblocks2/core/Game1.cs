using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace InfiniBlocks2
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		#region Variables
		public static readonly Random RNG = new Random();
		public static readonly Color[] blockColors = new Color[]{Color.Yellow, Color.DarkOrange, Color.Red, 
			Color.Lime, Color.DodgerBlue, Color.BlueViolet, 
			Color.LightGoldenrodYellow, Color.PeachPuff, 
			Color.LightSalmon, Color.PaleGreen, Color.LightSkyBlue, 
			Color.MediumPurple, Color.Gold, Color.Lavender};
		public static readonly Color[] powerColors = new Color[3]{Color.Red, Color.Green, Color.Blue};
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		GameStateEnumeration gameState;
		KeyboardState currKey, oldKey;
		MouseState currMouse, oldMouse;

		IntroState gameIntro;
		MenuState gameMenu;
		InPlayState gameInPlay;
		OutroState gameOutro;

		//Art Assets
		public Texture2D[] introArt;
		public Texture2D[] menuArt;
		public Texture2D[] InPlayArt;
		public Texture2D[] outroArt;
		private SpriteFont debugFont;
		private SpriteFont scoreFont;
		private SpriteFont menuFont;
		private SpriteFont pauseFont;
		// public static SoundEffect[] gameEffects;
		// public static Song[] gameMusic;
		#endregion

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);

			//Set graphics preferences
			graphics.PreferredBackBufferHeight = 768;
			graphics.PreferredBackBufferWidth = 1366;
			graphics.IsFullScreen = true;
			this.IsMouseVisible = true;

			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			gameState = GameStateEnumeration.Intro;
			//Init inputs
			currKey = Keyboard.GetState();
			oldKey = currKey;
			currMouse = Mouse.GetState();
			oldMouse = currMouse;

			base.Initialize();
		}

		protected override void LoadContent()
		{
			// TODO move out into seperate classes
			spriteBatch = new SpriteBatch(GraphicsDevice);
			#region Loading Art Content
			debugFont = Content.Load<SpriteFont>(".\\fonts\\DebugFont");
			scoreFont = Content.Load<SpriteFont>(".\\fonts\\ScoreFont");
			menuFont = Content.Load<SpriteFont>(".\\fonts\\MenuFont");
			pauseFont = Content.Load<SpriteFont>(".\\fonts\\PauseFont");

			introArt = new Texture2D[3];
			introArt[0] = Content.Load<Texture2D>(".\\images\\transition\\intro\\IntroBackground");
			introArt[1] = Content.Load<Texture2D>(".\\images\\transition\\intro\\Logo");
			introArt[2] = Content.Load<Texture2D>(".\\images\\transition\\intro\\LogoMask");

			menuArt = new Texture2D[16];
			menuArt[0] = Content.Load<Texture2D>(".\\images\\menu\\MainMenuBack");
			menuArt[1] = Content.Load<Texture2D>(".\\images\\menu\\NewGameBack");
			menuArt[2] = Content.Load<Texture2D>(".\\images\\menu\\HighScoreBack");
			menuArt[3] = Content.Load<Texture2D>(".\\images\\menu\\SettingsBack");
			menuArt[4] = Content.Load<Texture2D>(".\\images\\menu\\NewGameButton");
			menuArt[5] = Content.Load<Texture2D>(".\\images\\menu\\HighScoreButton");
			menuArt[6] = Content.Load<Texture2D>(".\\images\\menu\\SettingsButton");
			menuArt[7] = Content.Load<Texture2D>(".\\images\\menu\\ExitGameButton");
			menuArt[8] = Content.Load<Texture2D>(".\\images\\menu\\EasyGameButton");
			menuArt[9] = Content.Load<Texture2D>(".\\images\\menu\\MediumGameButton");
			menuArt[10] = Content.Load<Texture2D>(".\\images\\menu\\HardGameButton");
			menuArt[11] = Content.Load<Texture2D>(".\\images\\menu\\Setting1Button");
			menuArt[12] = Content.Load<Texture2D>(".\\images\\menu\\Setting2Button");
			menuArt[13] = Content.Load<Texture2D>(".\\images\\menu\\Setting3Button");
			menuArt[14] = Content.Load<Texture2D>(".\\images\\menu\\EmptyButton");
			menuArt[15] = Content.Load<Texture2D>(".\\images\\menu\\BackGameButton");

			InPlayArt = new Texture2D[10];
			InPlayArt[0] = Content.Load<Texture2D>(".\\images\\game\\Bat");
			InPlayArt[1] = Content.Load<Texture2D>(".\\images\\game\\Ball");
			InPlayArt[2] = Content.Load<Texture2D>(".\\images\\game\\Block");
			InPlayArt[3] = Content.Load<Texture2D>(".\\images\\game\\PowerUp");
			InPlayArt[4] = Content.Load<Texture2D>(".\\images\\game\\InPlayBackground");
			InPlayArt[5] = Content.Load<Texture2D>(".\\images\\game\\Border");
			InPlayArt[6] = Content.Load<Texture2D>(".\\images\\game\\PlayingField");
			InPlayArt[7] = Content.Load<Texture2D>(".\\images\\game\\InfoScreen");
			InPlayArt[8] = Content.Load<Texture2D>(".\\images\\game\\PauseScreen");
			InPlayArt[9] = Content.Load<Texture2D>(".\\images\\game\\BackTile");

			outroArt = new Texture2D[1];
			outroArt[0] = Content.Load<Texture2D>(".\\images\\transition\\outro\\OutroBackground");
			/*
			gameEffects = new SoundEffect[8];
			gameEffects[0] = Content.Load<SoundEffect>(".\\audio\\effects\\ButtonPress");
			gameEffects[1] = Content.Load<SoundEffect>(".\\audio\\effects\\GameOver");
			gameEffects[2] = Content.Load<SoundEffect>(".\\audio\\effects\\HitBlock");
			gameEffects[3] = Content.Load<SoundEffect>(".\\audio\\effects\\LoseLife");
			gameEffects[4] = Content.Load<SoundEffect>(".\\audio\\effects\\NewLevel");
			gameEffects[5] = Content.Load<SoundEffect>(".\\audio\\effects\\PowerUp");
			gameEffects[6] = Content.Load<SoundEffect>(".\\audio\\effects\\ServeBall");
			gameEffects[7] = Content.Load<SoundEffect>(".\\audio\\effects\\HitBat");

			gameMusic = new Song[8];
			gameMusic[0] = Content.Load<Song>(".\\audio\\music\\InPlayMusic01");
			gameMusic[1] = Content.Load<Song>(".\\audio\\music\\InPlayMusic02");
			gameMusic[2] = Content.Load<Song>(".\\audio\\music\\InPlayMusic03");
			gameMusic[3] = Content.Load<Song>(".\\audio\\music\\InPlayMusic04");
			gameMusic[4] = Content.Load<Song>(".\\audio\\music\\InPlayMusic05");
			gameMusic[5] = Content.Load<Song>(".\\audio\\music\\IntroMusic");
			gameMusic[6] = Content.Load<Song>(".\\audio\\music\\OutroMusic");
			gameMusic[7] = Content.Load<Song>(".\\audio\\music\\TitleTheme");

			MediaPlayer.IsRepeating = true;
			*/
			//Initialise Intro
			gameIntro = new IntroState(introArt);
			gameMenu = new MenuState(menuArt, menuFont);
			gameInPlay = new InPlayState(InPlayArt, scoreFont, pauseFont);
			gameOutro = new OutroState(outroArt);

			#endregion
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			currMouse = Mouse.GetState();
			currKey = Keyboard.GetState();

			//**Remove this when polishing code
			if (currKey.IsKeyDown(Keys.P))
			{
				this.Exit();
			}

			#region GameState Loop
			switch (gameState)
			{
			case GameStateEnumeration.Intro:

				gameIntro.UpdateMe(gameTime, ref gameState, currKey, oldKey);

				break;

			case GameStateEnumeration.Menu:

				//Put in some bool to check if reinit is needed
				gameMenu.UpdateMe(currKey, oldKey, currMouse, oldMouse, ref gameState, ref gameInPlay);

				break;

			case GameStateEnumeration.InPlay:

				gameInPlay.UpdateMe(gameTime, ref gameState, currKey, oldKey);

				break;

			case GameStateEnumeration.Outro:

				gameOutro.UpdateMe(gameTime, ref gameState);

				break;

			case GameStateEnumeration.Exit:

				this.Exit();

				break;
			}
			#endregion

			oldMouse = currMouse;
			oldKey = currKey;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();

			#region GameState Loop
			switch (gameState)
			{
			case GameStateEnumeration.Intro:

				gameIntro.DrawMe(spriteBatch);

				break;

			case GameStateEnumeration.Menu:

				gameMenu.DrawMe(spriteBatch);

				break;

			case GameStateEnumeration.InPlay:

				gameInPlay.DrawMe(spriteBatch, gameTime);

				break;

			case GameStateEnumeration.Outro:

				gameOutro.DrawMe(spriteBatch);

				break;

			case GameStateEnumeration.Exit:

				this.Exit();

				break;
			}
			#endregion

			//spriteBatch.DrawString(debugFont, 

			spriteBatch.End();

			base.Draw(gameTime);
		}

	}
}