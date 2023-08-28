using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using EPRPlatform.API.File.Model;
using EPRPlatform.API.Interfaces;
using System.ComponentModel.DataAnnotations;
using System;
using System.Net;
using EPRPlatform.API.Method;
using Microsoft.AspNetCore.Authorization;
using EPRPlatform.API.Models;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Dto;

namespace EPRPlatform.API.File.Controllers
{
    /// <summary>
    /// 上传接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadController : BaseController
    {
        private readonly UploadSetting _config;
        private readonly IErrorRepository _iError;
        private readonly IUploadRepository _iUpload;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iError">错误数据接口</param>
        /// <param name="config">上传配置</param>
        /// <param name="iUpload">上传数据接口</param>
        public UploadController(IErrorRepository iError, IOptions<UploadSetting> config, IUploadRepository iUpload)
        {
            try
            {
                _iError = iError;
                _config = config.Value;
                _iUpload = iUpload;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="setting">上传模块，空为默认模块</param>
        /// <param name="IsZoom">是否缩放</param>
        /// <param name="ZoomWidth">缩放宽度</param>
        /// <param name="ZoomHeight">缩放高度</param>
        /// <param name="IsZip">是否压缩</param>
        /// <param name="Quality">压缩质量 1-100</param>
        /// <param name="file">上传文件</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<OutPutModel<object>> UploadFileAsync([Display(Name = "文件")] string setting,
            [Display(Name = "是否缩放")] bool? IsZoom,
            [Display(Name = "缩放宽度")] int? ZoomWidth,
            [Display(Name = "缩放高度")] int? ZoomHeight,
            [Display(Name = "是否压缩")] bool? IsZip,
            [Display(Name = "压缩质量")] int? Quality,
            [Display(Name = "文件")]
        [Required(ErrorMessage = "请选择{0}")]
        IFormFile file)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(setting))
                    setting = "default";
                UploadConfig config = _config.Models.Find(w => w.Model == setting);
                #region 信息验证
                if (config == null)
                    return OutPutMethod<object>.Failure("上传模块有误！", null, (int)HttpStatusCode.NotFound);
                var fileExt = Path.GetExtension(file.FileName);
                if (fileExt == null)
                    return OutPutMethod<object>.Failure("上传文件后缀有误！", null, (int)HttpStatusCode.BadRequest);
                if (!config.ExtName.Contains(fileExt))
                    return OutPutMethod<object>.Failure("不能上传该类型文件！", null, (int)HttpStatusCode.BadRequest);
                long length = file.Length;
                if (length > 1024 * 1024 * config.MaxSize)
                    return OutPutMethod<object>.Failure($"上传文件不能超过{config.MaxSize}M！", null, (int)HttpStatusCode.BadRequest);
                #endregion


                #region 路径处理
                string datePath = DateTime.Now.ToString("yyyyMMdd");
                string guid = Guid.NewGuid().ToString();
                string savePath = _config.FileProvider + config.Path + "\\" + datePath + "\\";
                string fileUrl = _config.RequestPath + config.Url + "/" + datePath + "/";
                string fileName = guid + fileExt;
                if (!FileHepler.DirectoryExists(savePath))
                    FileHepler.DirectoryCreate(savePath);
                while (FileHepler.FileExists(savePath + "\\" + guid))
                    fileName = Guid.NewGuid() + fileExt;
                savePath += fileName;
                fileUrl += fileName;
                #endregion

                #region 文件处理
                Stream stream = file.OpenReadStream();
                string[] picext = new string[] { ".jpg", ".jpeg", ".png" };
                if (!picext.Contains(fileExt))
                    FileHepler.SaveFile(stream, savePath);
                else
                    FileHepler.SavePicture(stream, savePath, IsZoom ?? config.Zoom, ZoomWidth ?? config.Width, ZoomHeight ?? config.Height, IsZip ?? config.Zip, Quality ?? config.Quality);
                #endregion

                if (!FileHepler.FileExists(savePath))
                    return OutPutMethod<object>.Failure("上传失败！", null, (int)HttpStatusCode.BadRequest);
                #region 数据处理
                UploadFile data = new()
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName,
                    Path = savePath,
                    Url = fileUrl,
                    FileSize = file.Length,
                    UserId = UserInformation.Id,
                    UploadTime = DateTime.Now
                };
                #endregion
                if (!await _iUpload.AddAsync(data))
                    return OutPutMethod<object>.Failure();
                else
                    return OutPutMethod<object>.Success(data.Url);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="file">上传文件</param>
        /// <returns></returns>
        [HttpPost("Video")]
        public async Task<OutPutModel<object>> UploadVideoAsync(
            [Display(Name = "文件")]
        [Required(ErrorMessage = "请选择{0}")]
        IFormFile file)
        {
            try
            {
                UploadConfig config = _config.Models.Find(w => w.Model == "m3u8");
                #region 信息验证
                if (config == null)
                    return OutPutMethod<object>.Failure("上传模块有误！", null, (int)HttpStatusCode.NotFound);
                var fileExt = Path.GetExtension(file.FileName);
                if (fileExt == null)
                    return OutPutMethod<object>.Failure("上传文件后缀有误！", null, (int)HttpStatusCode.BadRequest);
                if (!config.ExtName.Contains(fileExt))
                    return OutPutMethod<object>.Failure("不能上传该类型文件！", null, (int)HttpStatusCode.BadRequest);
                long length = file.Length;
                if (length > 1024 * 1024 * config.MaxSize)
                    return OutPutMethod<object>.Failure($"上传文件不能超过{config.MaxSize}M！", null, (int)HttpStatusCode.BadRequest);
                #endregion


                #region 路径处理
                string datePath = DateTime.Now.ToString("yyyyMMdd");
                string guid = Guid.NewGuid().ToString();
                string savePath = _config.FileProvider + config.Path + "\\" + datePath + "\\";
                string fileUrl = _config.RequestPath + config.Url + "/" + datePath + "/";
                string fileName = guid;
                while (FileHepler.FileExists(fileName))
                {
                    fileName = Guid.NewGuid().ToString();
                }
                if (!FileHepler.DirectoryExists(savePath+ fileName))
                    FileHepler.DirectoryCreate(savePath + fileName);
                
                string m3u8Parh = savePath + fileName + "\\index.m3u8";
                fileUrl += fileName + "/index.m3u8";
                savePath += fileName + "\\1" + fileExt;
                #endregion

                #region 文件处理
                Stream stream = file.OpenReadStream();
                FileHepler.SaveFile(stream, savePath);
                if (FileHepler.FileExists(savePath))
                {
                    FileHepler.VideoToM3u8(_config.FFmpegPath, savePath, m3u8Parh);
                }
                #endregion

                if (!FileHepler.FileExists(m3u8Parh))
                    return OutPutMethod<object>.Failure("上传失败！", null, (int)HttpStatusCode.BadRequest);

                #region 数据处理
                UploadFile data = new()
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName,
                    Path = m3u8Parh,
                    Url = fileUrl,
                    FileSize = file.Length,
                    UserId = UserInformation.Id,
                    UploadTime = DateTime.Now
                };
                #endregion
                if (!await _iUpload.AddAsync(data))
                    return OutPutMethod<object>.Failure();
                else
                    return OutPutMethod<object>.Success(data.Url);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}
