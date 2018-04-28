using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MfaCsvUploader.Data;
using MfaCsvUploader.Data.Entities;
using MfaCsvUploader.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.IO.File;

namespace MfaCsvUploader.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly CsvContext _csvContext;

        public HomeController(CsvContext csvContext)
        {
            _csvContext = csvContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null) return View("Index", new HomeViewModel {Message = "File was empty!"});

                var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var uploadedTeachers = ReadTeachersFromCsv(filePath);
                await _csvContext.Teachers.AddRangeAsync(uploadedTeachers);
                await _csvContext.SaveChangesAsync();

                return View("Index", new HomeViewModel {Message = "Uploaded Successfully!", Succcess = true});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View("Index", new HomeViewModel {Message = "Currently we are having issues!"});
            }
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        private static IEnumerable<CsvTeacher> ReadTeachersFromCsv(string filePath)
        {
            return ReadAllLines(filePath)
                .Skip(1) // Skip Headers
                .Select(row => row.Split(','))
                .Select(col => new CsvTeacher
                {
                    Name = col[0] + new Random().Next(int.MaxValue),
                    Course = col[1],
                    Gender = col[2]
                });
        }
    }
}