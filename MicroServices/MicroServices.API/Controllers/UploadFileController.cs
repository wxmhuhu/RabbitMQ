using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace MicroServices.API.Controllers
{
    /// <summary>
    /// 文件上传
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly ISqlSugarClient db;

        public UploadFileController(IWebHostEnvironment env, ISqlSugarClient db)
        {
            this.env = env;
            this.db = db;
        }
        /// <summary>
        /// 上传临时文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadTempFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("未选择文件");

            var uploadsFolder = Path.Combine(env.WebRootPath, "temp");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { message = "上传成功", path = "/temp/" + uniqueFileName });
        }
    }
}
