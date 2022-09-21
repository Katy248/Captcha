using System.Drawing;
using System.Text;

namespace Captcha;
class Captcha : IDisposable
{
    public Captcha(int length = 10)
    {
        answer = GenerateAnswer(length);
        image = GenerateImage(20);
    }
    private readonly string answer;
    private readonly Bitmap image;

    public Bitmap Image => image;
    public bool CheckAnswer(string answer) => string.Equals(this.answer, answer);

    private string GenerateAnswer(int length)
    {
        StringBuilder answer = new StringBuilder();
        Random R = new Random();
        for (int i = 0; i < length; i++)
        {
            answer.Insert(R.Next(0, i), (char)R.Next('A', 'Z'));
        }
        return answer.ToString();
    }
    private Bitmap GenerateImage(int height, int symWidth = 20)
    {
        Random r = new Random();

        Bitmap result = new Bitmap(symWidth * answer.Length, height);
        Graphics graphics = Graphics.FromImage(result);
        Brush[] colors = {
Brushes.Black,
Brushes.Red,
Brushes.RoyalBlue,
Brushes.Green };


        graphics.Clear(Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)));
        graphics.DrawString(answer, new Font("Consolas", 12), colors[r.Next(colors.Length)], new PointF(0, 0));

        /*for (int i = 0; i < r.Next(3, 7); i++)
        {
        graphics.DrawLine(Pens.Black, new Point(r.Next(result.Width), r.Next(result.Height)), new Point(r.Next(result.Width), r.Next(result.Height)));
        }*/

        return result;
    }
    void IDisposable.Dispose()
    {
        ((IDisposable)image).Dispose();
    }
}
