using System;
using System.Collections.Generic;
using System.IO;
using ExLeafSoftApplication.iOS;
using Foundation;
using ExLeafSoftApplication.Common;
using UIKit;
using Xamarin.Forms;
using CoreGraphics;
using System.Drawing;

[assembly: Dependency(typeof(FileHelper))]
namespace ExLeafSoftApplication.iOS
{

    //https://stackoverflow.com/questions/17352061/fastest-way-to-convert-image-to-byte-array
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
         
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // libFolder = Path.Combine(docFolder,"/..");
            //a++;
                //libFolder = Path.Combine(docFolder,filename);
            
            //string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
             //libFolder = docFolder;
            //if (!Directory.Exists(libFolder))
            //{
            //    Directory.CreateDirectory(libFolder);
            //}

            return Path.Combine(docFolder, filename);
        }

        public string[] GetPhotoPathList(string fieldGuid)
        {
            string[] files = null;

            var documentsDirectory = Environment.GetFolderPath
                         (Environment.SpecialFolder.MyDocuments);

            var directory = Path.Combine(documentsDirectory, "Photos");
            if(Directory.Exists(directory))
             files = Directory.GetFiles(directory,"NEW_*");

            return files;
        }

        public void SendFilesNameChanged(List<FileMetaInformation> sendfileList)
        {
            var documentsDirectory = Environment.GetFolderPath
                       (Environment.SpecialFolder.MyDocuments);

            var leafsoftPhotosdirectory = Path.Combine(documentsDirectory, "LeafSoftPhotos");
            
            if(!Directory.Exists(leafsoftPhotosdirectory))
            Directory.CreateDirectory(leafsoftPhotosdirectory);

            string cameraFolderPath = CameraFolderPath();
            
            foreach (var item in sendfileList)
            {

                //string leanFileName = System.IO.Path.GetFileNameWithoutExtension(item.filePath);
                //string directory = System.IO.Path.GetDirectoryName(item.filePath);

                byte[] decodedStr = System.Convert.FromBase64String(item.encodedFile);
                string newfilepath = System.IO.Path.Combine(leafsoftPhotosdirectory, "Send_" + item.leanFileName + ".jpg");

                using (var imageFile = new FileStream(newfilepath, FileMode.Create, FileAccess.Write))
                {
                    imageFile.Write(decodedStr, 0, decodedStr.Length);
                    imageFile.Flush();
                }

                string oldfilepath = System.IO.Path.Combine(cameraFolderPath, item.leanFileName + ".jpg");
                File.Delete(oldfilepath);


            }

        }

        public List<FileMetaInformation> GetImages(string[] files)
        {
            List<FileMetaInformation> sendfileList = new List<FileMetaInformation>();

            int count = 0;
            foreach (string path in files)
            {
                var memoryStream = new MemoryStream();


                count++;

                if (count > 5)
                    break;

                FileStream fs = File.OpenRead(path);
                fs.CopyTo(memoryStream);
                
               
               
                byte[] a = memoryStream.ToArray();
                string encoded = System.Convert.ToBase64String(a);

                string directory = System.IO.Path.GetDirectoryName(path);
                sendfileList.Add(new FileMetaInformation { leanFileName = System.IO.Path.GetFileNameWithoutExtension(path), encodedFile = encoded });
                fs.Close();
                //image.Recycle();
                memoryStream.Close();



            }

            if (sendfileList.Count > 0)
            {
                SendFilesNameChanged(sendfileList);
            }
            //}

            return sendfileList;
             
        }

        public string CameraFolderPath()
        {
            var documentsDirectory = Environment.GetFolderPath
                       (Environment.SpecialFolder.MyDocuments);

            var directory = Path.Combine(documentsDirectory, "Photos");

            return directory;
        }

        public void ShowMessage(string message)
        {
            BigTed.BTProgressHUD.ShowToast(message, toastPosition: BigTed.ProgressHUD.ToastPosition.Bottom, timeoutMs: 1200);
        }

        public List<FileMetaInformation> GetTumbNailImages(string fieldGuid)
        {
            string[] fileList = GetPhotoPathList(fieldGuid);
            List<FileMetaInformation> sendfileList = new List<FileMetaInformation>();
            foreach (string path in fileList)
            {
                FileStream fs = File.OpenRead(path);
                var memoryStream = new MemoryStream();
                fs.CopyTo(memoryStream);
                byte[] a = memoryStream.ToArray();
                byte[] images = ResizeImageIOS(a, 100, 100, 100);
                fs.Close();
                memoryStream.Close();
                sendfileList.Add(new FileMetaInformation { orjinalImage = images, leanFileName = System.IO.Path.GetFileNameWithoutExtension(path) });
            }

            return sendfileList;
        }


        private byte[] ResizeImageIOS(byte[] fs, float width, float height, int quality)
        {
            UIImage originalImage = ImageFromByteArray(fs);

            

            float oldWidth = (float)originalImage.Size.Width;
            float oldHeight = (float)originalImage.Size.Height;
            float scaleFactor = 0f;

            if (oldWidth > oldHeight)
            {
                scaleFactor = width / oldWidth;
            }
            else
            {
                scaleFactor = height / oldHeight;
            }

            float newHeight = oldHeight * scaleFactor;
            float newWidth = oldWidth * scaleFactor;

            UIImage  scaledimage = originalImage.Scale(new CGSize(newWidth, newHeight));

            //create a 24bit RGB image
            //using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
            //    (int)newWidth, (int)newHeight, 8,
            //    (int)(4 * newWidth), CGColorSpace.CreateDeviceRGB(),
            //    CGImageAlphaInfo.PremultipliedFirst))
            //{

            //    RectangleF imageRect = new RectangleF(0, 0, newWidth, newHeight);

            //    // draw the image
            //    context.DrawImage(imageRect, originalImage.CGImage);

            //    UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage());

            //    // save the image as a jpeg
            //    return resizedImage.AsJPEG((float)quality).ToArray();
            //}
            return scaledimage.AsJPEG(quality).ToArray();
        }

        public static UIKit.UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            UIKit.UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }


    }
}
