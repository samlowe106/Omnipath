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
    /// Object from which most objects in the game world will inherit
    /// </summary>
    abstract class GameObject
    {
        #region Fields
        // MonoGame-relevant fields
        protected Rectangle rectangle;  // Hitbox, position, width, height
        protected Texture2D texture;    // Texture
        protected bool active;
        #endregion

        #region Constructor
        public GameObject(Rectangle rectangle, Texture2D texture)
        {
            this.rectangle = rectangle;
            this.texture = texture;
        }
        #endregion

        #region Methods
        public void Draw(SpriteBatch sp)
        {
            if (this.active)
            {
                sp.Draw(texture, rectangle, Color.White);
            }
        }

        public virtual void Update()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// The middle point of this object, equal to this.Rectangle.Center
        /// </summary>
        public Point Position
        {
            get
            {
                return rectangle.Center;
            }
        }

        /// <summary>
        /// This object's rectangle, defining where it exists in space
        /// </summary>
        public Rectangle Rectangle
        {
            get
            {
                return this.Rectangle;
            }
        }

        /// <summary>
        /// This object's texture
        /// </summary>
        public Texture Texture
        {
            get
            {
                return texture;
            }
        }

        /// <summary>
        /// Whether this object is active
        /// </summary>
        public bool Active
        {
            get
            {
                return active;
            }
        }
        #endregion
    }
}
