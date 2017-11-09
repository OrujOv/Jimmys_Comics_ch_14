using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media.Imaging;

namespace Jimmys_Comics_ch_14_
{
    class ComicQuery
    {
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public BitmapImage Image { get; private set; }

        public ComicQuery (string title, string subTitle, string description, BitmapImage image)
        {
            Title = title;
            Subtitle = subTitle;
            Description = description;
            Image = image;
        }
    }
}
