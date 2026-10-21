using Microsoft.AspNetCore.Mvc;

using pr6_trpo_nab.Models;

namespace pr6_trpo_nab.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CourseController : ControllerBase
    {

        private readonly WebApiDatabaseContext _db;
        public CourseController(WebApiDatabaseContext db) => _db = db;

        [HttpGet]
        public IActionResult GetAll([FromQuery] CourseFilter f)
        {
            // фильтрация teacher и minduration
            var list = _db.Courses
                .Where(c => f.Teacher == null || c.Teacher.Contains(f.Teacher))
                .Where(c => f.MinDuration == null || c.DurationHours >= f.MinDuration)
                .ToList();

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Course? course = _db.Courses.Find(id);

            if (course == null)
                return NotFound();

            return Ok(course);
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            course.Id = 0;
            _db.Courses.Add(course);
            _db.SaveChanges();

            return Created();
        }

        [HttpPut]
        public IActionResult Put(int id, Course course)
        {
            if (id != course.Id)
                return BadRequest();

            if (!_db.Courses.Any(c => c.Id == id))
                return NotFound();

            _db.Courses.Update(course);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPatch]
        public IActionResult Patch(int id, CoursePatchDto dto)
        {
            Course? course = _db.Courses.Find(id);

            if (course == null)
                return NotFound();

            if (dto.Title != null) course.Title = dto.Title;
            if (dto.Teacher != null) course.Teacher = dto.Teacher;
            if (dto.DurationHours.HasValue) course.DurationHours = dto.DurationHours.Value;
            if (dto.Price.HasValue) course.Price = dto.Price.Value;
            if (dto.IsActive.HasValue) course.IsActive = dto.IsActive.Value;

            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Course? course = _db.Courses.Find(id);

            if (course == null)
                return NotFound();

            _db.Courses.Remove(course);
            _db.SaveChanges();

            return Ok();
        }
    }
}
