using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace ShopWebsite.Common.Utils
{
    public class ImageHandler
    {
        public ImageHandler()
        {

        }

        private MemoryStream BytearrayToStream(byte[] arr)
        {
            return new MemoryStream(arr, 0, arr.Length);
        }

        public byte[] Resize(byte[] imgData, int maxWidth, int maxHeight)
        {
            Image img = Resize(Image.FromStream(BytearrayToStream(imgData)), maxWidth, maxHeight);

            return imgData;
        }

        private Image Resize(Image img, int maxWidth, int maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                // Double ratio = Math.Max(xRatio, yRatio);
                int newWidth = (int)Math.Floor(img.Width / xRatio);
                int newHeight = (int)Math.Floor(img.Height / yRatio);
                Bitmap newImg = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppArgb);
                using (Graphics gr = Graphics.FromImage(newImg))
                {
                    gr.Clear(Color.Transparent);

                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.CompositingQuality = CompositingQuality.HighSpeed;
                    gr.CompositingMode = CompositingMode.SourceCopy;
                    gr.SmoothingMode = SmoothingMode.HighQuality;

                    gr.DrawImage(img,
                        new Rectangle(0, 0, newWidth, newHeight),
                        new Rectangle(0, 0, img.Width, img.Height),
                        GraphicsUnit.Pixel);
                }
                return newImg;
            }
        }

        public Tuple<string, int, int> HandleImageUpload(string imgDirectories, byte[] binaryImage,
            string id, string name, int maxWidth, int maxHeight)
        {
            if (!Directory.Exists(imgDirectories)) return null;
            try
            {
                using (var stream = BytearrayToStream(binaryImage))
                {

                    using (var img = Image.FromStream(stream))
                    {
                        //img = Resize(img, maxWidth, maxHeight);
                        string imgDir = imgDirectories;
                        name = String.Format("{0}_{1}", id, name);
                        imgDir += name;
                        img.Save(imgDir);
                        return new Tuple<string, int, int>(name, img.Width, img.Height);
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetImage(string imgDirectories, string originalImgUrl, string id)
        {
            if (!Directory.Exists(imgDirectories))
            {
                return String.Empty;
            }
            DirectoryInfo imgDir = new DirectoryInfo(imgDirectories);
            var imgFiles = imgDir.GetFiles().ToList();

            string url = "";
            if (imgFiles != null || imgFiles.Count == 0)
            {
                var searchFile = imgFiles.Find(file => file.Name.Substring(0, file.Name.IndexOf('_')) == id);

                if (searchFile != null)
                {
                    url = originalImgUrl + "/" + searchFile.Name;
                }
            }

            return url;
        }

        public string GetImage(string imgDirectories, string resizedDirectories, string resizedUrl, string id, int width, int height, int type)
        {
            if (!Directory.Exists(imgDirectories) || !Directory.Exists(resizedDirectories))
            {
                return String.Empty;
            }
            DirectoryInfo imgDir = new DirectoryInfo(imgDirectories);
            var searchFile = imgDir.GetFiles().ToList()
                .Find(file => file.Name.Substring(0, file.Name.IndexOf('_')) == id);
            byte[] imgByteData = null;

            var resizedImgPath = resizedDirectories;
            string url = "";
            if (searchFile != null)
            {
                string searchPath = searchFile.FullName;

                Image img = Image.FromFile(searchPath);
                if (img == null) return null;
                imgByteData = File.ReadAllBytes(searchPath);

                img = Resize(img, width, height);
                resizedImgPath = String.Format("{0}\\{1}_{2}_{3}_{4}_{5}", resizedImgPath, id, type, width, height, searchFile.Name);
                url = String.Format("{0}_{1}_{2}_{3}_{4}", id, type, width, height, searchFile.Name);
                try
                {
                    img.Save(resizedImgPath, img.RawFormat);
                }
                catch (Exception ex)
                {
                    throw;
                }
                url = resizedUrl + "/" + url;
            }
            return url;
        }

        public List<string> GetFileId(string imgDirectories)
        {
            if (!Directory.Exists(imgDirectories))
            {
                return new List<string>();
            }
            DirectoryInfo imgDir = new DirectoryInfo(imgDirectories);
            var allFiles = imgDir.GetFiles().ToList();
            List<string> fileIds = new List<string>();
            foreach (var file in allFiles)
            {
                fileIds.Add(file.Name.Substring(0, file.Name.IndexOf('_')));
            }
            return fileIds;
        }

        public string GetImageId(string originalImgHost, string imgUrl)
        {
            string imageId = null;
            var searchHostUrlIndex = imgUrl.IndexOf(originalImgHost);
            if (searchHostUrlIndex >= 0)
            {
                imgUrl = imgUrl.Substring(searchHostUrlIndex + originalImgHost.Length + 1);
                imageId = imgUrl.Substring(0, imgUrl.IndexOf('_'));
            }
            return imageId;
        }
    }
}
