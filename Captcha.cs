using System.Drawing;
using System.Text;

namespace Captcha_K;
public class Captcha : IDisposable
{
    public Captcha(int length = 10)
    {
        answer = GenerateAnswer(length);
        image = GenerateImage(200, 90 * length);
    }
    public Captcha(string captchaText)
    {
        answer = captchaText;
        image = GenerateImage(200, 90 * captchaText.Length);
    }
    private readonly string answer;
    private readonly Bitmap image;
    private static Random random = new Random();

    public Bitmap Image => image;
    public bool CheckAnswer(string answer) => string.Equals(this.answer, answer);

    private string GenerateAnswer(int length)
    {
        StringBuilder answer = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            answer.Insert(random.Next(0, i), (char)random.Next('A', 'Z'));
        }
        return answer.ToString();
    }
    private Bitmap GenerateImage(int height, int width)
    {
        using (CaptchaImageBuilder cib = new CaptchaImageBuilder(height, width))
            return cib
                .FillBackground(RandomColor())
                .DrawText(answer, Color.White)
                .AddEffect((gr, x, y) =>
                {
                    if (x % 5 != 0) return;
                    using (SolidBrush sb = new SolidBrush(RandomColor()))
                    using (Pen pen = new Pen(new SolidBrush(RandomColor())))
                        gr.DrawRectangle(pen, x, y, 2, 2);
                })
                .Image();
    }
    Color RandomColor() => Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
    void IDisposable.Dispose()
    {
        image.Dispose();
    }
}
