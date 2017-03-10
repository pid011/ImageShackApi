using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShackApi
{
    public class UploadResult
    {
        public class RatingDetails
        {
            public int Ratings { get; internal set; }
            public double Average { get; internal set; }
        }

        public class ResolutionDetails
        {
            public int Width { get; internal set; }
            public int Height { get; internal set; }
        }

        public class LinkDetails
        {
            public string ImageLink { get; internal set; }
            public string ThumbLink { get; internal set; }
        }

        public RatingDetails Rating { get; set; }
        public ResolutionDetails Resolution { get; set; }
        public LinkDetails Links { get; set; }
    }
}
