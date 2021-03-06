﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

using System.IO;
using FacilityInfo.Management.EnumStore;
using System.Drawing.Drawing2D;

namespace FacilityInfo.Management.Helpers
{
    public static class PictureHelper
    {

        public static Image ImageFromByteArray(byte[] curArray)
        {
            Image retVal = null;
            MemoryStream memStream = new MemoryStream(curArray);
            retVal = Image.FromStream(memStream);
            return retVal;
        }

        /// <summary>
        /// Gibt ein   Thumbnail zurück Seitenverhältnis 
        /// </summary>
        /// <param name="curArray"></param>
        /// <returns></returns>
        public static Image getThumbnailPic(byte[] curArray)
        {
            Image retVal = null;
            Image workingImage = ImageFromByteArray(curArray);

            retVal = workingImage.GetThumbnailImage(64, 48, () => false, IntPtr.Zero);
            return retVal;
        }

        public static byte[] getThumbnailByteArray(byte[] curArray)
        {
            Image retVal = null;
            Image workingImage = ImageFromByteArray(curArray);
            retVal = workingImage.GetThumbnailImage(64, 48, () => false, IntPtr.Zero);
            return imageToByteArray(retVal);
        }


        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            //wenn Höhe 0 ist
            //wenn breite 0 ist
            //wenn beides ungleich 0 ist
            double ratioX = 0;
            double ratioY = 0;
            double ratio = 0;
            if (maxWidth > 0 && maxHeight > 0)
            {
                ratioX = (double)maxWidth / image.Width;
                ratioY = (double)maxHeight / image.Height;
                ratio = Math.Min(ratioX, ratioY);
            }
            if (maxWidth > 0 && maxHeight == 0)
            {
                ratio = (double)maxWidth / image.Width;
                //ratioY = (double)maxHeight / image.Height;
                //ratio = Math.Min(ratioX, ratioY);
            }
            if (maxWidth == 0 && maxHeight > 0)
            {

                ratio = (double)maxHeight / image.Height;
                //ratio = Math.Min(ratioX, ratioY);
            }



            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        public static byte[] ResizePicByWidth(Image sourceImage, double newWidth)
        {
            double sizeFactor = newWidth / sourceImage.Width;
            double newHeigth = sizeFactor * sourceImage.Height;
            Bitmap newImage = new Bitmap((int)newWidth, (int)newHeigth);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(sourceImage, new Rectangle(0, 0, (int)newWidth, (int)newHeigth));
            }
            return imageToByteArray(newImage);
        }

        public static byte[] ResizePicByHeight(Image sourceImage, double newHight)
        {
            double sizeFactor = newHight / sourceImage.Height;
            double newWidth = sizeFactor * sourceImage.Width;
            
            Bitmap newImage = new Bitmap((int)newWidth, (int)newHight);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(sourceImage, new Rectangle(0, 0, (int)newWidth, (int)newHight));
            }
            return imageToByteArray(newImage);
        }

        public static byte[] ResizePicByHeight(byte[] sourceImageArray, double newHeight)
        {
            Image sourceImage = ImageFromByteArray(sourceImageArray);
            double sizeFactor = newHeight / sourceImage.Height;
            double newWidth = sizeFactor * sourceImage.Width;

            Bitmap newImage = new Bitmap((int)newWidth, (int)newHeight);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(sourceImage, new Rectangle(0, 0, (int)newWidth, (int)newHeight));
            }
            return imageToByteArray(newImage);
        }


        public static Image SetWaterMark(Image image,Image watermark,enmVertical vPostion,enmHorizontal hPostition,Int32 widht,Int32 height)
        {

            
            Graphics imageGraphics = Graphics.FromImage(image);
            //das Ding auf die gewünschte Größe eindampfen
            Image workingMark = ScaleImage(watermark,widht,height);
            Brush watermarkBrush = new TextureBrush(workingMark);
            //Brush watermarkBrush = new TextureBrush(watermark);



            //hier die Postionierung ausrtechnen
            Console.WriteLine("h" + image.Height.ToString() + " b" + image.Width.ToString());
            //vderticale
            int y = 0;
            switch (vPostion)
            {
                case enmVertical.oben:
                    y = 0;
                    break;

                case enmVertical.unten:
                    y = image.Height-workingMark.Height;
                    break;

                case enmVertical.mitte:
                    y = (image.Height / 2) + (workingMark.Height / 2);
                    break;

            }
            //horizontal
            int x = 0;
            switch(hPostition)
            {
                case enmHorizontal.links:
                    x = 0;
                    break;

                case enmHorizontal.rechts:
                    x = image.Width - workingMark.Width;
                    break;

                case enmHorizontal.mitte:
                    x = (image.Width / 2) - (workingMark.Width / 2);
                    break;

            }
            Console.WriteLine("x" + x.ToString() + " y" + y.ToString());
            imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), workingMark.Size));
           
            var newImage = new Bitmap(image);

            Graphics.FromImage(newImage).DrawImage(image, 0, 0);
            return newImage;

        }

        public static Image SetWaterMark(Image image,Image watermark)
        {

            //die beiden Bilder verheiraten und dann wieder als Bild zurückgeben

            
            Graphics imageGraphics = Graphics.FromImage(image);
            Image workingMark = ScaleImage(watermark, watermark.Width, watermark.Height);
            Brush watermarkBrush = new TextureBrush(watermark);
            ColorMatrix myMatrix = new ColorMatrix();
            //myMatrix.
            int x = 0;

            int y = 0;
            
            imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), workingMark.Size));
            var newImage = new Bitmap(image);

            Graphics.FromImage(newImage).DrawImage(image, 0, 0);
            return newImage;


           
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image retVal = Image.FromStream(ms);
            return retVal;
        }
        public static Bitmap byteArrayToBitmap(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Bitmap retVal = new Bitmap(ms);
            return retVal;

        }
        
        public static byte[] imageToByteArray(System.Drawing.Image ImageIn)
        {
            MemoryStream ms = new MemoryStream();
            ImageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        //Ein thumbnakl erstellen und anzeigen


    }
}
