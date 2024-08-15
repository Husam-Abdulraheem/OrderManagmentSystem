using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Authentication;
using OrderManagementSystem.Data;
using OrderManagementSystem.Data.Models;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _db;
        private readonly TokenService _tokenService;
        private readonly IWebHostEnvironment _environment;

        public AccountService(ApplicationDbContext db, TokenService tokenService, IWebHostEnvironment environment)
        {
            _db = db;
            _tokenService = tokenService;
            _environment = environment;
        }

        public async Task<object> Login(UsersAuth usersAuth)
        {

            var userLogin = await _db.Users.Where(x => x.Email == usersAuth.Email).FirstOrDefaultAsync();

            if (userLogin != null && BCrypt.Net.BCrypt.Verify(usersAuth.Password, userLogin.PasswordHash))
            {
                if (userLogin.UserType == "Supplier")
                {
                    var supplierId = await _db.Suppliers.Where(x => x.User.Email == userLogin.Email).FirstOrDefaultAsync();
                    var token = _tokenService.GenerateJwtToken(usersAuth.Email, userLogin.UserType, supplierId.Id);

                    return new { token };
                }
                if (userLogin.UserType == "Retailer")
                {
                    var retailerId = await _db.Retailers.Where(x => x.User.Email == userLogin.Email).FirstOrDefaultAsync();
                    var token = _tokenService.GenerateJwtToken(usersAuth.Email, userLogin.UserType, retailerId.Id);

                    return new { token };
                }
                return (new { Message = "You don't have Account" });
            }
            else
            {
                return (new { Message = "You don't have Account or uncorect password" });
            }
        }

        // Register Retailers Method

        public async Task<object> RegisterRetailer([FromForm] RegisterDTO userData)
        {
            var existingUser = _db.Users.FirstOrDefault(x => x.Email == userData.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("Email is already registered.");
            }

            string document = null;
            if (userData.BusinessDocument != null)
            {
                var resizedImage = await ImageService.ResizeAndCompressImage(userData.BusinessDocument);

                if (resizedImage != null)
                {
                    string imagesFolderPath = Path.Combine(_environment.WebRootPath, "Images");
                    if (!Directory.Exists(imagesFolderPath))
                        Directory.CreateDirectory(imagesFolderPath);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + resizedImage.FileName;
                    string filePath = Path.Combine(imagesFolderPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await resizedImage.CopyToAsync(fileStream);
                    }

                    document = "/Images/" + uniqueFileName;
                }
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userData.Password);


            User addUser = new User
            {
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Email = userData.Email,
                PasswordHash = hashedPassword,
                PhoneNumber = userData.PhoneNumber,
                UserType = "Retailer",
                BusinessName = userData.BusinessName,
                BusinessDocument = document,
                IsAdmin = false,
                IsVerified = false,
                LogoUrl = null
            };

            Retailer retailer = new Retailer
            {
                User = addUser,
            };


            _db.Users.Add(addUser);
            _db.Retailers.Add(retailer);
            await _db.SaveChangesAsync();
            return (new { Message = "You have successfully registered." });

        }


        // Register Suppliers Method

        public async Task<object> RegisterSupplier([FromForm] RegisterDTO userData)
        {
            var existingUser = _db.Users.FirstOrDefault(x => x.Email == userData.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("Email is already registered.");
            }

            string document = null;
            if (userData.BusinessDocument != null)
            {
                var resizedImage = await ImageService.ResizeAndCompressImage(userData.BusinessDocument);

                if (resizedImage != null)
                {
                    string imagesFolderPath = Path.Combine(_environment.WebRootPath, "Images");
                    if (!Directory.Exists(imagesFolderPath))
                        Directory.CreateDirectory(imagesFolderPath);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + resizedImage.FileName;
                    string filePath = Path.Combine(imagesFolderPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await resizedImage.CopyToAsync(fileStream);
                    }

                    document = "/Images/" + uniqueFileName;
                }
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userData.Password);


            User addUser = new User
            {
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Email = userData.Email,
                PasswordHash = hashedPassword,
                PhoneNumber = userData.PhoneNumber,
                UserType = "Supplier",
                BusinessName = userData.BusinessName,
                BusinessDocument = document,
                IsAdmin = false,
                IsVerified = false,
                LogoUrl = null
            };

            Supplier supplier = new Supplier
            {
                User = addUser,
                Subscription = "none",

            };


            _db.Users.Add(addUser);
            _db.Suppliers.Add(supplier);
            await _db.SaveChangesAsync();
            return (new { Message = "You have successfully registered." });
        }
    }
}
