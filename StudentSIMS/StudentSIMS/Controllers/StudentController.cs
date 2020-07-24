using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSIMS.Data;
using StudentSIMS.Models;

namespace StudentSIMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _context;

        public StudentController(StudentContext context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {

            return await _context.Student.Include(a => a.address).ToListAsync();
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Student/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.studentId)
            {
                return BadRequest();
            }

            if (!StudentExists(id))
                return BadRequest();

            var updateStudent = await _context.Student.FirstOrDefaultAsync(s => s.studentId == student.studentId);

            _context.Entry(updateStudent).State = EntityState.Modified;

            updateStudent.firstName = student.firstName;
            updateStudent.middleName = student.middleName;
            updateStudent.lastName = student.lastName;
            updateStudent.emailAddress = student.emailAddress;
            updateStudent.phoneNumber = student.phoneNumber;

            await _context.SaveChangesAsync();

            return NoContent();


        }

        // POST: api/Student
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            student.timeCreated = DateTime.Now;
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.studentId }, student);
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.studentId == id);
        }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressContext _context;

        public AddressController(AddressContext context)
        {
            _context = context;
        }

        // GET: api/Address
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            return await _context.Address.Include(a => a.student).ToListAsync();
        }

        // GET: api/Address/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            //var address = await _context.Address.FindAsync(id);
            var address = await _context.Address.Include(i => i.student).FirstOrDefaultAsync(i => i.studentID == id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Address/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.studentID)
            {
                return BadRequest();
            }

            if (!AddressExists(id))
            {
                return BadRequest();
            }

            var updateAddress = await _context.Address.FirstOrDefaultAsync(s => s.studentID == address.studentID);

            _context.Entry(updateAddress).State = EntityState.Modified;

            updateAddress.streetNumber = address.streetNumber;
            updateAddress.street = address.street;
            updateAddress.suburb = address.suburb;
            updateAddress.city = address.city;
            updateAddress.postcode = address.postcode;
            updateAddress.country = address.country;

            await _context.SaveChangesAsync();

          
            return NoContent();
        }

        // POST: api/Address
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {

            _context.Address.Add(address);
            await _context.SaveChangesAsync();
         
            return CreatedAtAction("GetAddress", new { id = address.studentID }, address);
        }

        // DELETE: api/Address/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return address;
        }

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.studentID == id);
        }
    }


}
