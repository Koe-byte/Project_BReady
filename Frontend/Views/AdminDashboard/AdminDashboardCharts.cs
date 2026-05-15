using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ProjectBReadyWPF.Backend.Services;

namespace ProjectBReadyWPF.Frontend.Views.AdminDashboard
{
    internal static class AdminDashboardCharts
    {
        public static void DrawSparkline(Canvas canvas, IList<double> values, Brush stroke, double height = 28)
        {
            canvas.Children.Clear();
            if (values == null || values.Count == 0) return;
            if (values.Count == 1)
            {
                values = new List<double> { values[0], values[0] };
            }

            double width = canvas.ActualWidth > 0 ? canvas.ActualWidth : 72;
            double max = 0, min = double.MaxValue;
            foreach (var v in values)
            {
                if (v > max) max = v;
                if (v < min) min = v;
            }
            if (Math.Abs(max - min) < 0.01) { max += 1; min -= 1; }

            var points = new PointCollection();
            for (int i = 0; i < values.Count; i++)
            {
                double x = i / (double)(values.Count - 1) * width;
                double y = height - ((values[i] - min) / (max - min)) * height;
                points.Add(new Point(x, y));
            }

            canvas.Children.Add(new Polyline
            {
                Points = points,
                Stroke = stroke,
                StrokeThickness = 2,
                StrokeLineJoin = PenLineJoin.Round
            });
        }

        public static void DrawLineChart(Canvas canvas, IList<double> values, double maxY = 100)
        {
            canvas.Children.Clear();
            if (values == null || values.Count == 0) return;

            double w = canvas.ActualWidth > 0 ? canvas.ActualWidth : 400;
            double h = canvas.ActualHeight > 0 ? canvas.ActualHeight : 180;
            const double padL = 36, padR = 12, padT = 12, padB = 28;
            double plotW = w - padL - padR;
            double plotH = h - padT - padB;

            for (int i = 0; i <= 4; i++)
            {
                double y = padT + plotH * i / 4;
                canvas.Children.Add(new Line
                {
                    X1 = padL, X2 = padL + plotW, Y1 = y, Y2 = y,
                    Stroke = new SolidColorBrush(Color.FromRgb(226, 232, 240)),
                    StrokeThickness = 1
                });
            }

            var areaPoints = new PointCollection { new Point(padL, padT + plotH) };
            var linePoints = new PointCollection();

            for (int i = 0; i < values.Count; i++)
            {
                double x = padL + (i / (double)Math.Max(values.Count - 1, 1)) * plotW;
                double clamped = Math.Clamp(values[i], 0, maxY);
                double y = padT + plotH - (clamped / maxY) * plotH;
                linePoints.Add(new Point(x, y));
                areaPoints.Add(new Point(x, y));
            }
            areaPoints.Add(new Point(padL + plotW, padT + plotH));

            canvas.Children.Add(new Polygon
            {
                Points = areaPoints,
                Fill = new LinearGradientBrush(
                    Color.FromArgb(80, 37, 99, 235),
                    Color.FromArgb(10, 37, 99, 235),
                    new Point(0, 0), new Point(0, 1))
            });

            canvas.Children.Add(new Polyline
            {
                Points = linePoints,
                Stroke = new SolidColorBrush(Color.FromRgb(37, 99, 235)),
                StrokeThickness = 2.5,
                StrokeLineJoin = PenLineJoin.Round
            });
        }

        public static void DrawPieChart(Canvas canvas, IList<InventorySlice> slices)
        {
            canvas.Children.Clear();
            if (slices == null || slices.Count == 0) return;

            int total = 0;
            foreach (var s in slices) total += s.Quantity;
            if (total <= 0) return;

            double size = Math.Min(canvas.ActualWidth, canvas.ActualHeight);
            if (size <= 0) size = 140;
            double cx = canvas.ActualWidth / 2;
            double cy = canvas.ActualHeight / 2;
            double radius = size / 2 - 4;
            double inner = radius * 0.55;

            double startAngle = -90;
            foreach (var slice in slices)
            {
                double sweep = (double)slice.Quantity / total * 360;
                if (sweep <= 0) continue;

                var color = (Color)ColorConverter.ConvertFromString(slice.ColorHex);
                canvas.Children.Add(CreateDonutSegment(cx, cy, radius, inner, startAngle, sweep, color));
                startAngle += sweep;
            }
        }

        private static Path CreateDonutSegment(double cx, double cy, double outerR, double innerR, double startDeg, double sweepDeg, Color color)
        {
            var start = DegreesToPoint(cx, cy, outerR, startDeg);
            var end = DegreesToPoint(cx, cy, outerR, startDeg + sweepDeg);
            var innerStart = DegreesToPoint(cx, cy, innerR, startDeg + sweepDeg);
            var innerEnd = DegreesToPoint(cx, cy, innerR, startDeg);

            bool largeArc = sweepDeg > 180;
            var fig = new PathFigure { StartPoint = start, IsClosed = true };
            fig.Segments.Add(new ArcSegment(end, new Size(outerR, outerR), 0, largeArc, SweepDirection.Clockwise, true));
            fig.Segments.Add(new LineSegment(innerStart, true));
            fig.Segments.Add(new ArcSegment(innerEnd, new Size(innerR, innerR), 0, largeArc, SweepDirection.Counterclockwise, true));

            return new Path
            {
                Fill = new SolidColorBrush(color),
                Data = new PathGeometry { Figures = { fig } }
            };
        }

        private static Point DegreesToPoint(double cx, double cy, double r, double degrees)
        {
            double rad = degrees * Math.PI / 180;
            return new Point(cx + r * Math.Cos(rad), cy + r * Math.Sin(rad));
        }
    }
}
