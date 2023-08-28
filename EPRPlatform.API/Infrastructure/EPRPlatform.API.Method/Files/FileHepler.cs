
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;
using System.Text;

namespace EPRPlatform.API.Method
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public static class FileHepler
    {
        /// <summary>
        /// 文件夹是否存在
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns></returns>
        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns></returns>
        public static void DirectoryCreate(string path)
        {
            Directory.CreateDirectory(path);
        }
        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file">文件流</param>
        /// <param name="path">保存地址</param>
        public static void SaveFile(Stream file, string path)
        {
            using FileStream fs = new(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            file.CopyTo(fs);
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="file">文件流</param>
        /// <param name="path">保存地址</param>
        /// <param name="IsZoom">是否缩放</param>
        /// <param name="ZoomWidth">宽度</param>
        /// <param name="ZoomHeight">高度</param>
        /// <param name="IsZip">是否压缩</param>
        /// <param name="Quality">压缩质量 1-100</param>
        public static void SavePicture(Stream file, string path, bool IsZoom, int ZoomWidth, int ZoomHeight, bool IsZip, int Quality)
        {
            using Image image = Image.Load(file);
            #region 等比例缩放图片
            if (IsZoom)
            {
                if (image.Width > ZoomWidth)
                {
                    var a = MakeSize(image.Width, image.Height, ZoomWidth, 0);
                    image.Mutate(x => x.Resize(a.Width, a.Height));
                }
                if (image.Height > ZoomHeight)
                {
                    var a = MakeSize(image.Width, image.Height, 0, ZoomHeight);
                    image.Mutate(x => x.Resize(a.Width, a.Height));
                }
            }
            #endregion

            if (IsZip)
            {
                IImageEncoder imageEncoderForJpeg = new JpegEncoder()
                {
                    Quality = Quality,
                };
                image.Save(path, imageEncoderForJpeg);
            }
            else
                image.Save(path);
        }


        /// <summary>
        /// 计算等比例缩放的新宽度和新高度
        /// </summary>
        /// <param name="oldWidth">旧宽度</param>
        /// <param name="oldHeight">旧高度</param>
        /// <param name="toWidth">新宽度（0为不限）</param>
        /// <param name="toHeight">新高度（0为不限）</param>
        /// <returns></returns>
        private static WidthAndHeight MakeSize(int oldWidth, int oldHeight, int toWidth = 0, int toHeight = 0)
        {
            WidthAndHeight res = new()
            {
                Height = oldHeight,
                Width = oldWidth
            };

            if (toWidth == 0 && toHeight > 0)   //不限宽度
            {
                if (toHeight < oldHeight)
                {
                    res.Width = (int)(toHeight / (decimal)oldHeight * oldWidth);
                    res.Height = toHeight;
                }
            }
            else if (toWidth > 0 && toHeight == 0)  //不限高度
            {
                if (toWidth < oldWidth)
                {
                    res.Width = toWidth;
                    res.Height = (int)(toWidth / (decimal)oldWidth * oldHeight);
                }
            }
            else if (toWidth > 0 && toHeight > 0)
            {
                if (toWidth * oldHeight > oldWidth * toHeight && toHeight < oldHeight)  //按指定高度缩小
                {
                    res.Width = (int)(toHeight / (decimal)oldHeight * oldWidth);
                    res.Height = toHeight;
                }
                else if (toWidth < oldWidth)  //按指定宽度缩小
                {
                    res.Width = toWidth;
                    res.Height = (int)(toWidth / (decimal)oldWidth * oldHeight);
                }
            }

            return res;
        }

        /// <summary>
        /// 视频转m3u8
        /// </summary>
        /// <param name="ffmpegPath"></param>
        /// <param name="filePath"></param>
        /// <param name="savePath"></param>
        public static void VideoToM3u8(string ffmpegPath, string filePath,string savePath)
        {
            try
            {
                string ffmpegstr = $" -i {filePath} -codec copy -bsf:v h264_mp4toannexb -hls_list_size 0 -hls_time 10 {savePath}";

                using (Process p = new())
                {
                    p.StartInfo.FileName = ffmpegPath;//要执行的程序名称  
                    p.StartInfo.Arguments = ffmpegstr;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardInput = false;//可能接受来自调用程序的输入信息  
                    p.StartInfo.RedirectStandardOutput = false;//由调用程序获取输出信息   
                    p.StartInfo.RedirectStandardError = false;//重定向标准错误输出
                    p.StartInfo.CreateNoWindow = false;//不显示程序窗口   
                    p.Start();//启动程序   
                    p.WaitForExit();//等待程序执行完退出进程
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally
            {
                //File.Delete(filePath);
            }
            
        }

    }
    /// <summary>
    /// 宽高
    /// </summary>
    public class WidthAndHeight
    {
        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; }
    }
}
