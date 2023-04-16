using PluginInterface;
using System.Drawing;

namespace PluginTwo
{
    [Version(1, 0)]
    public class BlackAndWhitePlugin : IPlugin
    {
        public string Name
        {
            get
            {
                return "Оттенки серого";
            }
        }

        public string Author
        {
            get
            {
                return "Reapka";
            }
        }

        public Bitmap Transform(Bitmap bitmap)
        {
            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    int val = (color.R + color.G + color.B) / 3;
                    bitmap.SetPixel(i, j, Color.FromArgb(val,val,val));
                }
            return bitmap;
        }
    }
}
