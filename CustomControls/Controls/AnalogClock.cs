using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CustomControls.Controls
{
    public class AnalogClock : Control
    {
        Line hourHand;
        Line minuteHand;
        Line secondHand;

        static AnalogClock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogClock), new FrameworkPropertyMetadata(typeof(AnalogClock)));
        }

        public override void OnApplyTemplate()
        {
            hourHand = Template.FindName("PART_HourHand", this) as Line;
            minuteHand = Template.FindName("PART_MinuteHand", this) as Line;
            secondHand = Template.FindName("PART_SecondHand", this) as Line;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += (s, e) => UpdateHandAngles();
            timer.Start();

            base.OnApplyTemplate();
        }

        private void UpdateHandAngles()
        {
            int hours = (DateTime.Now.Hour <= 12) ? DateTime.Now.Hour : DateTime.Now.Hour - 12;
            hours = (hours != 0) ? hours : hours + 12;
            int minutes = DateTime.Now.Minute;
            int seconds = DateTime.Now.Second;

            double angleMinuteHand = (minutes / 60.0) * 360;
            double angleSecondMinuteHand = (seconds / 60.0) * 360;
            double angleHourHand = (hours / 12.0) * 360 + (minutes / 12) * 6;

            hourHand.RenderTransform = new RotateTransform(angleHourHand, 0.5, 0.5);
            minuteHand.RenderTransform = new RotateTransform(angleMinuteHand, 0.5, 0.5);
            secondHand.RenderTransform = new RotateTransform(angleSecondMinuteHand, 0.5, 0.5);
        }
    }
}
