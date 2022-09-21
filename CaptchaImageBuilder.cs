using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Captcha_K;
internal class CaptchaImageBuilder : IDisposable
{
    public CaptchaImageBuilder(int height, int width)
    {
        bitmap = new Bitmap(width, height);
        graphics = Graphics.FromImage(bitmap);
    }

    private readonly Graphics graphics;
    private readonly Bitmap bitmap;

    public CaptchaImageBuilder FillBackground(Color color)
    {
        graphics.Clear(color);
        return this;
    }
    public CaptchaImageBuilder DrawText(string text, Color textColor, Font? font = null)
    {
        font ??= new Font("Consolas", bitmap.Height / 2, FontStyle.Bold);

        graphics.DrawString(text, font, new SolidBrush(textColor), new PointF(0, 0));
        return this;
    }
    public CaptchaImageBuilder AddEffect(Action<Graphics, int, int> effectAction)
    {
        for (int x = 0; x < bitmap.Width; x++)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                effectAction.Invoke(graphics, x, y);
            }
        }
        return this;
    }
    public Bitmap Image() => bitmap; 

    public void Dispose()
    {
        graphics.Dispose();
    }
}
