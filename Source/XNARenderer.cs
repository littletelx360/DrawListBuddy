using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Filename;

namespace DrawListBuddy
{
	public class Renderer
	{
		#region Member Variables

		/// <summary>
		/// the title safe area of the game window
		/// </summary>
		private Rectangle m_TitleSafeArea;

		/// <summary>
		/// the whole game window
		/// </summary>
		private Rectangle m_ScreenRect;

		/// <summary>
		/// My own content manager, so images can be loaded separate from xml
		/// </summary>
		private ContentManager m_Content;
		private Game m_Game; //needed to initialize content manager

		//the graphics card device manager
		private GraphicsDevice m_Graphics;

		//sprite batch being used
		private SpriteBatch m_SpriteBatch;

		#endregion //Member Variables

		#region Properties

		public GraphicsDevice Graphics
		{
			get { return m_Graphics; }
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Hello, standard constructor!
		/// </summary>
		/// <param name="GameReference">Reference to the game engine</param>
		public Renderer(Game GameReference)
		{
			//set up the content manager
			Debug.Assert(null != GameReference);
			m_Game = GameReference;

			//set up the content manager
			Debug.Assert(null != m_Game);
			Debug.Assert(null == m_Content);
			m_Content = new ContentManager(m_Game.Services, "Content");

			//set up all the stuff
			m_Graphics = null;
			m_SpriteBatch = null;
		}

		/// <summary>
		/// Reload all the graphics content
		/// </summary>
		public void LoadContent(GraphicsDevice myGraphics, Rectangle screenRect, Rectangle titleSafeRect)
		{
			//grab all the member variables
			Debug.Assert(null != myGraphics);
			m_Graphics = myGraphics;
			m_ScreenRect = screenRect;
			m_TitleSafeArea = titleSafeRect;

			m_SpriteBatch = new SpriteBatch(m_Graphics);

			//setup all the rendering stuff
			Debug.Assert(null != m_Graphics);
			Debug.Assert(null != m_Graphics.BlendState);

			BlendState myBlendState = new BlendState();
			myBlendState.AlphaSourceBlend = Blend.SourceAlpha;
			myBlendState.ColorSourceBlend = Blend.SourceAlpha;
			myBlendState.AlphaDestinationBlend = Blend.InverseSourceAlpha;
			myBlendState.ColorDestinationBlend = Blend.InverseSourceAlpha;
			m_Graphics.BlendState = myBlendState;
		}

		/// <summary>
		/// Unload all the graphics content
		/// </summary>
		public void UnloadGraphicsContent()
		{
			//unload the bitmaps
			m_Content.Unload();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Load a bitmap into the system
		/// </summary>
		/// <param name="strBitmapFile">name of the bitmap to load</param>
		/// <returns>ID of the bitmap</returns>
		public Texture2D LoadBitmap(FilenameBuddy strBitmapFile)
		{
			Debug.Assert(null != strBitmapFile);
			Debug.Assert(null != m_Content);
			Debug.Assert(strBitmapFile.ToString().Length > 0);

			//create the ctexture thing
			return m_Content.Load<Texture2D>(strBitmapFile.GetRelPathFileNoExt());
		}

		public SpriteBatch SpriteBatch()
		{
			Debug.Assert(null != m_SpriteBatch);
			return m_SpriteBatch;
		}

		public ContentManager Content()
		{
			Debug.Assert(null != m_Content);
			return m_Content;
		}

		public void Draw(
			Texture2D iImageID,
			Vector2 Position,
			Color rColor, 
			float fRotation,
			bool bFlip,
			float fScale)
		{
			m_SpriteBatch.Draw(
				iImageID,
				Position,
				null,
				rColor,
				fRotation,
				Vector2.Zero,
				fScale,
				(bFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None),
				0.0f);
		}

		public void Draw(
			Texture2D iImageID,
			Rectangle Destination,
			Color rColor,
			float fRotation,
			bool bFlip)
		{
			m_SpriteBatch.Draw(
				iImageID,
				Destination,
				null,
				rColor,
				fRotation,
				Vector2.Zero,
				(bFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None),
				0.0f);
		}

		public void SpriteBatchBegin(BlendState myBlendState, Matrix translation)
		{
			m_SpriteBatch.Begin(SpriteSortMode.Immediate, //TODO: switch this to deferred sorting?
				myBlendState, 
				null,
				null,
				RasterizerState.CullNone, 
				null,
				translation);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name=""></param>
		public void SpriteBatchEnd()
		{
			m_SpriteBatch.End();
		}

		#endregion
	}
}