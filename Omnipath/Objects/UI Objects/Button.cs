using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Omnipath
{
    /// <summary>
    /// Simple button to be used clicked on in a menu
    /// </summary>
    class Button : IDisplayable
    {
        #region Fields
        MouseState currentMouse;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new button with the upper left corner specified by the x and y parameters
        /// </summary>
        /// <param name="x">The x coordinate for this button's upper left corner</param>
        /// <param name="y">The y coordinate for this button's upper left corner</param>
        /// <param name="width">The width of this button</param>
        /// <param name="height">The height of this button</param>
        /// <param name="texture">The texture for this button</param>
        public Button(int x, int y, int width, int height, Texture2D texture)
        {
            this.Rectangle = new Rectangle(x, y, width, height);
            this.Texture = texture;
            this.Text = "";
            this.Active = true;
        }

        /// <summary>
        /// Creates a new button with the upper left corner specified by the x and y parameters
        /// </summary>
        /// <param name="x">The x coordinate for this button's upper left corner</param>
        /// <param name="y">The y coordinate for this button's upper left corner</param>
        /// <param name="width">The width of this button</param>
        /// <param name="height">The height of this button</param>
        /// <param name="texture">The texture for this button</param>
        /// <param name="text">The text overlay on this button</param>
        public Button(int x, int y, int width, int height, Texture2D texture, Texture2D mousedOverTexture = null, string text = "")
        {
            this.Rectangle = new Rectangle(x, y, width, height);
            this.Texture = texture;
            // If the mousedOverTexture was specified, use it; else, use the default texture
            if (mousedOverTexture == null)
            {
                this.MouseOverTexture = Texture;
            }
            else
            {
                this.MouseOverTexture = mousedOverTexture;
            }
            this.Text = text;
            this.Active = true;
        }

        /// <summary>
        /// Creates a new button with the center of the button specified by the Point parameter
        /// </summary>
        /// <param name="center">The point at which this button will be centered on</param>
        /// <param name="width">The width of this button</param>
        /// <param name="height">The height of this button</param>
        /// <param name="texture">The texture for this button</param>
        public Button(Point center, int width, int height, Texture2D texture)
        {
            this.Rectangle = new Rectangle(center.X - width/2, center.Y - height/2, width, height);
            this.Texture = texture;
            this.Text = "";
            this.Active = true;
        }

        /// <summary>
        /// Creates a new button with the center of the button specified by the Point parameter
        /// </summary>
        /// <param name="center">The point at which this button will be centered on</param>
        /// <param name="width">The width of this button</param>
        /// <param name="height">The height of this button</param>
        /// <param name="texture">The texture for this button</param>
        /// <param name="text">The text overlay this button</param>
        public Button(Point center, int width, int height, Texture2D texture, Texture2D mousedOverTexture = null, string text = "")
        {
            this.Rectangle = new Rectangle(center.X - width / 2, center.Y - height / 2, width, height);
            this.Texture = texture;
            // If the mousedOverTexture was specified, use it; else, use the default texture
            if (mousedOverTexture == null)
            {
                this.MouseOverTexture = Texture;
            }
            else
            {
                this.MouseOverTexture = mousedOverTexture;
            }
            this.Text = text;
            this.Active = true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Simply updates the position of the current mouse
        /// </summary>
        /// <param name="currentMouse">The current mouse</param>
        public void Update(MouseState currentMouse)
        {
            this.currentMouse = currentMouse;
        }

        /// <summary>
        /// Draws this mouse to the screen. Uses the MousedOverTexture if the button is moused over,
        /// otherwise draws using the regular texture
        /// </summary>
        /// <param name="sp"></param>
        public void Draw(SpriteBatch sp)
        {
            if (MousedOver)
            {
                sp.Draw(MouseOverTexture, Rectangle, Color.White);
            }
            else
            {
                sp.Draw(Texture, Rectangle, Color.White);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Rectangle describing this object's position
        /// </summary>
        public Rectangle Rectangle { get; set; }

        /// <summary>
        /// The default texture to be drawn when this button is not moused over
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// The texture to be displayed when this button is moused over
        /// </summary>
        public Texture2D MouseOverTexture { get; set; }

        /// <summary>
        /// The text overlayed on this button
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Whether this button's Draw method will draw it to the screen
        /// </summary>
        public bool Active { get; }

        /// <summary>
        /// Whether the mouse cursor is currently mousing over this button
        /// </summary>
        public bool MousedOver
        {
            get
            {
                return this.Rectangle.Contains(currentMouse.Position);
            }
        }
        #endregion
    }
}
