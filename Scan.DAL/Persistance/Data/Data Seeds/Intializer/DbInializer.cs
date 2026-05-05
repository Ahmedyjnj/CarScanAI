using Domain.Contracts;
using IKEa.DAL.Persinstance.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scan.DAL.Models.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance.Data
{
    public class DbInializer(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : IDbInializer
    {
        public async Task IdentityInializeAsync()
        {

            try
            {

                if (!userManager.Users.Any())
                {
                    var User1 = new User()
                    {
                        Email = "ahmedkok88h@gmail.com",
                        UserName = "Ahmed",
                        Name = "Ahmed elsagheer",
                        PhoneNumber = "01112643021",
                        ProfileImage = "default.png"
                    };



                    // await userManager.CreateAsync(User1, "mompoplolkok98");

                    var result = await userManager.CreateAsync(User1, "mompoplolkok98");

                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));
                    }


                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }




        }






        public async Task InializeAsync()
        {
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }

            try
            {



                var createdUser = await userManager.FindByEmailAsync("ahmedkok88h@gmail.com");

                if (createdUser != null)
                {
                    if (!context.Set<Car>().Any())
                    {
                        var cars = new List<Car>
                    {
                        new Car
                        {
                            CarId = Guid.NewGuid(),
                            PlateNumber = "ABC123",
                            Brand = "Toyota",
                            Model = "Corolla",
                            Year = 2022,
                            Color = "White",
                            UserId = createdUser.Id
                        },
                        new Car
                        {
                            CarId = Guid.NewGuid(),
                            PlateNumber = "XYZ789",
                            Brand = "BMW",
                            Model = "X5",
                            Year = 2023,
                            Color = "Black",
                            UserId = createdUser.Id
                        }
                    };

                        await context.Set<Car>().AddRangeAsync(cars);
                        await context.SaveChangesAsync();
                    }
                    if (!context.Set<RepairCenter>().Any())
                    {
                        var centers = new List<RepairCenter>
                     {

            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Toyota Egypt Authorized Service - Abbas El Akkad",
                Phone = "+20 10 1100 0001",
                Address = "15 Makram Ebeid St, Zone 6",
                Location = "Nasr City",
                SupportedBrand = "Toyota",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "El-Saba Automotive Toyota Service",
                Phone = "+20 11 1100 0002",
                Address = "Corniche El Nile, Next to Maadi Grand Mall",
                Location = "Maadi",
                SupportedBrand = "Toyota",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Auto Misr Toyota Center",
                Phone = "+20 12 1100 0003",
                Address = "120 El-Horreya Road, Sidi Gaber",
                Location = "Alexandria",
                SupportedBrand = "Toyota",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "El-Salam Premium Auto Care",
                Phone = "+20 15 1100 0004",
                Address = "104 Gesr El Suez St, Industrial Zone",
                Location = "El-Salam City",
                SupportedBrand = "Toyota, Kia",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "GB Auto Hyundai Service",
                Phone = "+20 10 1200 0005",
                Address = "Km 28 Cairo-Alex Desert Road, Abu Rawash",
                Location = "Giza",
                SupportedBrand = "Hyundai",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "El-Tarek Auto Hyundai",
                Phone = "+20 11 1200 0006",
                Address = "Mostafa El-Nahas St, Block 12",
                Location = "Nasr City",
                SupportedBrand = "Hyundai",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Delta Motors Hyundai Mansoura",
                Phone = "+20 50 1200 0007",
                Address = "Gehan St, Hay El Gamaa",
                Location = "Mansoura",
                SupportedBrand = "Hyundai",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Heliopolis Korean Auto Service",
                Phone = "+20 12 1200 0008",
                Address = "30 El-Hegaz St",
                Location = "Heliopolis",
                SupportedBrand = "Hyundai, Kia",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "EIT Kia Authorized Center",
                Phone = "+20 10 1300 0009",
                Address = "Road 250, Degla",
                Location = "Maadi",
                SupportedBrand = "Kia",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Kia Tanta Service Hub",
                Phone = "+20 40 1300 0010",
                Address = "El-Geish St, Tanta Center",
                Location = "Tanta",
                SupportedBrand = "Kia",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Auto Market Kia Garage",
                Phone = "+20 11 1300 0011",
                Address = "45 El-Batal Ahmed Abdel Aziz",
                Location = "Dokki",
                SupportedBrand = "Kia",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Sheikh Zayed Kia Clinic",
                Phone = "+20 15 1300 0012",
                Address = "Beverly Hills Commercial Strip",
                Location = "Sheikh Zayed",
                SupportedBrand = "Kia",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Nissan Auto Egypt Main Center",
                Phone = "+20 10 1400 0013",
                Address = "South 90th St, 5th Settlement",
                Location = "New Cairo",
                SupportedBrand = "Nissan",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "El-Saba Nissan Service",
                Phone = "+20 12 1400 0014",
                Address = "Pyramids Road, El-Talbia",
                Location = "Giza",
                SupportedBrand = "Nissan",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Modern Auto Repair Nissan",
                Phone = "+20 11 1400 0015",
                Address = "Smouha Sporting Club Gate 2",
                Location = "Alexandria",
                SupportedBrand = "Nissan",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Nasr City Alliance Auto",
                Phone = "+20 15 1400 0016",
                Address = "Hassan El Maamoun St",
                Location = "Nasr City",
                SupportedBrand = "Nissan, Renault",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Mansour Automotive Chevrolet",
                Phone = "+20 10 1500 0017",
                Address = "Sheraton Heliopolis Square",
                Location = "Heliopolis",
                SupportedBrand = "Chevrolet",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Chevrolet Delta Garage",
                Phone = "+20 50 1500 0018",
                Address = "Suez Canal St, Talkha",
                Location = "Mansoura",
                SupportedBrand = "Chevrolet",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Ring Road Chevy Service",
                Phone = "+20 11 1500 0019",
                Address = "Zahraa El Maadi Main Road",
                Location = "Maadi",
                SupportedBrand = "Chevrolet",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Alex Multi-Brand Services",
                Phone = "+20 12 1500 0020",
                Address = "Roushdy Tram Station Area",
                Location = "Alexandria",
                SupportedBrand = "Chevrolet, MG",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Global Auto BMW Center",
                Phone = "+20 10 1600 0021",
                Address = "Katameya Ring Road Exit",
                Location = "New Cairo",
                SupportedBrand = "BMW",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "BMW Premium Service Mohandeseen",
                Phone = "+20 11 1600 0022",
                Address = "Geziret El Arab St",
                Location = "Dokki",
                SupportedBrand = "BMW",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Zayed German Auto Works",
                Phone = "+20 15 1600 0023",
                Address = "Arkan Plaza Rear Entrance",
                Location = "Sheikh Zayed",
                SupportedBrand = "BMW",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Bavarian Alexandria Garage",
                Phone = "+20 12 1600 0024",
                Address = "El-Max Industrial Zone",
                Location = "Alexandria",
                SupportedBrand = "BMW",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "NATCO Mercedes-Benz Heliopolis",
                Phone = "+20 10 1700 0025",
                Address = "El-Orouba Road",
                Location = "Heliopolis",
                SupportedBrand = "Mercedes-Benz",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "German Star Auto Service",
                Phone = "+20 11 1700 0026",
                Address = "Mohi El Din Abouzouid",
                Location = "Giza",
                SupportedBrand = "Mercedes-Benz",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Star Egypt Auto Tagamoa",
                Phone = "+20 15 1700 0027",
                Address = "North 90th St, Choueifat Area",
                Location = "New Cairo",
                SupportedBrand = "Mercedes-Benz",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "El-Gargour Premium Motors",
                Phone = "+20 12 1700 0028",
                Address = "Kafr Abdou St",
                Location = "Alexandria",
                SupportedBrand = "Mercedes-Benz",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Audi Center Cairo",
                Phone = "+20 10 1800 0029",
                Address = "First Settlement Commercial Hub",
                Location = "New Cairo",
                SupportedBrand = "Audi",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Ezz Elarab Audi Station",
                Phone = "+20 11 1800 0030",
                Address = "Nile Corniche, Agouza",
                Location = "Dokki",
                SupportedBrand = "Audi",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "German Motors Group Zayed",
                Phone = "+20 15 1800 0031",
                Address = "Dandy Mega Mall Service Area",
                Location = "Sheikh Zayed",
                SupportedBrand = "Audi, Volkswagen",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Audi Auto Group Alexandria",
                Phone = "+20 12 1800 0032",
                Address = "Stanly Bay Plaza",
                Location = "Alexandria",
                SupportedBrand = "Audi",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "VW Main Service Hub",
                Phone = "+20 10 1900 0033",
                Address = "El-Tayaran St",
                Location = "Nasr City",
                SupportedBrand = "Volkswagen",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Nile Engineering VW Center",
                Phone = "+20 11 1900 0034",
                Address = "Laselky Road",
                Location = "Maadi",
                SupportedBrand = "Volkswagen",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Alex Elite German Service",
                Phone = "+20 12 1900 0035",
                Address = "Glim, Abd El-Salam Aref St",
                Location = "Alexandria",
                SupportedBrand = "Volkswagen, Skoda",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Renault EIM Dokki",
                Phone = "+20 10 2000 0036",
                Address = "Mossadek St",
                Location = "Dokki",
                SupportedBrand = "Renault",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "French Auto Care Maadi",
                Phone = "+20 11 2000 0037",
                Address = "Zahraa El Maadi, Plot 14",
                Location = "Maadi",
                SupportedBrand = "Renault",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Renault Desert Road Service",
                Phone = "+20 15 2000 0038",
                Address = "Km 22 Cairo-Alex Desert Rd",
                Location = "Sheikh Zayed",
                SupportedBrand = "Renault",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Alex Renault Clinic",
                Phone = "+20 12 2000 0039",
                Address = "El-Mandara Qebli",
                Location = "Alexandria",
                SupportedBrand = "Renault",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Peugeot Auto Lounge",
                Phone = "+20 10 2100 0040",
                Address = "Youssef Abbas St",
                Location = "Nasr City",
                SupportedBrand = "Peugeot",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "El-Kasrawy Peugeot Heliopolis",
                Phone = "+20 11 2100 0041",
                Address = "Salah Salem Road",
                Location = "Heliopolis",
                SupportedBrand = "Peugeot",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Tanta French Motors",
                Phone = "+20 40 2100 0042",
                Address = "El-Nahda St",
                Location = "Tanta",
                SupportedBrand = "Peugeot",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Peugeot New Cairo Service",
                Phone = "+20 15 2100 0043",
                Address = "Industrial Zone 1, 3rd Settlement",
                Location = "New Cairo",
                SupportedBrand = "Peugeot",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "MG Mansour Service Hub",
                Phone = "+20 10 2200 0044",
                Address = "Makram Ebeid Extension",
                Location = "Nasr City",
                SupportedBrand = "MG",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Smart Auto MG Zayed",
                Phone = "+20 11 2200 0045",
                Address = "Dahshour Link Road",
                Location = "Sheikh Zayed",
                SupportedBrand = "MG",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "MG Premium Service Maadi",
                Phone = "+20 15 2200 0046",
                Address = "Autostrad Road, El-Basatin",
                Location = "Maadi",
                SupportedBrand = "MG",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "El-Saba Asian Auto Alex",
                Phone = "+20 12 2200 0047",
                Address = "Montaza Palace Outer Road",
                Location = "Alexandria",
                SupportedBrand = "MG, Chery",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "GB Auto Chery Katameya",
                Phone = "+20 10 2300 0048",
                Address = "Ring Road Exit 14",
                Location = "New Cairo",
                SupportedBrand = "Chery",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Chery Express Center Giza",
                Phone = "+20 11 2300 0049",
                Address = "Faisal Main St",
                Location = "Giza",
                SupportedBrand = "Chery",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Chinese Auto Masters Mansoura",
                Phone = "+20 50 2300 0050",
                Address = "Bahr St, Mit Ghamr Direction",
                Location = "Mansoura",
                SupportedBrand = "Chery",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Chery City Service Heliopolis",
                Phone = "+20 12 2300 0051",
                Address = "Nabil El Wakkad St",
                Location = "Heliopolis",
                SupportedBrand = "Chery",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Kian Egypt Skoda Katameya",
                Phone = "+20 10 2400 0052",
                Address = "Katameya Industrial Area",
                Location = "New Cairo",
                SupportedBrand = "Skoda",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Artoc Auto Dokki",
                Phone = "+20 11 2400 0053",
                Address = "Tahrir St",
                Location = "Dokki",
                SupportedBrand = "Skoda",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Skoda Central Service Nasr City",
                Phone = "+20 15 2400 0054",
                Address = "Ahmed Fakhry St",
                Location = "Nasr City",
                SupportedBrand = "Skoda",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Alex Skoda Garage",
                Phone = "+20 12 2400 0055",
                Address = "Miami, Gamal Abdel Nasser St",
                Location = "Alexandria",
                SupportedBrand = "Skoda",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Fiat Nile Center",
                Phone = "+20 10 2500 0056",
                Address = "Port Said St, Maadi Hadaeq",
                Location = "Maadi",
                SupportedBrand = "Fiat",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Italian Motors Club",
                Phone = "+20 11 2500 0057",
                Address = "Mirghany St",
                Location = "Heliopolis",
                SupportedBrand = "Fiat",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Fiat Service Hub Zayed",
                Phone = "+20 15 2500 0058",
                Address = "Wahat Road",
                Location = "Sheikh Zayed",
                SupportedBrand = "Fiat",
                IsActive = true
            },
            new RepairCenter
            {
                CenterId = Guid.NewGuid(),
                Name = "Giza Italian Auto Service",
                Phone = "+20 12 2500 0059",
                Address = "Giza Square, Mourad St",
                Location = "Giza",
                SupportedBrand = "Fiat, Alfa Romeo",
                IsActive = true
            }
        }

                    ;

                        await context.Set<RepairCenter>().AddRangeAsync(centers);
                        await context.SaveChangesAsync();



                    }






                }
            }
            catch (Exception ex)
            {
                throw;
            }








}
    }
}
