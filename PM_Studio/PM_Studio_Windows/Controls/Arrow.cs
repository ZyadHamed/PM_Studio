using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PM_Studio
{
    public class Arrow : Shape
    {
        

        #region Geometry

        protected override Geometry DefiningGeometry
        {
            get
            {

                // Create a StreamGeometry for Storing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                //Create a StreamGeometryContext for Creating the actual shape
                using (StreamGeometryContext context = geometry.Open())
                {
                    DrawArrowGeometry(context);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                //Return that Geometry
                return geometry;
            }
        }

        #endregion

        #region Methods

        void DrawArrowGeometry(StreamGeometryContext context)
        {
            //Get the Angle of points forming the Line
            double theta = Math.Atan2(Y1 - Y2, X1 - X2);

            //Get the Sines and the Cosines of that angle for Getting the angle of the two halfes of the arrow
            double sint = Math.Sin(theta);
            double cost = Math.Cos(theta);

            //Create a Point that Stores the MidPoint of the Line
            Point lineMidPoint = new Point((X1 + X2) / 2, (Y1 + Y2) / 2);

            //Create a Point to Store the starting point of the Line
            Point lineStartingPoint = new Point(X1, this.Y1);

            //Create a Point to Store the ending point of the line
            Point lineEndingPoint = new Point(X2, this.Y2);

            //Create a Point that Stores the Coordinates of the First half of the arrow
            //Multiply the Cosine of theta by a number to increase the width of the arrow by increasing the value of the arrow angle
            //Multiply the Sine of theta by another number to increase the height of the arrow by increasing the value of the Opposite/Hypotonise
            //where the Opposite is the height of the arrow
            //Create the Point where the X is the X of the Midpoint + the point where the arrow head and the arrow height intersect at postive X axis
            //and the Y is the Y of the MidPoint + the point where the arrow head and the arrow height intersect at postive Y axis
            Point arrowFirstHalfPoint = new Point(
                lineMidPoint.X + (cost * 6 - sint * 4),
                lineMidPoint.Y + (sint * 6 + cost * 4));

            //Create another Point that Stores the Coordinates of the Second half of the arrow
            //Multiply the Cosine of theta by a number to increase the width of the arrow by increasing the value of the arrow angle
            //Multiply the Sine of theta by another number to increase the height of the arrow by increasing the value of the Opposite/Hypotonise
            //where the Opposite is the height of the arrow
            //Create the Point where the X is the X of the Midpoint + the point where the arrow head and the arrow height intersect at negative X axis
            //and the Y is the Y of the MidPoint + the point where the arrow head and the arrow height intersect at postive Y axis(such that the points are mirroed)
            Point arrowSecondHalfPoint = new Point(
                lineMidPoint.X + (cost * 6 + sint * 4),
                lineMidPoint.Y - (cost * 4 - sint * 6));

            //Begain the Line from the first point of the Line
            context.BeginFigure(lineStartingPoint, true, false);

            //Create a Line from the starting point to the Ending Point
            context.LineTo(lineEndingPoint, true, true);

            //Move Back to the MidPoint
            context.LineTo(lineMidPoint, true, true);

            //Create a Line from the MidPoint to the Point represnting the first half of the arrow
            context.LineTo(arrowFirstHalfPoint, true, true);

            //Move Back to the MidPoint
            context.LineTo(lineMidPoint, true, true);

            //Create another Line from the MidPoint to the Point represnting the second half of the arrow
            context.LineTo(arrowSecondHalfPoint, true, true);
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty X1Property = DependencyProperty.Register("X1", typeof(double), typeof(Arrow), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty Y1Property = DependencyProperty.Register("Y1", typeof(double), typeof(Arrow), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", typeof(double), typeof(Arrow), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty Y2Property = DependencyProperty.Register("Y2", typeof(double), typeof(Arrow), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region Properties

        [TypeConverter(typeof(LengthConverter))]
        public double X1
        {
            get { return (double)base.GetValue(X1Property); }
            set { base.SetValue(X1Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Y1
        {
            get { return (double)base.GetValue(Y1Property); }
            set { base.SetValue(Y1Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double X2
        {
            get { return (double)base.GetValue(X2Property); }
            set { base.SetValue(X2Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Y2
        {
            get { return (double)base.GetValue(Y2Property); }
            set { base.SetValue(Y2Property, value); }
        }

        #endregion

    }
}
