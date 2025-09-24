using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //https://localhost:7001/api/members
    
    public class MembersController(AppDbContext context) : BaseAPIController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.AppUsers.ToListAsync();
            return members;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetMembers(string id)
        {
            var member = await context.AppUsers.FindAsync(id);
            if (member == null) return NotFound();
            return member;
        }
    }
}

//         private readonly AppDbContext _context = context;

//         [HttpGet]
//         public IActionResult GetMembers()
//         {
//             var members = _context.Members.ToList();
//             return Ok(members);
//         }

//         [HttpGet("{id}")]
//         public IActionResult GetMember(int id)
//         {
//             var member = _context.Members.Find(id);
//             if (member == null)
//             {
//                 return NotFound();
//             }
//             return Ok(member);
//         }

//         [HttpPost]
//         public IActionResult CreateMember(Member member)
//         {
//             _context.Members.Add(member);
//             _context.SaveChanges();
//             return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
//         }

//         [HttpPut("{id}")]
//         public IActionResult UpdateMember(int id, Member updatedMember)
//         {
//             var member = _context.Members.Find(id);
//             if (member == null)
//             {
//                 return NotFound();
//             }

//             member.Name = updatedMember.Name;
//             member.Email = updatedMember.Email;
//             // Update other properties as needed

//             _context.SaveChanges();
//             return NoContent();
//         }

//         [HttpDelete("{id}")]
//         public IActionResult DeleteMember(int id)
//         {
//             var member = _context.Members.Find(id);
//             if (member == null)
//             {
//                 return NotFound();
//             }

//             _context.Members.Remove(member);
//             _context.SaveChanges();
//             return NoContent();
//         }
//     }
// }