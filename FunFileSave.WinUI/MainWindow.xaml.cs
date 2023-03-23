using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FunFileSave.WinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            // 80 kb .png file
            var og = GetResourceFileContent("Icons.dotnet-bot.png");
            var filePicker = new FileSavePicker();

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            filePicker.FileTypeChoices.Add("PNG", new List<string>() { ".png" });

            // open windows file picker.
            var file = await filePicker.PickSaveFileAsync();
            
            // open the file you picked.
            using var fileStream = await file.OpenStreamForWriteAsync();

            og.Seek(0, SeekOrigin.Begin);

            // write the contents of the embedded png to the file.
            // this just takes the bits from 'og' and copies them on top of the
            // filestream, it doesn't remove anything.

            await og.CopyToAsync(fileStream);

            // closes the file.
            fileStream.Dispose();

            // check the resulting png, you'll see the embedded image in the png,
            // but the file size will be the same.
            // this is expected for the code as written, but it's very easy to foot gun yourself to do.
        }

        private async void myButton_Click2(object sender, RoutedEventArgs e)
        {
            // 80 kb .png file
            var og = GetResourceFileContent("Icons.dotnet-bot.png");
            var filePicker = new FileSavePicker();

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            filePicker.FileTypeChoices.Add("PNG", new List<string>() { ".png" });

            // open windows file picker.
            var file = await filePicker.PickSaveFileAsync();

            // Use SafeFileHandle instead...

            var handle = file.CreateSafeFileHandle(FileAccess.ReadWrite);
            var stream = new FileStream(handle, FileAccess.Write);

            og.Seek(0, SeekOrigin.Begin);

            // write the contents of the embedded png to the file.
            // this just takes the bits from 'og' and copies them on top of the
            // filestream, it doesn't remove anything.

            await og.CopyToAsync(stream);
            await stream.FlushAsync();

            stream.Dispose();

            // check the resulting png, you'll see the embedded image in the png,
            // but the file size will be the same.
        }

        private async void myButton_Click3(object sender, RoutedEventArgs e)
        {
            // 80 kb .png file
            var og = GetResourceFileContent("Icons.dotnet-bot.png");
            var filePicker = new FileSavePicker();

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            filePicker.FileTypeChoices.Add("PNG", new List<string>() { ".png" });

            // open windows file picker.
            var file = await filePicker.PickSaveFileAsync();

            // open the file you picked.
            using var fileStream = await file.OpenStreamForWriteAsync();

            // Copy the original png to a byte array, and write that to the file.

            using var memoryStream = new MemoryStream();
            og.CopyTo(memoryStream);
            var byteArray = memoryStream.ToArray();

            // Writes the contents over the stream.
            // Again though, this doesn't clean the bytes after the initial count.
            // If you don't clear it yourself, they will remain.
            fileStream.Write(byteArray, 0, byteArray.Count());

            // closes the file.
            fileStream.Dispose();
        }

        private async void myButton_Click4(object sender, RoutedEventArgs e)
        {
            // 80 kb .png file
            var og = GetResourceFileContent("Icons.dotnet-bot.png");
            var filePicker = new FileSavePicker();

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            filePicker.FileTypeChoices.Add("PNG", new List<string>() { ".png" });

            // open windows file picker.
            var file = await filePicker.PickSaveFileAsync();

            // open the file you picked.
            using var fileStream = await file.OpenStreamForWriteAsync();

            og.Seek(0, SeekOrigin.Begin);

            // Set the file stream length to 0 resets the existing file and clears it.
            fileStream.SetLength(0);

            // write the contents of the embedded png to the file.
            // this just takes the bits from 'og' and copies them on top of the
            // filestream, it doesn't remove anything.

            await og.CopyToAsync(fileStream);

            // closes the file.
            fileStream.Dispose();

            // This should produce a valid png with none of the contents left.
        }



        /// <summary>
        /// Get Resource File Content via FileName.
        /// </summary>
        /// <param name="fileName">Filename.</param>
        /// <returns>Stream.</returns>
        public static Stream? GetResourceFileContent(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "FunFileSave.WinUI." + fileName;
            if (assembly is null)
            {
                return null;
            }

            return assembly.GetManifestResourceStream(resourceName);
        }

        private async void shaggarTest_Click(object sender, RoutedEventArgs e)
        {
            // 80 kb .png file
            var og = GetResourceFileContent("Icons.dotnet-bot.png");
            var filePicker = new FileSavePicker();

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            filePicker.FileTypeChoices.Add("PNG", new List<string>() { ".png" });

            // open windows file picker.
            var file = await filePicker.PickSaveFileAsync();

            // Create a byte array of the original file.
            using var memoryStream = new MemoryStream();
            og.CopyTo(memoryStream);
            var byteArray = memoryStream.ToArray();

            // Write it out using the StorageFile and byte array.
            await FileIO.WriteBytesAsync(file, byteArray);

            // This should result in the correct 80kb file.
        }

        private async void shaggarTest_Click2(object sender, RoutedEventArgs e)
        {
            // 80 kb .png file
            var og = GetResourceFileContent("Icons.dotnet-bot.png");
            var filePicker = new FileSavePicker();

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            filePicker.FileTypeChoices.Add("PNG", new List<string>() { ".png" });

            // open windows file picker.
            var file = await filePicker.PickSaveFileAsync();

            using var memoryStream = new MemoryStream();
            og.CopyTo(memoryStream);

            // Write it out using the StorageFile and IBuffer.
            await FileIO.WriteBufferAsync(file, memoryStream.GetWindowsRuntimeBuffer());

            // This should result in the correct 80kb file.
        }
    }
}
