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


            if (!await _userManager.CheckPasswordAsync(user, usersAuth.Password))
                return (new { Message = "Email not found and/or password incorrect" });

            var supplier = await _db.Suppliers.FirstOrDefaultAsync(x => x.User.Id == user.Id);
            var retailer = await _db.Retailers.FirstOrDefaultAsync(x => x.User.Id == user.Id);

            int? roleId = supplier?.Id ?? retailer?.Id;

            return new UserTokenDTO
            {
                Token = _tokenService.GenerateJwtToken(roleId, user),
            };
        }











        // Register Retailers Method

        public async Task<object> RegisterRetailer([FromForm] RegisterDTO registerDto)
        {
            // Check if the user already exists
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("Email is already registered.");
            }

            // Image processing if present
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

                    document = $"https://husamta-001-site1.htempurl.com/Images/{uniqueFileName}";
                }
            }


            // Create User Object
            User addUser = new User
            {
                UserName = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserType = "Retailer",
                BusinessName = registerDto.BusinessName,
                BusinessDocument = document,
                IsAdmin = false,
                IsVerified = false,
                LogoUrl = null
            };

            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    // Create a Retailer object and associate it with the user.
                    Retailer retailer = new Retailer
                    {
                        User = addUser,
                    };


                    // Add User Using Identity
                    // Password encryption
                    var createdUser = await _userManager.CreateAsync(addUser, registerDto.Password);
                    if (!createdUser.Succeeded)
                    {
                        var errors = string.Join(", ", createdUser.Errors.Select(e => e.Description));
                        throw new Exception($"User creation failed: {errors}");
                    }

                    // Add Role
                    var roleResult = await _userManager.AddToRoleAsync(addUser, "Retailer");
                    if (!roleResult.Succeeded)
                    {
                        var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                        throw new Exception($"Role assignment failed: {errors}");
                    }

                    // Add Retailer Data To Database
                    _db.Retailers.Add(retailer);
                    await _db.SaveChangesAsync();

                    await transaction.CommitAsync();

                    var retailerId = await _db.Retailers.FirstOrDefaultAsync(x => x.User.Id == addUser.Id);
                    int? supplier = null;
                    int? roleId = supplier ?? retailerId?.Id;
                    // Return registration result with Token and Email
                    return new UserTokenDTO
                    {
                        Token = _tokenService.GenerateJwtToken(roleId, addUser)
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Transaction failed: " + ex.Message, ex);
                }
            }
        }







        // Register Suppliers Method

        public async Task<object> RegisterSupplier([FromForm] RegisterDTO registerDto)
        {
            // Check if the user already exists
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("Email is already registered.");
            }

            // Image processing if present
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

                    document = $"https://husamta-001-site1.htempurl.com/Images/{uniqueFileName}";
                }
            }



            // Create User Object
            User addUser = new User
            {
                UserName = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserType = "Supplier",
                BusinessName = registerDto.BusinessName,
                BusinessDocument = document,
                IsAdmin = false,
                IsVerified = false,
                LogoUrl = null
            };

            // Add User Using Identity
            // Password encryption
            var createdUser = await _userManager.CreateAsync(addUser, registerDto.Password);
            if (!createdUser.Succeeded)
            {
                // Collect errors in one message
                var errors = string.Join(", ", createdUser.Errors.Select(e => e.Description));
                throw new Exception($"Error when creating user: {errors}");
            }

            // Add Role
            var roleResult = await _userManager.AddToRoleAsync(addUser, "Supplier");
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                throw new Exception($"Error when assigning role to the user: {errors}");
            }

            // Create a resource object and associate it with a user.
            Supplier supplier = new Supplier
            {
                User = addUser,
                Subscription = null,
            };
            // Add Supplier to database
            try
            {
                _db.Suppliers.Add(supplier);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Transaction failed: An error occurred while saving the supplier data.", ex);
            }

            // Return registration result with token
            var supplierId = await _db.Suppliers.FirstOrDefaultAsync(x => x.User.Id == addUser.Id);
            int? retailer = null;
            int? roleId = supplierId?.Id ?? retailer;
            return new UserTokenDTO
            {
                Token = _tokenService.GenerateJwtToken(roleId, addUser)
            };
        }



    }
}
