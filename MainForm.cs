using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RandomEncounters
{
    public class MainForm : Form
    {
        private const int ElapsedTimeMilliseconds = 1000 / Constants.Fps;
        private const int BaseResetTimeMilliseconds = ElapsedTimeMilliseconds * Constants.Fps * Constants.Interval;
        private const int MaxResetTimeOffsetMilliseconds = BaseResetTimeMilliseconds / 2;

        private readonly ImageProvider _imageProvider;
        private readonly Random _random;

        private readonly Stopwatch _stopwatch;

        private Image _currentImage;
        private int _resetIn;

        private float _xPos;
        private float _yPos;

        public MainForm(ImageProvider imageProvider, Random random)
        {
            _imageProvider = imageProvider;
            _random = random;

            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            _currentImage = imageProvider.GetImage();
            _resetIn = GetResetTime();
            _xPos = -_currentImage.Width;

            SetupStyle();
            StartLoop();
        }

        private new bool Update()
        {
            var actualPos = _xPos + _currentImage.Width;

            if (_stopwatch.ElapsedMilliseconds > _resetIn && actualPos <= 0)
            {
                _currentImage = _imageProvider.GetImage();
                _xPos = Width;
                _resetIn = GetResetTime();
                _stopwatch.Restart();
            }
            else if (actualPos > 0)
            {
                _xPos -= Constants.MoveSpeed;

                var multiplier = (float) Math.Cos(_xPos * Math.PI / 180 * Constants.Bounciness);
                var actual = Height / 2 - _currentImage.Height / 2;

                _yPos = actual + actual * multiplier;

                // Seems that TopMost has to be reloaded all the time
                // or else the application loses focus.
                TopMost = true;
                
                return true;
            }
            return false;
        }

        private void Render(Graphics graphics)
        {
            graphics.DrawImage(_currentImage, _xPos, _yPos);
        }

        private int GetResetTime()
        {
            return BaseResetTimeMilliseconds + _random.Next(MaxResetTimeOffsetMilliseconds);
        }

        private void Tick(object sender, EventArgs e)
        {
            if (Update())
            {
                Invalidate();
            }
        }

        private void StartLoop()
        {
            var timer = new Timer
            {
                Interval = ElapsedTimeMilliseconds
            };

            timer.Tick += Tick;
            timer.Start();
        }

        private void SetupStyle()
        {
            FormBorderStyle = FormBorderStyle.None;
            TransparencyKey = BackColor;
            Bounds = Screen.PrimaryScreen.Bounds;
            ShowInTaskbar = false;
            DoubleBuffered = true;
            TopMost = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Render(e.Graphics);
        }
    }
}