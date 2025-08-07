using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MicroServices.API
{
    public class RemoveAiEndpointsFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // 需要移除的路径列表
            var pathsToRemove = new List<string>
        {
            "/api/Ai/Ask",
            "/api/Ai/AskStream",
            "/api/Ai/AskCallback",
            "/api/Ai/Status",
            "/api/UploadFile/UploadTempFile"
        };

            // 遍历并移除匹配的路径
            foreach (var path in pathsToRemove)
            {
                swaggerDoc.Paths.Remove(path);
            }

            // 也可以通过 Tag 来移除所有带特定 Tag 的操作 (如果AI接口都有统一的Tag，例如"Ai")
            // 请注意：移除整个路径会移除该路径下的所有HTTP方法（GET, POST等）

            // 示例：如果想移除所有带有 "Ai" Tag 的操作（更通用）
            // var aiPaths = swaggerDoc.Paths
            //     .Where(pair => pair.Value.Operations.Any(opPair => opPair.Value.Tags.Any(tag => tag.Name == "Ai")))
            //     .Select(pair => pair.Key)
            //     .ToList();
            // foreach (var path in aiPaths)
            // {
            //     swaggerDoc.Paths.Remove(path);
            // }
        }
    }
}
