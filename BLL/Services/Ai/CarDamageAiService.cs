using Microsoft.Extensions.Configuration;
using Scan.BLL.Dto_s.AiDto;
using Scan.BLL.Dto_s.AiResponseDto;
using Scan.BLL.Services.Ai;
using Scan.BLL.Services.Attachments;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Scan.BLL.Services.AiServices
{
    public class FakeCarDamageAiService(HttpClient _httpClient, IConfiguration _config) : ICarDamageAiService

    {
        


        public async Task<AiResponseDto> GenerateAiDamageImageAsync(AiRequestDto aiRequestDto)
        {

          
            using var form = new MultipartFormDataContent();

            //  var fullPath = urlService.GetImageUrl(aiRequestDto.ImagePath);

            var fullPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                aiRequestDto.ImagePath.TrimStart('/')
            );

            if (!File.Exists(fullPath))
                throw new Exception($"Image not found: {fullPath}");

            var imageBytes = await File.ReadAllBytesAsync(fullPath);

            var imageContent = new ByteArrayContent(imageBytes);

            var ext = Path.GetExtension(fullPath).ToLower();

            var mime = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };

            imageContent.Headers.ContentType = new MediaTypeHeaderValue(mime);

            form.Add(imageContent, "image", Path.GetFileName(fullPath));

            var smtpSection = _config.GetSection("AiSettings");

            var response = await _httpClient.PostAsync(
                 smtpSection["PredictUrl"] + "/predict"
                , form);
            //,


            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"AI failed: {json}");

            return JsonSerializer.Deserialize<AiResponseDto>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }
    }
}