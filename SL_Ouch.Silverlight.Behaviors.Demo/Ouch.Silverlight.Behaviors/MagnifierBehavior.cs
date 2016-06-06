using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;

using Microsoft.Expression.Media.Effects;

namespace Ouch.Silverlight.Behaviors
{
    public class MagnifierBehavior : Behavior<FrameworkElement>
    {

        private MagnifyEffect _magnifyEffect;

        public MagnifierBehavior() :
            base()
        {
            this._magnifyEffect = new MagnifyEffect();
        }

        public double Amount
        {
            get
            {
                return _magnifyEffect.Amount;
            }
            set
            {
                _magnifyEffect.Amount = value;
            }
        }

        public double InnerRadius
        {
            get
            {
                return _magnifyEffect.InnerRadius;

            }
            set
            {
                _magnifyEffect.InnerRadius = value;
            }
        }

        public double OuterRadius
        {
            get
            {
                return _magnifyEffect.OuterRadius;

            }
            set
            {
                _magnifyEffect.OuterRadius = value;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.MouseEnter += new MouseEventHandler( AssociatedObject_MouseEnter );
            this.AssociatedObject.MouseLeave += new MouseEventHandler( AssociatedObject_MouseLeave );
            this.AssociatedObject.MouseWheel += new MouseWheelEventHandler( AssociatedObject_MouseWheel );
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.MouseEnter -= new MouseEventHandler( AssociatedObject_MouseEnter );
            this.AssociatedObject.MouseLeave -= new MouseEventHandler( AssociatedObject_MouseLeave );
            this.AssociatedObject.MouseWheel -= new MouseWheelEventHandler( AssociatedObject_MouseWheel );
        }

        private void AssociatedObject_MouseLeave( object sender , MouseEventArgs e )
        {
            this.AssociatedObject.MouseMove -= new MouseEventHandler( AssociatedObject_MouseMove );
            this.AssociatedObject.Effect = null;
        }

        private void AssociatedObject_MouseEnter( object sender , MouseEventArgs e )
        {
            this.AssociatedObject.MouseMove += new MouseEventHandler( AssociatedObject_MouseMove );
            this.AssociatedObject.Effect = this._magnifyEffect;
        }

        void AssociatedObject_MouseWheel( object sender , MouseWheelEventArgs e )
        {
            if( this._magnifyEffect != null )
            {
                if( e.Delta > 0 && this.Amount <= 0.9 )
                {
                    this.Amount += 0.1;
                }
                else if( e.Delta < 0 && this.Amount >= -0.9 )
                {
                    this.Amount -= 0.1;
                }
            }
        }

        private void AssociatedObject_MouseMove( object sender , MouseEventArgs e )
        {

            //設定放大鏡的中心位置
            ( this.AssociatedObject.Effect as MagnifyEffect ).Center = e.GetPosition( this.AssociatedObject );

            //設定放大鏡的中心點為滑鼠的相對位置
            Point mousePosition = e.GetPosition( this.AssociatedObject );
            mousePosition.X /= this.AssociatedObject.ActualWidth;
            mousePosition.Y /= this.AssociatedObject.ActualHeight;
            this._magnifyEffect.Center = mousePosition;

            //撥放動畫
            Storyboard zoomInStoryboard = new Storyboard();
            DoubleAnimation zoomAnimation = new DoubleAnimation();
            zoomAnimation.To = this._magnifyEffect.Amount;
            zoomAnimation.Duration = TimeSpan.FromSeconds( 0.5 );
            Storyboard.SetTarget( zoomAnimation , this.AssociatedObject.Effect );
            Storyboard.SetTargetProperty( zoomAnimation , new PropertyPath( MagnifyEffect.AmountProperty ) );

            //動畫撥放完為停止狀態
            zoomAnimation.FillBehavior = FillBehavior.HoldEnd;
            zoomInStoryboard.Children.Add( zoomAnimation );
            zoomInStoryboard.Begin();
        }
    }
}

