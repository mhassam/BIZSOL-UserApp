using Microsoft.AspNetCore.Mvc;
using UserApp_Backend.Models;
using UserApp_Backend.Models.Dtos;

namespace UserApp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> _users = new List<User>();
        private readonly IWebHostEnvironment _hostEnvironment;

        public UsersController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _users;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(Guid id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public ActionResult<User> Create([FromForm]UserInputDto userInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = userInput.Name,
                Email = userInput.Email,
                Phone = userInput.Phone,
                Address = userInput.Address,
                PicturePath = SavePicture(userInput.Picture)
            };
            _users.Add(user);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UserInputDto userInput)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = userInput.Name;
            user.Email = userInput.Email;
            user.Phone = userInput.Phone;
            user.Address = userInput.Address;

            if (userInput.Picture != null)
            {
                DeletePicture(user.PicturePath);
                user.PicturePath = SavePicture(userInput.Picture);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _users.Remove(user);
            DeletePicture(user.PicturePath);

            return NoContent();
        }

        private string SavePicture(IFormFile picture)
        {
            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(picture.FileName)}";
            var filePath = Path.Combine(_hostEnvironment.WebRootPath, "profileimages", uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                picture.CopyTo(fileStream);
            }

            return $"profileimages/{uniqueFileName}";
        }

        private void DeletePicture(string picturePath)
        {
            var filePath = Path.Combine(_hostEnvironment.WebRootPath, picturePath.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
