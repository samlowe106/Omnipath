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
    abstract class GameObject : IDisplayable
    {
        #region Fields
        // MonoGame-relevant fields
        protected Rectangle rectangle;  // Hitbox, position, width, height
        protected Texture2D texture;    // Texture
        protected bool active;
        protected float angle;
        protected Vector2 direction;
        #endregion

        #region Constructor
        public GameObject(Rectangle rectangle, Texture2D texture)
        {
            this.rectangle = rectangle;
            this.texture = texture;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Draws this object to the screen
        /// </summary>
        /// <param name="sp">SpriteBatch with which to draw this object to the screen</param>
        public void Draw(SpriteBatch sp)
        {
            if (this.active)
            {
                sp.Draw(texture, rectangle, Color.White);
            }
        }

        /// <summary>
        /// Updates this object
        /// </summary>
        public virtual void Update() { }
        #endregion

        #region Methods
        /// <summary>
        /// Orients this object to look at the specified subject
        /// </summary>
        /// <param name="subject">The IDisplayable to face towards</param>
        public void FaceTowards(IDisplayable subject)
        {
            FaceTowards(subject.Rectangle.Center.X, subject.Rectangle.Center.Y);
        }

        /// <summary>
        /// Orients this object to look at the specified x y coordinate pair
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void FaceTowards(int x, int y)
        {
            this.direction.X = x - this.Center.X;
            this.direction.Y = y - this.Center.Y;

            // Plug Y and X into arctangent to calculate the angle
            angle = (float)Math.Atan2(direction.Y, direction.X);
        }

        #endregion

        #region Properties
        /// <summary>
        /// The angle that this object is facing
        /// </summary>
        public float Angle
        {
            get
            {
                return angle;
            }
        }
        
        /// <summary>
        /// The middle point of this object, equal to this.Rectangle.Center
        /// </summary>
        public Point Center
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
        public Texture2D Texture
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
