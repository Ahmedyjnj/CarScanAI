using AutoMapper;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Scan.BLL.Dto_s;
using Scan.BLL.Dto_s.AiDto;
using Scan.BLL.Dto_s.AnalysisDto_s;
using Scan.BLL.Services.Ai;
using Scan.BLL.Services.Attachments;
using Scan.BLL.Services.DamageServices;
using Scan.BLL.Services.DetectionServices;
using Scan.BLL.Services.RepairCenterServices;
using Scan.DAL.Models.Car;
using Scan.DAL.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Scan.BLL.Services.AnalysisServices
{
    public class AnalysisService(IMapper mapper, IDetectionService detectionService, ICarDamageAiService fakeCarDamageAiService,
        IDamageService damageService, IRepairCenterService repairCenterService, IFileService fileService, IUnitOfWork unitOfWork, IUrlService urlService) : IAnalysisService
    {
        public async Task<AnalysisResponseDetailsDto>
     AnalyzeAsync(
         Guid carId,
         IFormFile image,
         string userId)
        {
            if (image == null || image.Length == 0)
            {
                throw new BadRequestException(

                "Image is required."
                    );
            }

            var car =
                await unitOfWork
                    .GetRepository<Car>()
                    .GetById(carId);

            if (car == null)
            {
                throw new NotFoundException(
                    "Car not found.");
            }

            if (car.UserId != userId)
            {
                throw new UnauthorizedException(
                    "You are not allowed to access this car.");
            }

            var imagePath =
                await fileService.UploadAsync(image);

            var detection =
                await detectionService
                    .CreateDetectionAsync(
                        carId,
                        imagePath);


            var aiResult =
             await fakeCarDamageAiService
            .GenerateAiDamageImageAsync(
            new AiRequestDto
            {
                ImagePath = imagePath,

            });

            var severity = aiResult.prediction.ToLower();

            var random = new Random();

            decimal estimatedCost = severity switch
            {
                "minor" => random.Next(1000, 3000),

                "moderate" => random.Next(4000, 10000),

                "severe" => random.Next(12000, 30000),

                _ => 3000
            };

            string locationLabel = "";

            try
            {
                var damage = new DamageDetail
                {
                    DetectionId =
            detection.DetectionId,

                    DamagedAreaLocation =
            locationLabel,

                    SeverityLevel =
            aiResult.prediction,

                    EstimatedCost =CalculateCost(aiResult.prediction),
                                            

                    ConfidenceScore = (decimal)
            aiResult.Confidence,

                    DamageType =
            "Scratch"
                };

                await unitOfWork
                    .GetRepository<DamageDetail>()
                    .AddAsync(damage);

                await unitOfWork.SaveAsync();

                var centers =
                    await repairCenterService
                        .GetCentersByBrandAsync(
                            detection.DetectionId,
                            car.Brand);

                var centersDto =
                    mapper.Map<
                        List<RepairCenterRecommendationDto>>
                        (centers);

                return new AnalysisResponseDetailsDto
                {
                    AnalysisId =
                        detection.DetectionId,

                    UserId =
                        userId,

                    CreatedAtUtc =
                        detection.DetectionDate,

                    TotalEstimatedCost =
                        damage.EstimatedCost,

                    Finding =
                        new AnalysisFindingDto
                        {
                            LocationLabel =
                                damage.DamagedAreaLocation,

                            Severity =
                                damage.SeverityLevel,

                            EstimatedCost =
                                damage.EstimatedCost,

                            Confidence =
                                damage.ConfidenceScore,
                            ImagePath = urlService.GetImageUrl(detection.ImagePath)


                        },

                    Recommendations =
                        centersDto
                };
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error saving damage details: {ex.Message}");
                // Optionally, you can rethrow the exception or handle it as needed
                throw new Exception(
        $"An error occurred while saving damage details: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<IEnumerable<AnalysisResponseDetailsDto>> GetAll(string userId)
        {
            var detections = await unitOfWork
             .GetRepository<Detection>()
             .GetAll()
             .Where(d => d.UserId == userId)
             .OrderByDescending(d => d.DetectionDate)
             .ToListAsync();

            var result =
                new List<AnalysisResponseDetailsDto>();

            foreach (var detection in detections)
            {
                var damage =
                    await unitOfWork
                        .GetRepository<DamageDetail>()
                        .GetAll()
                        .FirstOrDefaultAsync(d =>
                            d.DetectionId ==
                            detection.DetectionId);

                var centers =
                    await repairCenterService
                        .GetCentersByDetectionIdAsync(
                            detection.DetectionId);// Get centers by detection ID

                var centersDto =
                    mapper.Map<
                        List<RepairCenterRecommendationDto>>
                        (centers);

                result.Add(
                    new AnalysisResponseDetailsDto
                    {
                        AnalysisId =
                            detection.DetectionId,

                        UserId =
                            detection.UserId,

                        CreatedAtUtc =
                            detection.DetectionDate,

                        TotalEstimatedCost =
                            detection.TotalCost,

                        Finding =
                            damage == null
                            ? null
                            : new AnalysisFindingDto
                            {
                                LocationLabel =
                                    damage.DamagedAreaLocation,

                                Severity =
                                    damage.SeverityLevel,

                                EstimatedCost =
                                    damage.EstimatedCost,

                                Confidence =
                                    damage.ConfidenceScore,

                                ImagePath = urlService.GetImageUrl(detection.ImagePath)
                            },

                        Recommendations =
                            centersDto
                    });
            }

            return result;
        }
        private decimal CalculateCost(string severity)
        {
            severity = severity.Trim().ToLower();

            var random = new Random();

            return severity switch
            {
                "minor" => random.Next(1000, 3000),

                "moderate" => random.Next(4000, 10000),

                "severe" => random.Next(12000, 30000),

                _ => random.Next(1000, 5000)
            };
        }
    }
    }

