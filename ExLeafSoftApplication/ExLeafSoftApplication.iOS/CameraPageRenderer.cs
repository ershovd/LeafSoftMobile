using AssetsLibrary;
using AVFoundation;
using CoreGraphics;
using ExLeafSoftApplication.iOS;
using ExLeafSoftApplication.Views;
using Foundation;
using ImageIO;
using System;
using System.IO;
using System.Reflection;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CameraPage), typeof(CameraPageRenderer))]
namespace ExLeafSoftApplication.iOS
{
    public class CameraPageRenderer : PageRenderer
    {
        AVCaptureSession captureSession;
        AVCaptureDeviceInput captureDeviceInput;
        AVCaptureStillImageOutput stillImageOutput;
        UIView liveCameraStream;
        UIButton takePhotoButton;
        UIButton toggleCameraButton;
        UIButton toggleFlashButton;
        VisualElement MainPage;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            MainPage = e.NewElement;

         

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                SetupUserInterface();
                SetupEventHandlers();
                SetupLiveCameraStream();
                AuthorizeCameraUse();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"			ERROR: ", ex.Message);
            }
        }
        //https://michaelridland.com/xamarin/creating-native-view-xamarin-forms-viewpage/
        private UIView ConvertFormsToNative(Picker view, CGRect size)
        {

            var renderer = Platform.GetRenderer(view);
            
             renderer.NativeView.Frame = size;

            //renderer.NativeView.AutoresizingMask = UIViewAutoresizing.None;
            //renderer.NativeView.ContentMode = UIViewContentMode.Center;

            renderer.Element.Layout(size.ToRectangle());

            var nativeView = renderer.NativeView;

            nativeView.SetNeedsDisplayInRect(size);

            return nativeView;
        }

        void SetupUserInterface()
        {
            
           
            //UIView a = this.NativeView;
            //Type type =  a.GetType();
            //PropertyInfo[] infos = type.GetProperties();

            var centerButtonX = View.Bounds.GetMidX() - 35f;
            var topLeftX = View.Bounds.X + 25;
            var topRightX = View.Bounds.Right - 65;
            var bottomButtonY = View.Bounds.Bottom - 150;
            var topButtonY = View.Bounds.Top + 15;
            var buttonWidth = 70;
            var buttonHeight = 70;
            
            
            liveCameraStream = new UIView()
            {
                Frame = new CGRect(0f, 0f,320f, View.Bounds.Height)
            };

          

            takePhotoButton = new UIButton()
            {
                Frame = new CGRect(centerButtonX, bottomButtonY, buttonWidth, buttonHeight)
            };
            takePhotoButton.SetBackgroundImage(UIImage.FromFile("TakePhotoButton.png"), UIControlState.Normal);

            toggleCameraButton = new UIButton()
            {
                Frame = new CGRect(topRightX, topButtonY + 5, 35, 26)
            };
            toggleCameraButton.SetBackgroundImage(UIImage.FromFile("ToggleCameraButton.png"), UIControlState.Normal);

            toggleFlashButton = new UIButton()
            {
                Frame = new CGRect(topLeftX, topButtonY, 37, 37)
            };
            toggleFlashButton.SetBackgroundImage(UIImage.FromFile("NoFlashButton.png"), UIControlState.Normal);


            //tmp.Add(tmp);
           
            View.Add(liveCameraStream);
            Picker newpicker = MainPage.FindByName<Picker>("CropPicker");
            double x = newpicker.X;

            View.Add(ConvertFormsToNative(newpicker, new CGRect(View.Bounds.Location.X/2, View.Bounds.Location.Y / 2, 150, 35)));
            View.Add(takePhotoButton);
            View.Add(toggleCameraButton);
            View.Add(toggleFlashButton);

          
            
            
          
           


        }

        void SetupEventHandlers()
        {
            takePhotoButton.TouchUpInside += (object sender, EventArgs e) => {
                CapturePhoto();
            };

            toggleCameraButton.TouchUpInside += (object sender, EventArgs e) => {
                ToggleFrontBackCamera();
            };

            toggleFlashButton.TouchUpInside += (object sender, EventArgs e) => {
                ToggleFlash();
            };
        }

        async void CapturePhoto()
        {
            var videoConnection = stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
            var sampleBuffer = await stillImageOutput.CaptureStillImageTaskAsync(videoConnection);
            var jpegImage = AVCaptureStillImageOutput.JpegStillToNSData(sampleBuffer);

            var photo = new UIImage(jpegImage);

            NSData imgData = photo.AsJPEG(0.8f);

            string timestr = DateTime.Now.Ticks.ToString();

            //The Folllowing is an answer to quesition that how to Get the selected Crop and Image Name
            Models.CropModel SelectedCrop = null;
            string selectedImageName = (MainPage as CameraPage).GetViewModel.Parent.SelectedImagePath;
            if ((MainPage as CameraPage).GetViewModel.SelectedCrop != null)
                SelectedCrop = (MainPage as CameraPage).GetViewModel.SelectedCrop;
            //End of


            var documentsDirectory = Environment.GetFolderPath
                        (Environment.SpecialFolder.MyDocuments);

            var directory = Path.Combine(documentsDirectory, "Photos");
            //string[] files = Directory.GetFiles(directory);

            if (!Directory.Exists(directory))
            {



                Directory.CreateDirectory(directory);
            }

            string jpgFilename = System.IO.Path.Combine(directory, "NEW_IOS_" + timestr + "_Photo.jpg"); // hardcoded filename, overwritten each time



            NSError err = null;
            if (imgData.Save(jpgFilename, false, out err))
            {
                BigTed.BTProgressHUD.ShowToast("Photo is taken!!!", toastPosition: BigTed.ProgressHUD.ToastPosition.Bottom, timeoutMs: 1200);
                //Console.WriteLine("saved as " + jpgFilename);
            }
            else
            {
                BigTed.BTProgressHUD.ShowToast("Photo is not taken!!!", toastPosition: BigTed.ProgressHUD.ToastPosition.Bottom, timeoutMs: 1200);
                //Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
            }

           await MainPage.Navigation.PopModalAsync();
        }


        //public static void WriteImageToSavedPhotosAlbum(UIImage photo, string imagePath)
        //{
        //    CGImageSource myImageSource;
        //    myImageSource = CGImageSource.FromUrl(new NSUrl(imagePath, false));
        //    var ns = new NSDictionary();
        //    CGImage image = photo.CGImage;

        //    var meta = myImageSource.CopyProperties(ns, 0);



        //    CGImageOptions testOpts = new CGImageOptions
        //    {
        //        ShouldCache = false,
        //        ShouldCacheImmediately = false
        //    };

        //    ALAssetsLibrary library = new ALAssetsLibrary();
        //    //myImageSource.CreateImage(0, testOpts)
        //    library.WriteImageToSavedPhotosAlbum(photo.CGImage, meta, (assetUrl, error) => {
        //        //Console.WriteLine ("assetUrl:"+assetUrl);
        //        if (error == null)
        //        {
        //            //                  _photoAlbumImagePath = assetUrl.AbsoluteString;
        //        }
        //        else
        //        {
        //            Console.WriteLine("library.WriteImageToSavedPhotosAlbum error!");
        //        }
        //    });
        //}

        void ToggleFrontBackCamera()
        {
            var devicePosition = captureDeviceInput.Device.Position;
            if (devicePosition == AVCaptureDevicePosition.Front)
            {
                devicePosition = AVCaptureDevicePosition.Back;
            }
            else
            {
                devicePosition = AVCaptureDevicePosition.Front;
            }

            var device = GetCameraForOrientation(devicePosition);
            ConfigureCameraForDevice(device);

            captureSession.BeginConfiguration();
            captureSession.RemoveInput(captureDeviceInput);
            captureDeviceInput = AVCaptureDeviceInput.FromDevice(device);
            captureSession.AddInput(captureDeviceInput);
            captureSession.CommitConfiguration();
        }

        void ToggleFlash()
        {
            var device = captureDeviceInput.Device;

            var error = new NSError();
            if (device.HasFlash)
            {
                if (device.FlashMode == AVCaptureFlashMode.On)
                {
                    device.LockForConfiguration(out error);
                    device.FlashMode = AVCaptureFlashMode.Off;
                    device.UnlockForConfiguration();
                    toggleFlashButton.SetBackgroundImage(UIImage.FromFile("NoFlashButton.png"), UIControlState.Normal);
                }
                else
                {
                    device.LockForConfiguration(out error);
                    device.FlashMode = AVCaptureFlashMode.On;
                    device.UnlockForConfiguration();
                    toggleFlashButton.SetBackgroundImage(UIImage.FromFile("FlashButton.png"), UIControlState.Normal);
                }
            }
        }

        AVCaptureDevice GetCameraForOrientation(AVCaptureDevicePosition orientation)
        {
            var devices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);

            foreach (var device in devices)
            {
                if (device.Position == orientation)
                {
                    return device;
                }
            }
            return null;
        }

        void SetupLiveCameraStream()
        {
            captureSession = new AVCaptureSession();

            var viewLayer = liveCameraStream.Layer;
            var videoPreviewLayer = new AVCaptureVideoPreviewLayer(captureSession)
            {
                Frame = liveCameraStream.Bounds
            };
            liveCameraStream.Layer.AddSublayer(videoPreviewLayer);

            var captureDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);
            ConfigureCameraForDevice(captureDevice);
            captureDeviceInput = AVCaptureDeviceInput.FromDevice(captureDevice);

            var dictionary = new NSMutableDictionary();
            dictionary[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG);
            stillImageOutput = new AVCaptureStillImageOutput()
            {
                OutputSettings = new NSDictionary()
            };

            captureSession.AddOutput(stillImageOutput);
            captureSession.AddInput(captureDeviceInput);
            captureSession.StartRunning();
        }

        void ConfigureCameraForDevice(AVCaptureDevice device)
        {
            var error = new NSError();
            if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
            {
                device.LockForConfiguration(out error);
                device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
                device.UnlockForConfiguration();
            }
            else if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
            {
                device.LockForConfiguration(out error);
                device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
                device.UnlockForConfiguration();
            }
            else if (device.IsWhiteBalanceModeSupported(AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance))
            {
                device.LockForConfiguration(out error);
                device.WhiteBalanceMode = AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance;
                device.UnlockForConfiguration();
            }
        }

        async void AuthorizeCameraUse()
        {
            var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
            if (authorizationStatus != AVAuthorizationStatus.Authorized)
            {
                await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
            }
        }
    }
}

