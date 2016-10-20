using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimaXNA.Core.Graphics;

namespace SpriteBatchTest
{
	public class TestGame : Game
	{
		GraphicsDeviceManager GraphicsDeviceManager;
		SpriteBatchUI UI;
		SpriteBatch sp;
		Texture2D tex;

		public TestGame()
		{
			GraphicsDeviceManager = new GraphicsDeviceManager(this);
			GraphicsDeviceManager.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
			GraphicsDeviceManager.PreparingDeviceSettings += OnPreparingDeviceSettings;
			Window.AllowUserResizing = false;
			GraphicsDeviceManager.PreferredBackBufferWidth = 640;
			GraphicsDeviceManager.PreferredBackBufferHeight = 480;
			GraphicsDeviceManager.ApplyChanges();
		}

		protected override void Initialize()
		{
			sp = new SpriteBatch(GraphicsDevice);
			UI = new SpriteBatchUI(this);
			tex = new Texture2D(GraphicsDevice, 128, 128, false, SurfaceFormat.Bgra5551);
			tex.SetData(File.ReadAllBytes("tex.data"));
			//tex = Texture2D.FromStream(GraphicsDeviceManager.GraphicsDevice, File.OpenRead("tex.bmp"));
		}

		private static void OnPreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
		{
			e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
		}

		protected override void Draw(GameTime time)
		{
			GraphicsDevice.Clear(Color.White);

			// this actually works fine 
			/*
			//sp.Begin();
			//sp.Draw(tex, new Rectangle(0, 0, 128, 128), Color.Blue);
			//sp.End();
			*/

			// this doesn't (the actual test case)
			UI.Reset();
			UI.Draw2D(tex, new Vector3(0, 0, 0), new Vector3(128, 128, 128));
			UI.FlushSprites(false);
		}
	}

	public static class Test
	{
		static public void Main(string[] args)
		{
			(new TestGame()).Run();
		}
	}
}
