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
        protected Rectangle rectangle;  // Position, width, height
        protected Texture2D texture;    // Texture
        protected bool active;
        protected float angle;
        protected Vector2 direction;
        protected List<Modifier> modifiers;
        protected float velocityY;
        protected float velocityX;
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
        public virtual void Update()
        {
        
        }

        /// <summary>
        /// Checks if the specified point is within this GameObject's hitbox
        /// </summary>
        /// <param name="point"></param>
        /// <returns>
        /// True if the specified point's distance to this GameObject's center is less than this GameObject's radius,
        /// else false
        /// </returns>
        public bool HitBoxContains(Point p)
        {
            return DistanceTo(p) < Radius;
        }

        /// <summary>
        /// Calculates the absolute value of the distance between this GameObject's center and the specified Point
        /// </summary>
        /// <param name="p"></param>
        /// <returns>The absolute distance between this GameObject's center and the specified Point</returns>
        public double DistanceTo(Point p)
        {
            return Math.Abs(Math.Sqrt(Math.Pow(this.Center.X - p.X, 2) + Math.Pow(this.Center.Y - p.Y, 2)));
        }

        /// <summary>
        /// Checks if the hitboxes of this GameObject and another GameObject are overlapping
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns>
        /// True if the distance between the centers of both this GameObject and the specified GameObject is less than their combined radii,
        /// else false
        /// </returns>
        public bool HitBoxContains(GameObject gameObject)
        {
            return DistanceTo(gameObject.Center) < this.Radius + gameObject.Radius;
        }

        /// <summary>
        /// Calculates and returns the distance between the edges of this GameObject's hitbox and the specified GameObject's hitbox
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns>
        /// The distance between the edges of this GameObject's hitbox and the specified GameObject's hitbox
        /// A positive value indicates these GameObjects are not overlapping, a negative value indicates that they are
        /// </returns>
        public double EdgeDistance(GameObject gameObject)
        {
            return DistanceTo(gameObject.Center) - (this.Radius + gameObject.Radius);
        }

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

        /// <summary>
        /// Pathfinds from current position to the specified point
        /// </summary>
        public void AStar(Point destination)
        {

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

        /// <summary>
        /// The radius of this GameObject's hitbox
        /// </summary>
        public int Radius
        {
            get;
        }

        /// <summary>
        /// A vector representing this object's velocity
        /// </summary>
        public Vector2 Velocity => Vector2.Normalize(new Vector2(velocityX, velocityY)) * Speed;

        /// <summary>
        /// A scalar representing this object's speed
        /// </summary>
        public float Speed { get; set; }
        #endregion
    }
}
