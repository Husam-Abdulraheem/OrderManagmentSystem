using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(ApplicationDbContext db, TokenService tokenService, IWebHostEnvironment environment, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _db = db;
            _tokenService = tokenService;
            _environment = environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<object> Login(UsersAuth usersAuth)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == usersAuth.Email);

            if (user == null)
                return (new { Message = "Invalid Email!" });


            if (!BCrypt.Net.BCrypt.Verify(usersAuth.Password, user.PasswordHash))
                return (new { Message = "Email not found and/or password incorrect" });


            return new UserTokenDTO
            {
                Email = user.Email,
                Token = _tokenService.GenerateJwtToken(user),
            };
        }











        // Register Retailers Method

        public async Task<object> RegisterRetailer([FromForm] RegisterDTO registerDto)
        {
            var existingUser = _db.Users.FirstOrDefault(x => x.Email == registerDto.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("Email is already registered.");
            }

            string document = null;
            if (registerDto.BusinessDocument != null)
            {
                var resizedImage = await ImageService.ResizeAndCompressImage(registerDto.BusinessDocument);

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

                    document = "https://husamta-001-site1.htempurl.com/Images/" + uniqueFileName;
                }
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);


            User addUser = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
                PhoneNumber = registerDto.PhoneNumber,
                UserType = "Retailer",
                BusinessName = registerDto.BusinessName,
                BusinessDocument = document,
                IsAdmin = false,
                IsVerified = false,
                LogoUrl = null
            };

            Retailer retailer = new Retailer
            {
                User = addUser,
            };


            var createdUser = await _userManager.CreateAsync(addUser, registerDto.Password);
            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(addUser, "Retailer");
                if (roleResult.Succeeded)
                {
                    return (new { Message = "You have successfully registered." });
                }
                else
                {
                    throw new ArgumentException("Error when create User");
                }
            }
            _db.Retailers.Add(retailer);
            await _db.SaveChangesAsync();
            return new UserTokenDTO
            {
                Email = addUser.Email,
                Token = _tokenService.GenerateJwtToken(addUser)
            };

        }













        // Register Suppliers Method

        public async Task<object> RegisterSupplier([FromForm] RegisterDTO registerDto)
        {
            var existingUser = _db.Users.FirstOrDefault(x => x.Email == registerDto.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("Email is already registered.");
            }

            string document = null;
            if (registerDto.BusinessDocument != null)
            {
                var resizedImage = await ImageService.ResizeAndCompressImage(registerDto.BusinessDocument);

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

                    document = "https://husamta-001-site1.htempurl.com/Images/" + uniqueFileName;
                }
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);


            User addUser = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
                PhoneNumber = registerDto.PhoneNumber,
                UserType = "Supplier",
                BusinessName = registerDto.BusinessName,
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
            var createdUser = await _userManager.CreateAsync(addUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(addUser, "Supplier");
                if (roleResult.Succeeded)
                {
                    return (new { Message = "You have successfully registered." });

                }
                else
                {
                    throw new ArgumentException("Error when create User");
                }
            }
            _db.Suppliers.Add(supplier);
            await _db.SaveChangesAsync();
            return new UserTokenDTO
            {
                Email = addUser.Email,
                Token = _tokenService.GenerateJwtToken(addUser)
            };
        }

    }
}
